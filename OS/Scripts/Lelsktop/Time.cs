using Godot;
using System;

namespace Lelsktop.Interface;

public partial class Time : Button
{
    public override void _Process(double delta)
    {
        base._Process(delta);
        DateTime time = DateTime.Now;
        time = time.AddYears(2069-time.Year);
        Text = $"{time.Day:D2}/{time.Month:D2}/{time.Year:D2} {time.Hour:D2}:{time.Minute:D2}";
    }
}
