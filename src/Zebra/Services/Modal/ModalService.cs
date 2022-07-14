using Microsoft.AspNetCore.Components;
using Zebra.Components.Modal;

namespace Zebra.Services.Modal;

public class ModalContent : EventArgs
{
	public string? Title { get; set; }
	public RenderFragment Content { get; set; } = default!;
}

public class ModalService
{
	public EventHandler<EventArgs>? OnClose { get; set; }
	public EventHandler<ModalContent>? OnOpen { get; set; }

	/// <summary>
	/// Close the modal.
	/// </summary>
	public void Close()
	{
		OnClose?.Invoke(this, EventArgs.Empty);
	}

	/// <summary>
	/// Show a <see cref="ModalTemplate{TValue}"/> on the dialog.
	/// </summary>
	/// <param name="template"><see cref="ModalTemplate{TValue}"/> to show.</param>
	/// <typeparam name="T">Type of the value the templates holds on its <c>TValue</c>.</typeparam>
	public void Show<T>(ModalTemplate<T> template)
	{
		OnOpen?.Invoke(this, new ModalContent() { Title = template.Title, Content = template.TemplateContent });
	}
}