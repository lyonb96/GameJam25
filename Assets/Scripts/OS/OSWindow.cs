using System;
using UnityEngine;

public class OSWindow
{
    public string Title { get; set; }

    public Sprite Icon { get; set; }

    public WindowSize Size { get; set; }

    public GameObject Content { get; set; }

    public bool IsBlocking { get; set; }

    public Action OnClose { get; set; }

    public bool AllowCloseButton { get; set; } = true;

    public GameObject LifespanWatcher { get; set; }

    public Action<GameObject> OnContentCreated { get; set; }
}

public enum WindowSize
{
    Small,
    Medium,
    Large,
    Special,
}
