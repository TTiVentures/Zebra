using Microsoft.AspNetCore.Components;
using Zebra.Services.Modal;

namespace Zebra.Components.Modal;

/// <summary>
/// Defines a fragment of razor components that will be displayed inside a modal whenever requested.
/// </summary>
/// <typeparam name="TValue">The type of the value this modal will return.</typeparam>
public class ModalTemplate<TValue> : ComponentBase, IModalTemplate
{
	[Inject]
	private ModalService ModalService { get; set; } = default!;

	public record ModalEvents(Action<TValue>? OnOk, Action<TValue>? OnCancel, ModalService ModalService)
	{
		/// <summary>
		/// Dispatches the <see cref="OnOk"/> callback.
		/// </summary>
		/// <param name="value">Value to pass to the callback.</param>
		public void Ok(TValue value) {
			OnOk?.Invoke(value);
			ModalService.Close();
		}

		/// <summary>
		/// Dispatches the <see cref="OnCancel"/> callback.
		/// </summary>
		/// <param name="value">Value to pass to the callback.</param>
		public void Cancel(TValue value) {
			OnCancel?.Invoke(value);
			ModalService.Close();
		}
	}

	[Parameter]
	public RenderFragment<ModalEvents> ChildContent { get; set; } = default!;

	/// <summary>
	/// Callback that will be called when the template calls <c>@context.Ok()</c>
	/// </summary>
	[Parameter]
	public Action<TValue>? OnOk { get; set; }

	/// <summary>
	/// Callback that will be called when the template calls <c>@context.Cancel()</c>
	/// </summary>
	[Parameter]
	public Action<TValue>? OnCancel { get; set; }

	[Parameter]
	public Action? OnDismiss { get; set; }

	/// <summary>
	/// Title that will be displayed at the header of the modal.
	/// </summary>
	[Parameter]
	public string? Title { get; set; }

	[Parameter]
	public bool DismissOnOutClick { get; set; } = true;

	public RenderFragment TemplateContent => ChildContent(new ModalEvents(OnOk, OnCancel, ModalService));

	/// <summary>
	/// Requests the <see cref="ModalService"/> to display this <see cref="ModalContent"/> in a modal.
	/// </summary>
	public void Show() => ModalService.Show(this);

	public void Close() => ModalService.Close(this);

	public void Dismiss()
	{
		if (!DismissOnOutClick)
			return;

		Close();
		OnDismiss?.Invoke();
	}
}

public interface IModalTemplate
{
	public void Dismiss();

	[Parameter]
	public string? Title { get; set; }

	public RenderFragment TemplateContent { get; }
}