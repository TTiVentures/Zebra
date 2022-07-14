using Microsoft.AspNetCore.Components;
using Zebra.Services.Modal;

namespace Zebra.Components.Modal;

/// <summary>
/// Defines a fragment of razor components that will be displayed inside a modal whenever requested.
/// </summary>
/// <typeparam name="TValue">The type of the value this modal will return.</typeparam>
/// <param name="OnCancel">Callback that will be called when the template calls <code>@context.Cancel()</code></param>
public class ModalTemplate<TValue> : ComponentBase
{
	[Inject]
	private ModalService ModalService { get; set; } = default!;

	public record DialogEvents(Action<TValue>? OnOk, Action<TValue>? OnCancel, ModalService ModalService)
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
	public RenderFragment<DialogEvents> ChildContent { get; set; } = default!;

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

	/// <summary>
	/// Title that will be displayed at the header of the modal.
	/// </summary>
	[Parameter]
	public string? Title { get; set; }

	public RenderFragment TemplateContent => ChildContent(new DialogEvents(OnOk, OnCancel, ModalService));

	/// <summary>
	/// Requests the <see cref="ModalService"/> to display this <see cref="ModalContent"/> in a modal.
	/// </summary>
	public void Show() => ModalService.Show(this);
}