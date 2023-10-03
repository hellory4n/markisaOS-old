using Godot;
using System;

public class WebsiteView : Control {
    string coolAddress = "web://passionfruit.com/lelcubeos/me/home.tscn";
    Control previousThing;

    public override void _Ready() {
        base._Ready();
        //GetNode<LineEdit>("Toolbar/Address").Connect("text_entered", this, nameof(LoadStuff));
        LoadStuff(coolAddress);
    }

    public void LoadStuff(string newText) {
        coolAddress = newText;
        // parse the address :)
        string h = coolAddress.Replace("web://", "res://Web/");
        // maybe the user didn't put the "home.tscn" thing at the end
        if (!ResourceLoader.Exists(h)) {
            h += "/home.tscn";
            GetNode<LineEdit>("Toolbar/Address").Text += "/home.tscn";
        }
        // or maybe the user forgor to put the web:// thingy
        if (!ResourceLoader.Exists(h)) {
            h = "res://Web/" + h;
            GetNode<LineEdit>("Toolbar/Address").Text = "web://" + GetNode<LineEdit>("Toolbar/Address").Text;
        }

        // get the tab title
        string tabTitle = h.Replace("res://Web/", "");
        tabTitle = tabTitle.Substring(0, tabTitle.IndexOf('/'));

        // actually load the website :)
        // we need to check if it's on the web folder so you can't just ask the browser
        // to load the login screen
        if (!ResourceLoader.Exists(h) || !h.StartsWith("res://Web/")) {
            previousThing?.QueueFree();
            var ohShoes = ResourceLoader.Load<PackedScene>("res://Web/404/home.tscn");
            var m = ohShoes.Instance<Control>();
            m.MarginTop = 60;
            AddChild(m);
            previousThing = m;
            GetNode<Label>("TabTitle").Text = "404";
        } else {
            previousThing?.QueueFree();
            var jgkjfgkb = ResourceLoader.Load<PackedScene>(h);
            var m = jgkjfgkb.Instance<Control>();
            m.MarginTop = 60;
            AddChild(m);
            previousThing = m;
            GetNode<Label>("TabTitle").Text = tabTitle;
        }
    }
}
