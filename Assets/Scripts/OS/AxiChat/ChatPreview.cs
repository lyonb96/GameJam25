using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatPreview : MonoBehaviour
{
    public AxiChatController controller;

    public Chat chat;

    public TextMeshProUGUI senderText;
    public TextMeshProUGUI previewText;
    public Image bg;

    void Start()
    {
        senderText.text = chat.Person;
        previewText.text = chat.Messages.LastOrDefault()?.Message;
        if (previewText.text.Length > 60)
        {
            previewText.text = previewText.text[..57] + "...";
        }
    }

    public void OnClick()
    {
        // focus
        controller.SelectChat(chat);
    }

    public void SetFocused(bool focused)
    {
        if (focused)
        {
            senderText.color = Color.white;
            previewText.color = new Color(195.0F / 255.0F, 195.0F / 255.0F, 195.0F / 255.0F);
            bg.color = new Color(2.0F / 255.0F, 0.0F, 127.0F / 255.0F);
        }
        else
        {
            senderText.color = Color.black;
            previewText.color = new Color(142.0F / 255.0F, 142.0F / 255.0F, 142.0F / 255.0F);
            bg.color = Color.white;
        }
    }
}
