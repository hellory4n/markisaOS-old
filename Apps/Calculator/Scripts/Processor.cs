using Godot;
using System;

namespace Calculator;

public partial class Processor : Node
{
    [Export]
    public LineEdit TheThing;
    public string Expression
    {
        get { return TheThing.Text; }
        set { TheThing.Text = value; }
    }
}
