using System.Collections.Generic;
using TMPro; 
using UnityEngine.UI; 
using UnityEngine;
using System;
using System.Collections;

public class OSManager : MonoBehaviour
{
    public GameObject LargeWindowPrefab;

    public Sprite ErrorIcon;

    public Sprite WarningIcon;

    public Sprite CommandLineIcon;

    public GameObject ErrorPrefab;

    public GameObject TaskPrefab;

    public GameObject BlockerPrefab;

    public GameObject CommandPromptPrefab;

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
        StartCoroutine(SpawnWindowRoutine(window));
    }

    private IEnumerator SpawnWindowRoutine(OSWindow window)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5F, 1.5F));
        Windows.Add(window);
        GameObject blocker = null;
        if (window.IsBlocking)
        {
            blocker = Instantiate(BlockerPrefab, transform);
        }
        var obj = Instantiate(LargeWindowPrefab, transform.position, Quaternion.identity, transform);
        if (obj.TryGetComponent<RectTransform>(out var tf))
        {
            var windowSize = window.Size switch
            {
                WindowSize.Special => new Vector2(800, 400),
                WindowSize.Small => new Vector2(400, 200),
                WindowSize.Medium => new Vector2(800, 600),
                _ => new Vector2(1250, 900),
            };
            tf.sizeDelta = windowSize;
        }
        GameObject task = Instantiate(TaskPrefab, taskBar.transform);
        task.GetComponent<BarProgram>().taskbar = taskBar;
        if (obj.TryGetComponent<WindowController>(out var windowHandle))
        {
            windowHandle.SetOSWindow(window);
            windowHandle.blocker = blocker;
            windowHandle.task = task.GetComponent<BarProgram>();
            task.GetComponent<BarProgram>().window = windowHandle;
        }
        task.GetComponentInChildren<TextMeshProUGUI>().text = window.Title;
        task.transform.Find("Image").GetComponent<Image>().sprite = window.Icon;
        taskBar.AddProgram(task);
    }

    public void OpenCommandPrompt(Action onClose = null)
    {
        SpawnWindow(new()
        {
            Size = WindowSize.Special,
            Icon = CommandLineIcon,
            Title = "Console",
            Content = CommandPromptPrefab,
            IsBlocking = true,
            AllowCloseButton = false,
            OnClose = onClose,
        });
    }

    public void AddError(string message, bool blocking = true, Action onClose = null)
    {
        var error = Instantiate(ErrorPrefab);
        var text = error.GetComponentInChildren<TextMeshProUGUI>();
        text.text = message;
        var image = error.GetComponentInChildren<Image>();
        image.sprite = ErrorIcon;
        SpawnWindow(new()
        {
            Size = WindowSize.Small,
            Icon = ErrorIcon,
            Title = "Error",
            Content = error,
            IsBlocking = blocking,
            OnClose = onClose,
        });
    }
}
