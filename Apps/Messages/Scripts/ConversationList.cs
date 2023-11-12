using Godot;
using System;
using Lelsktop.Toolkit;

public partial class ConversationList : VBoxContainer {
    // quite the mouthful
    readonly PackedScene MessageThingy = GD.Load<PackedScene>("res://Apps/Messages/ConversationOpen.tscn");
    readonly PackedScene Shit = GD.Load<PackedScene>("res://Apps/Messages/MessagingInterface.tscn");

    public override void _Ready() {
        base._Ready();
        Refresh();
    }

    public void Refresh() {
        // clear previous list
        foreach (Node mbcicfda in GetChildren()) {
            mbcicfda.QueueFree();
        }

        // yeah
        Conversation[] conversations = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser)
            .Conversations;
        
        // indeed
        int i = 0;
        foreach (var conversation in conversations) {
            var h = MessageThingy.Instantiate<SidebarButton>();
            h.Icon = GD.Load<Texture2D>(conversation.Icon);
            h.Text = conversation.Name;
            var bullshit = Shit.Instantiate<MessagingInterface>();
            // the sidebar button thing requires the content's name to start with "Category" :)))
            bullshit.Name = $"Category{LelfsManager.GenerateID()}";
            bullshit.Visible = false;
            bullshit.Theme = null;
            bullshit.ConversationIndex = i;
            // quite the mouthful
            GetParent().GetParent().GetParent().GetNode<Control>("Content").AddChild(bullshit);
            h.Category = bullshit;
            AddChild(h);
            i++;
        }
    }
}
