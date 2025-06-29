using UnityEngine;

public class ChatArea : MonoBehaviour
{
    private Chat chat;

    public GameObject MessagePrefab;

    public void SetChat(Chat chat)
    {
        this.chat = chat;
        transform.DetachChildren();
        foreach (var message in chat.Messages)
        {
            var messageObject = Instantiate(MessagePrefab, transform);
            if (messageObject.TryGetComponent<ChatEntry>(out var entry))
            {
                entry.SetMessage(message);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
