using Microsoft.AspNetCore.Components;
using Zebra.Components;

namespace Zebra.Services;

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

	public void Open<T>(ModalTemplate<T> content)
	{
		// OnOpen?.Invoke(this, content);
	}
}