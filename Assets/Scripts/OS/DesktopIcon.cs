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

    [Tooltip("If this is a text file, the image to display in the window.")]
    public Sprite TextImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IconImage = GetComponent<Image>();
        IconImage.sprite = Icon;
        DesktopText = GetComponentInChildren<TextMeshProUGUI>();
        DesktopText.text = Title;
        if (TextImage != null)
        {
            // Get the child content
            WindowContent.transform.GetChild(1).GetComponent<Image>().sprite = TextImage;
        }
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
