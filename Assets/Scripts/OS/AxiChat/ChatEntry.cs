using TMPro;
using UnityEngine;

public class ChatEntry : MonoBehaviour
{
    public TextMeshProUGUI senderText;
    public TextMeshProUGUI messageText;

    public void SetMessage(AxiChatMessage message)
    {
        senderText.text = message.Sender + ":";
        senderText.color = message.Sender == "You"
            ? new Color(156.0F / 255.0F, 33.0F / 255.0F, 85.0F / 255.0F)
            : new Color(2.0F / 255.0F, 0.0F, 127.0F / 255.0F);
        messageText.text = message.Message;
    }
}
