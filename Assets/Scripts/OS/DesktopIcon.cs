using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesktopIcon : MonoBehaviour
{
    public string Title;

    public Sprite Icon;

    public GameObject WindowContent;

    public WindowSize WindowSize;

    private Image IconImage { get; set; }

    private TextMeshProUGUI DesktopText { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IconImage = GetComponent<Image>();
        IconImage.sprite = Icon;
        DesktopText = GetComponentInChildren<TextMeshProUGUI>();
        DesktopText.text = Title;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        OSManager.Instance.SpawnWindow(new()
        {
            Title = Title,
            Icon = Icon,
            Content = WindowContent,
            Size = WindowSize,
        });
    }
}
