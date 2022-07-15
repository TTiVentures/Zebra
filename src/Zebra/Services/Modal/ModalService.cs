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
	private IModalTemplate? CurrentTemplate { get; set; }
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
	/// Closes the specified modal template.
	/// </summary>
	/// <param name="template">Template to close</param>
	/// <exception cref="InvalidModalException">Raised when the specified template is not currently open.</exception>
	public void Close(IModalTemplate template)
	{
		if (!ReferenceEquals(template, CurrentTemplate))
			throw new InvalidModalException("Cannot close a modal that is not open.");

		Close();
	}

	/// <summary>
	/// Show a <see cref="ModalTemplate{TValue}"/> on the dialog.
	/// </summary>
	/// <param name="template"><see cref="ModalTemplate{TValue}"/> to show.</param>
	/// <typeparam name="T">Type of the value the templates holds on its <c>TValue</c>.</typeparam>
	public void Show<T>(ModalTemplate<T> template)
	{
		CurrentTemplate = template;
		OnOpen?.Invoke(this, new ModalContent() { Title = template.Title, Content = template.TemplateContent });
	}
}

public class InvalidModalException : Exception
{
	public InvalidModalException(string message) : base(message)
	{ }
}