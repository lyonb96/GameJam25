using System.Collections.Generic;
using UnityEngine;

public class OSManager : MonoBehaviour
{
    public GameObject SmallWindowPrefab;

    public GameObject MediumWindowPrefab;

    public GameObject LargeWindowPrefab;

    public static OSManager Instance { get; private set; }

    private List<OSWindow> Windows { get; set; } = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnWindow(OSWindow window)
    {
        Windows.Add(window);
        var windowPrefab = window.Size switch
        {
            WindowSize.Small => SmallWindowPrefab,
            WindowSize.Medium => MediumWindowPrefab,
            WindowSize.Large => LargeWindowPrefab,
            _ => SmallWindowPrefab,
        };
        var obj = Instantiate(windowPrefab, transform.position, Quaternion.identity, transform);
        if (obj.TryGetComponent<WindowController>(out var windowHandle))
        {
            windowHandle.SetOSWindow(window);
        }
        // TODO: Maybe taskbar stuff if necessary
    }
}
