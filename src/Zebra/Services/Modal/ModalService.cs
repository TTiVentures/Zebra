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

	public void Close()
	{
		OnClose?.Invoke(this, EventArgs.Empty);
	}

	public void Show<T>(ModalTemplate<T> template, string? title = null)
	{
		OnOpen?.Invoke(this, new ModalContent() { Title = title, Content = template.TemplateContent });
	}
}