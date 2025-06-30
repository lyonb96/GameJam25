using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesktopIcon : MonoBehaviour
{
    public string Title;

    public Sprite Icon;

    public GameObject WindowContent, createdWindow;

    public WindowSize WindowSize;

    private Image IconImage { get; set; }

    private TextMeshProUGUI DesktopText { get; set; }

    [Tooltip("If this is a text file, the image to display in the window.")]
    public Sprite TextImage;

    void Start()
    {
        IconImage = GetComponent<Image>();
        IconImage.sprite = Icon;
        DesktopText = GetComponentInChildren<TextMeshProUGUI>();
        DesktopText.text = Title;
        if (TextImage != null)
        {
            // WindowContent.transform.Find("Content").GetComponent<Image>().sprite = TextImage;
        }
    }

    void Update()
    {
        // if (createdWindow == null)
        // {
        //     var found = GameObject.Find(WindowContent.name);
        //     if (found != null)
        //     {
        //         createdWindow = found;
        //     }
        // }
        // else
        // {
        //     Debug.Log(createdWindow.transform.position);
        // }
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
