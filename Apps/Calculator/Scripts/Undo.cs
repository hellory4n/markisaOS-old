using Godot;
using System;

namespace Calculator;

public partial class Undo : Button
{
	[Export]
	public Processor Processor;

    public override void _Pressed()
    {
        base._Pressed();
		// Processor.Expression
		if (Processor.Expression == "")
			return;
		
		Processor.Expression = Processor.Expression.Remove(Processor.Expression.Length-1, 1);
    }
}
