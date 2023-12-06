using Godot;
using System;

namespace Dashboard.Toolkit;

[GlobalClass]
public partial class MksTabRoot : Control
{
    [Export]
    public string TabTitle = "New Tab";
    public bool IsTabActive;
}
