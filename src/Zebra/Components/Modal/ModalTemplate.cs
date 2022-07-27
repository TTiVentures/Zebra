using Microsoft.AspNetCore.Components;
using Zebra.Services.Modal;
using Zebra.Utils;

namespace Zebra.Components.Modal;

/// <summary>
/// Defines a fragment of razor components that will be displayed inside a modal whenever requested.
/// </summary>
/// <typeparam name="TValue">The type of the value this modal will return.</typeparam>
public class ModalTemplate<TValue> : ComponentBase, IModalTemplate
{
	[Inject]
	private ModalService ModalService { get; set; } = default!;

	[Parameter]
	public RenderFragment<ModalData<TValue>> ChildContent { get; set; } = default!;

	[Parameter]
	public AccentColor Accent { get; set; }

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

	public RenderFragment TemplateContent => ChildContent(new ModalData<TValue>(ModalService, this));

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

	public AccentColor Accent { get; set; }

	[Parameter]
	public string? Title { get; set; }

	public RenderFragment TemplateContent { get; }
}

public record ModalData<T>(ModalService ModalService, ModalTemplate<T> Template)
{
	/// <summary>
	/// Dispatches the <see cref="OnOk"/> callback.
	/// </summary>
	/// <param name="value">Value to pass to the callback.</param>
	public void Ok(T value) {
		Template.OnOk?.Invoke(value);
		ModalService.Close();
	}

	/// <summary>
	/// Dispatches the <see cref="OnCancel"/> callback.
	/// </summary>
	/// <param name="value">Value to pass to the callback.</param>
	public void Cancel(T value) {
		Template.OnCancel?.Invoke(value);
		ModalService.Close();
	}

	public AccentColor Accent => Template.Accent;
}