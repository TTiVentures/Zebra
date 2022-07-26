using Microsoft.AspNetCore.Components;
using Zebra.Components.Modal;


public abstract class DefaultTemplateBase<TValue> : ComponentBase
{
	[Parameter, EditorRequired]
	public ModalEvents<TValue> Ctx { get; set; } = default!;
}