using Microsoft.AspNetCore.Components;
using Zebra.Utils;

namespace Zebra.Components.Interfaces;

public interface IAccentComponent
{
	[Parameter]
	public AccentColor Accent { get; set; }
}