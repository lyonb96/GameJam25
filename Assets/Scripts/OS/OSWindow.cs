using UnityEngine;

public class OSWindow
{
    public string Title { get; set; }

    public Sprite Icon { get; set; }

    public WindowSize Size { get; set; }

    public GameObject Content { get; set; }

    public bool IsBlocking { get; set; }
}

public enum WindowSize
{
    Small,
    Medium,
    Large,
}
