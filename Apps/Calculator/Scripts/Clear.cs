using Godot;
using System;

namespace Calculator;

public partial class Clear : Button
{
	[Export]
	public Processor Processor;

    public override void _Pressed()
    {
        base._Pressed();
		Processor.Expression = "";
    }
}
