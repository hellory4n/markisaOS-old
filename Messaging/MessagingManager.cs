using Godot;
using System;

public class MessagingManager : Node {
    // haha yes
    public enum Messages {
        PassionfruitSupportApps,
        PassionfruitSupportDevice,
        PassionfruitSupportRefund,
        PassionfruitSupportLawsuit,
        PassionfruitSupportYes,
        PassionfruitSupportNo
    }

    public static void Answer(Messages message) {
        switch (message) {
            case Messages.PassionfruitSupportApps:
                PassionfruitSupportConversation.HelpApps();
                break;
            case Messages.PassionfruitSupportDevice:
                PassionfruitSupportConversation.HelpDevice();
                break;
            case Messages.PassionfruitSupportRefund:
                PassionfruitSupportConversation.Refund();
                break;
            case Messages.PassionfruitSupportLawsuit:
                PassionfruitSupportConversation.Lawsuit();
                break;
            case Messages.PassionfruitSupportYes:
                PassionfruitSupportConversation.Yes();
                break;
            case Messages.PassionfruitSupportNo:
                PassionfruitSupportConversation.No();
                break;
        }
    }
}
