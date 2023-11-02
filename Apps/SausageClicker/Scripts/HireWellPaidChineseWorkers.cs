using Godot;
using System;

public class HireWellPaidChineseWorkers : Button {
    Sausage sausage;

    public override void _Ready() {
        base._Ready();
        sausage = GetNode<Sausage>("../../../../../SausageButClick");
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        if (sausage.Sausages < 5)
            return;

        sausage.Sausages -= 5;
        sausage.Workers += 1;
        sausage.ScoreText.Text = $"{sausage.Sausages:G} sausages";
        GetNode<Label>("../WellPaidChineseWorkers").Text = $"Workers - {sausage.Workers:G} hired\nMake more sausages per click";
    }
}
