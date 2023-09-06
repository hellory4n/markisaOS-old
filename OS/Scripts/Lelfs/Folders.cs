using Godot;
using System;
using System.Collections.Generic;

public class Folder : BaseLelfs {
    public new string Type = "Folder";
    public List<string> Items = new List<string>();

    // yes :)
    public Folder(string name, string parent = null) : base(name, parent) {}
}
