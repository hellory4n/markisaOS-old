using Godot;
using System;

namespace Calculator;

public partial class AddStuff : Button
{
	[Export]
	public string Stuff;
	[Export]
	public Processor Processor;

    public override void _Pressed()
    {
        base._Pressed();
		Processor.Expression += Stuff;
    }
}
