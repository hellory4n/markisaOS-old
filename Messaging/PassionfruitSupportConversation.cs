using System;
using System.Collections.Generic;
using System.Linq;

public class PassionfruitSupportConversation {
    public static void HelpApps() {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "I need help with lelcubeOS Me apps"
        });
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "Have you tried restarting your device?"
        });
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>(){
            {"Yes", MessagingManager.Messages.PassionfruitSupportYes},
            {"No", MessagingManager.Messages.PassionfruitSupportNo}
        };
    }

    public static void HelpDevice() {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "I need help with my device"
        });
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "Have you tried smashing your device with a hammer?"
        });
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>(){
            {"Yes", MessagingManager.Messages.PassionfruitSupportYes},
            {"No", MessagingManager.Messages.PassionfruitSupportNo}
        };
    }

    public static void Refund() {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "I want a refund for my device"
        });
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "No."
        });
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>();
    }

    public static void Lawsuit() {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "I am going to sue you"
        });
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "I invoke the fifth"
        });
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>();
    }

    public static void Yes() {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "Yes"
        });
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "Oh ok."
        });
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>();
    }

    public static void No() {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "No"
        });
        socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "Then do it NOW!"
        });
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>();
    }
}
