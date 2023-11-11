using System;
using System.Collections.Generic;
using System.Linq;

namespace Messaging;

public partial class PassionfruitSupportConversation
{
    public static void HelpApps()
    {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "I need help with lelcubeOS Me apps"
        }).ToArray();
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "Have you tried restarting your device?"
        }).ToArray();
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>(){
            {"Yes", MessagingManager.Messages.PassionfruitSupportYes},
            {"No", MessagingManager.Messages.PassionfruitSupportNo}
        };
        SavingManager.Save(SavingManager.CurrentUser, socialStuff);
    }

    public static void HelpDevice()
    {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "I need help with my device"
        }).ToArray();
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "Have you tried smashing your device with a hammer?"
        }).ToArray();
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>(){
            {"Yes", MessagingManager.Messages.PassionfruitSupportYes},
            {"No", MessagingManager.Messages.PassionfruitSupportNo}
        };
        SavingManager.Save(SavingManager.CurrentUser, socialStuff);
    }

    public static void Refund()
    {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "I want a refund for my device"
        }).ToArray();
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "No."
        }).ToArray();
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>();
        SavingManager.Save(SavingManager.CurrentUser, socialStuff);
    }

    public static void Lawsuit()
    {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "I am going to sue you"
        }).ToArray();
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "I invoke the fifth"
        }).ToArray();
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>();
        SavingManager.Save(SavingManager.CurrentUser, socialStuff);
    }

    public static void Yes()
    {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "Yes"
        }).ToArray();
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "Oh ok."
        }).ToArray();
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>();
        SavingManager.Save(SavingManager.CurrentUser, socialStuff);
    }

    public static void No()
    {
        var socialStuff = SavingManager.Load<SocialStuff>(SavingManager.CurrentUser);
        int h = Array.FindIndex(socialStuff.Conversations, person => person.Name == "Passionfruit Support");
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "You",
            Text = "No"
        }).ToArray();
        socialStuff.Conversations[h].Messages = socialStuff.Conversations[h].Messages.Append(new Message {
            Author = "Passionfruit Support",
            Text = "Then do it NOW!"
        }).ToArray();
        socialStuff.Conversations[h].Choices = new Dictionary<string, MessagingManager.Messages>();
        SavingManager.Save(SavingManager.CurrentUser, socialStuff);
    }
}
