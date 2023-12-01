using Godot;
using System;
using System.Data;

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

    public void Calculate()
    {
        // this does something i don't understand
        DataTable hi = new();
        DataColumn column = new("Result", typeof(double), Expression);
        hi.Columns.Add(column);
        DataRow row = hi.NewRow();
        hi.Rows.Add(row);

        double result = (double)row["Result"];
        Expression = result.ToString();
    }
}
