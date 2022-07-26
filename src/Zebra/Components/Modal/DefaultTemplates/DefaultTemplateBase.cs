using Microsoft.AspNetCore.Components;

namespace Zebra.Components.Modal;

public class DefaultTemplateBase<TValue> : ComponentBase
{
	[Parameter, EditorRequired] public ModalEvents<TValue> Ctx { get; set; } = default!;
}