using RaceSharp;

namespace Zebra.Utils;

public enum AccentColor
{
	None,
	Info,
	Success,
	Warning,
	Danger
}

public static class AccentColorExtensions
{
	public static string CssClassName(this AccentColor color)
	{
		return "accent-" + color.ToString().PascalToKebabCase();
	}
}