using UnityEngine;

public class Gabag : MonoBehaviour
{
    public string Title = "AxiChat";

    public Sprite Icon;

    public WindowSize WindowSize = WindowSize.Medium;

    public GameObject axiChatContentPrefab;

    public void OpenAxiChatWindow()
    {
        if (axiChatContentPrefab == null)
        {
            Debug.LogError("AxiChat content prefab is not assigned in the Gabag script.", this);
            return;
        }

        // Fetch the OSManager instance and spawn the window
        OSManager.Instance.SpawnWindow(new()
        {
            Title = this.Title,
            Icon = this.Icon,
            Content = axiChatContentPrefab,
            Size = this.WindowSize,
        });
    }
}
