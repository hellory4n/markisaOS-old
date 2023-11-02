using Godot;
using System;

public class BuyFactories : Button {
    Sausage sausage;

    public override void _Ready() {
        base._Ready();
        sausage = GetNode<Sausage>("../../../../../SausageButClick");
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        if (sausage.Sausages < 25)
            return;

        sausage.Sausages -= 25;
        sausage.Factories += 1;
        sausage.ScoreText.Text = $"{sausage.Sausages:G} sausages";
        GetNode<Label>("../IndustrialRevolution").Text = $"Factories - {sausage.Factories:G} owned\nAutomatically create more sausages";
    }
}
