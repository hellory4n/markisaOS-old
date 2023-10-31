using Godot;
using System;

public class Sausage : Button {
    long Sausages = 0;
    Label ScoreText;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
        ScoreText = GetNode<Label>("../Score");
    }

    public void Click() {
       Sausages++;
       ScoreText.Text = $"{Sausages:G} sausages";
    }
}
