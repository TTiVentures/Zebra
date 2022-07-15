using Microsoft.AspNetCore.Components;
using Zebra.Components.Modal;

namespace Zebra.Services.Modal;


public class ModalService
{
	public EventHandler<IModalTemplate?>? OnClose { get; set; }
	public EventHandler<IModalTemplate>? OnOpen { get; set; }

	/// <summary>
	/// Close the modal.
	/// </summary>
	public void Close()
	{
		OnClose?.Invoke(this, null);
	}

	/// <summary>
	/// Close a modal template.
	/// </summary>
	public void Close(IModalTemplate template)
	{
		OnClose?.Invoke(this, template);
	}

	/// <summary>
	/// Show a <see cref="ModalTemplate{TValue}"/> on the dialog.
	/// </summary>
	/// <param name="template"><see cref="ModalTemplate{TValue}"/> to show.</param>
	/// <typeparam name="T">Type of the value the templates holds on its <c>TValue</c>.</typeparam>
	public void Show<T>(ModalTemplate<T> template)
	{
		OnOpen?.Invoke(this, template);
	}
}

public class InvalidModalException : Exception
{
	public InvalidModalException(string message) : base(message)
	{ }
}