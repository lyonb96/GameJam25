using System.Collections.Generic;
using UnityEngine;

public class ChatPreviews : MonoBehaviour
{
    public AxiChatController controller;

    public GameObject PreviewPrefab;

    private List<ChatPreview> previews = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void SpawnChats()
    {
        foreach (var chat in AxiChatController.Chats)
        {
            var previewInstance = Instantiate(PreviewPrefab, transform);
            var previewController = previewInstance.GetComponent<ChatPreview>();
            previewController.controller = controller;
            previewController.chat = chat;
            previews.Add(previewController);
        }
    }

    public void SetFocusedChat(Chat chat)
    {
        foreach (var preview in previews)
        {
            preview.SetFocused(preview.chat == chat);
        }
    }
}
