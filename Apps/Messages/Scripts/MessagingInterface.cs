using Godot;
using System;
using Messaging;

public partial class MessagingInterface : Control {
    public int ConversationIndex = 0;

    public override void _Ready() {
        base._Ready();
        Refresh();
    }

    public void Refresh() {
        // clear previous stuff
        foreach (Node mbcicfda in GetNode("M/H/Messages").GetChildren()) {
            mbcicfda.QueueFree();
        }
        foreach (Node mbcicfda in GetNode("M/Choices").GetChildren()) {
            mbcicfda.QueueFree();
        }

        var conversation = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser).Conversations[ConversationIndex];

        // first make a very cool list of messages
        foreach (var message in conversation.Messages) {
            var h = new Label {
                ThemeTypeVariation = "Header",
                Text = message.Author
            };
            var m = new Label {
                Text = message.Text
            };
            GetNode("M/H/Messages").AddChild(h);
            GetNode("M/H/Messages").AddChild(m);
        }

        // then show the options
        if (conversation.Choices.Count > 0) {
            var buttonGroup = new ButtonGroup();
            foreach (var reply in conversation.Choices) {
                var bhfbghudidfg = new Button {
                    Text = reply.Key,
                    ThemeTypeVariation = "SecondaryButton",
                    ToggleMode = true,
                    SizeFlagsHorizontal = SizeFlags.ExpandFill,
                    SizeFlagsVertical = SizeFlags.ExpandFill,
                    ButtonGroup = buttonGroup,
                };
                GetNode("M/Choices").AddChild(bhfbghudidfg);    
            }
            buttonGroup.Connect("pressed", new Callable(this, nameof(Reply)));
        } else {
            GetNode("M/H/Messages").AddChild(new Label {
                Text = "Conversation has ended."
            });
        }
    }

    public void Reply(Button button) {
        // quite the mouthful
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        MessagingManager.Messages h = socialStuff.Conversations[ConversationIndex].Choices[button.Text];
        MessagingManager.Answer(h);
        Refresh();
    }
}