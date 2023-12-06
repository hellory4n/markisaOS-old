using Godot;
using Kickstart.Records;
using System;

namespace Settings;

public partial class Languages : VBoxContainer
{
	[Export]
	CheckBox English;
	[Export]
	CheckBox Portuguese;
	[Export]
	CheckBox Spanish;
	[Export]
	CheckBox Russian;

    public override void _Ready()
    {
        base._Ready();
		ButtonGroup fuck = new();
		English.ButtonGroup = fuck;
		Portuguese.ButtonGroup = fuck;
		Spanish.ButtonGroup = fuck;
		Russian.ButtonGroup = fuck;

		switch (TranslationServer.GetLocale())
		{
			case "en":
				English.ButtonPressed = true;
				break;
			case "pt-br":
				Portuguese.ButtonPressed = true;
				break;
			case "es":
				Spanish.ButtonPressed = true;
				break;
			case "ru":
				Russian.ButtonPressed = true;
				break;
		}

		fuck.Pressed += Jgjehjhjsiyuawitejti;
    }

	public void Jgjehjhjsiyuawitejti(BaseButton button)
	{
		// a switch statement doesn't work for this
		string fucker = "";
		if (button == English)
			fucker = "en";
		else if (button == Portuguese)
			fucker = "pt-br";
		else if (button == Spanish)
			fucker = "es";
		else if (button == Russian)
			fucker = "ru";
		
		TranslationServer.SetLocale(fucker);
		Record<Kickstart.Records.SystemInfo> record = new();
		record.Data.Language = fucker;
		record.Save();
	}
}
