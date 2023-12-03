using Godot;
using Kickstart.Records;
using System;

namespace Settings;

public partial class ListThemes : VBoxContainer
{
    public override void _Ready()
    {
        base._Ready();
		Record<DashboardConfig> record = new();
		ButtonGroup lol = new();

		foreach (var theme in record.Data.Themes)
		{
            Button m = new()
            {
                Text = theme.Key,
                Theme = GD.Load<Theme>(theme.Value),
                ButtonGroup = lol,
                ToggleMode = true,
				ButtonPressed = record.Data.Theme == theme.Value,
				TextOverrunBehavior = TextServer.OverrunBehavior.TrimEllipsis,
				CustomMinimumSize = new Vector2(320, 40)
            };

			AddChild(m);
        }
    }
}
