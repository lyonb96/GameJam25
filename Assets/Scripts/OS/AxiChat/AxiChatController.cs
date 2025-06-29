using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AxiChatController : MonoBehaviour
{
    private ChatPreviews chatPreviews;

    private ChatArea chatArea;

    public List<Chat> Chats { get; private set; } = new()
    {
        new()
        {
            Person = "Jim's Mom",
            Messages = new()
            {
                new()
                {
                    Sender = "Jim's Mom",
                    Message = "You're so much cooler than my son (the loser)",
                },
                new()
                {
                    Sender = "You",
                    Message = "You're god damn right.",
                },
                new()
                {
                    Sender = "Jim's Mom",
                    Message = "Saucoma",
                }
            },
        },
        new()
        {
            Person = "Jim (loser)",
            Messages = new()
            {
                new()
                {
                    Sender = "Jim (loser)",
                    Message = "You haven't been sleeping with my mom, have you?",
                },
            },
        },
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chatPreviews = GetComponentInChildren<ChatPreviews>();
        chatPreviews.controller = this;
        chatPreviews.SpawnChats();
        chatArea = GetComponentInChildren<ChatArea>();
        SelectChat(Chats.First());
    }

    public void SelectChat(Chat chat)
    {
        chatPreviews.SetFocusedChat(chat);
        chatArea.SetChat(chat);
    }
}
