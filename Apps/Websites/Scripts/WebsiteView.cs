using Godot;
using System;
using System.Collections.Generic;

namespace Websites;

public partial class WebsiteView : Control 
{
    string coolAddress = "web://passionfruit.com/markisaos/me/home.tscn";
    Control previousThing;
    List<string> addresses = new();
    int addressIndex = -1;

    public override void _Ready() {
        base._Ready();
        LoadStuff(coolAddress);
    }

    public override void _Process(double delta) {
        base._Process(delta);
        GetNode<Button>("Toolbar/Back").Disabled = addressIndex == 0;
        GetNode<Button>("Toolbar/Forward").Disabled = addressIndex == addresses.Count-1;
    }

    public void LoadStuff(string newText) {
        if (addresses.Count > 0) {
            if (addresses[addressIndex] != newText) {
                addresses.Add(newText);
                addressIndex++;
            }
        } else {
            addresses.Add(newText);
            addressIndex++;
        }

        GetNode<LineEdit>("Toolbar/Address").Text = newText;
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
            var ohShoes = GD.Load<PackedScene>("res://Web/404/home.tscn");
            var m = ohShoes.Instantiate<Control>();
            m.OffsetTop = 60;
            AddChild(m);
            previousThing = m;
            GetNode<Label>("TabTitle").Text = "404";
        } else {
            previousThing?.QueueFree();
            var jgkjfgkb = GD.Load<PackedScene>(h);
            var m = jgkjfgkb.Instantiate<Control>();
            m.OffsetTop = 60;
            AddChild(m);
            previousThing = m;
            GetNode<Label>("TabTitle").Text = tabTitle;
        }
    }

    public void Forward() {
        addressIndex++;
        LoadStuff(addresses[addressIndex]);
    }

    public void Back() {
        addressIndex--;
        LoadStuff(addresses[addressIndex]);
    }

    public void Refresh() {
        LoadStuff(addresses[addressIndex]);
    }

    public void Home() {
        LoadStuff("web://passionfruit.com/markisaos/me/home.tscn");
    }
}
