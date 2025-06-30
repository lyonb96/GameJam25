using UnityEngine;
using UnityEngine.UI;

public class bgset : MonoBehaviour
{
    [Tooltip("The sprite to set as the new background.")]
    public Sprite backgroundSprite;

    private Image backgroundPanel;

    public void ApplyBackground()
    {
        Debug.Log("Applying background");
        GameObject backgroundObject = GameObject.Find("Background");
    
        backgroundPanel = backgroundObject.GetComponent<Image>();
        backgroundPanel.sprite = backgroundSprite;
        backgroundPanel.color = Color.white;
    }
}
