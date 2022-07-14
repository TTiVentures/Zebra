using Microsoft.AspNetCore.Components;
using Zebra.Services.Modal;

namespace Zebra.Components.Modal;

public class ModalTemplate<TValue> : ComponentBase
{
	[Inject]
	private ModalService ModalService { get; set; } = default!;

	public record DialogEvents(Action<TValue>? OnOk, Action<TValue>? OnCancel, ModalService ModalService)
	{
		public void Ok(TValue value) {
			OnOk?.Invoke(value);
			ModalService.Close();
		}

		public void Cancel(TValue value) {
			OnCancel?.Invoke(value);
			ModalService.Close();
		}
	}

	[Parameter]
	public RenderFragment<DialogEvents> ChildContent { get; set; } = default!;

	[Parameter]
	public Action<TValue>? OnOk { get; set; }

	[Parameter]
	public Action<TValue>? OnCancel { get; set; }

	public RenderFragment TemplateContent => ChildContent(new DialogEvents(OnOk, OnCancel, ModalService));

	public void Show(string? title = null) => ModalService.Show(this, title);
}