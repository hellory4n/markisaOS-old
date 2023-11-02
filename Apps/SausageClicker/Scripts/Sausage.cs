using Godot;
using System;

public class Sausage : Button {
    public long Sausages = 0;
    public long Factories = 0;
    public long Workers = 1;
    public Label ScoreText;
    Timer timer = new Timer {
        WaitTime = 1,
        Autostart = true
    };

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
        AddChild(timer);
        timer.Connect("timeout", this, nameof(TheIndustrialRevolutionAndItsConsequencesHaveBeenADisasterForTheHumanRace));
        ScoreText = GetNode<Label>("../Score");
    }

    public void Click() {
       Sausages += Workers;
       ScoreText.Text = $"{Sausages:G} sausages";
    }

    public void TheIndustrialRevolutionAndItsConsequencesHaveBeenADisasterForTheHumanRace() {
        Sausages += Factories;
        ScoreText.Text = $"{Sausages:G} sausages";
    }
}
