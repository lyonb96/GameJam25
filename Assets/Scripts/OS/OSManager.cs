using System.Collections.Generic;
using TMPro; 
using UnityEngine.UI; 
using UnityEngine;

public class OSManager : MonoBehaviour
{
    public GameObject SmallWindowPrefab;

    public GameObject MediumWindowPrefab;

    public GameObject LargeWindowPrefab;

    public GameObject TaskPrefab;

    public TaskBar taskBar;

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
        GameObject task = Instantiate(TaskPrefab, taskBar.transform);
        task.GetComponent<BarProgram>().taskbar = taskBar;
        if (obj.TryGetComponent<WindowController>(out var windowHandle))
        {
            windowHandle.SetOSWindow(window);
            windowHandle.task = task.GetComponent<BarProgram>();
        }
        task.GetComponentInChildren<TextMeshProUGUI>().text = window.Title;
        task.transform.Find("Image").GetComponent<Image>().sprite = window.Icon;
        taskBar.AddProgram(task);
    }
}
