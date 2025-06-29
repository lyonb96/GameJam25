using UnityEngine;
using System.Collections.Generic; // Required for using Lists

[System.Serializable]
public class ChatMessage
{
    public string sender;
    public string message;
}

public class AxiChat : MonoBehaviour
{
    [Tooltip("The list of chat messages to display.")]
    public List<ChatMessage> messages = new List<ChatMessage>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
