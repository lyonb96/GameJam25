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

    public Sprite InfoIcon;

    public Sprite CommandLineIcon;

    public GameObject ErrorPrefab;

    public GameObject TaskPrefab;

    public GameObject BlockerPrefab;

    public GameObject CommandPromptPrefab;

    public GameObject ChatNotificationPrefab;

    public TaskBar taskBar;

    public static OSManager Instance { get; private set; }

    private List<OSWindow> Windows { get; set; } = new();

    public Texture2D cursor;
    public Texture2D cursorHover;
    public Texture2D cursorLoad;
    private bool hovering;
    private bool loading;
    public Vector2 hotspot = Vector2.zero;

    public AudioSource audioSource;
    public AudioClip[] keyStrokes;
    public AudioClip[] spaceStrokes;
    public AudioClip[] mouseClicks;
    public AudioClip ErrorTone;

    void Start()
    {
        Instance = this;
        SetDefaultCursor();
    }

    void Update()
    {
        if (loading)
        {
            SetLoadingCursor();
        }
        else if (hovering)
        {
            SetHoverCursor();
        }
        else
        {
            SetDefaultCursor();
        }
    }

    public void SpawnWindow(OSWindow window)
    {
        StartCoroutine(SpawnWindowRoutine(window, window.IsBlocking));
    }

    private IEnumerator SpawnWindowRoutine(OSWindow window, bool fast = false)
    {
        loading = true;
        if (!fast)
        {
            var delay = UnityEngine.Random.Range(0.25F, 0.75F);
            if (UnityEngine.Random.Range(0.0F, 1.0F) < 0.05F)
            {
                delay *= 10.0F;
            }
            yield return new WaitForSeconds(delay);
        }
        loading = false;
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
                WindowSize.Small => new Vector2(650, 260),
                WindowSize.Folder => new Vector2(800, 600),
                WindowSize.Medium => new Vector2(1000, 720),
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

    public void OpenCommandPrompt(string terminateCommand = null, GameObject lifespanWatcher = null, Action onClose = null)
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
            LifespanWatcher = lifespanWatcher,
            OnContentCreated = (content) =>
            {
                var commandPrompt = content.GetComponent<CommandPrompt>();
                commandPrompt.TerminateCommand = terminateCommand;
            },
        });
    }

    public void AddError(string message, bool blocking = true, Action onClose = null)
    {
        audioSource.clip = ErrorTone;
        audioSource.Play();
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

    public void AddWarning(string message, bool blocking = true, Action onClose = null)
    {
        var error = Instantiate(ErrorPrefab);
        var text = error.GetComponentInChildren<TextMeshProUGUI>();
        text.text = message;
        var image = error.GetComponentInChildren<Image>();
        image.sprite = WarningIcon;
        SpawnWindow(new()
        {
            Size = WindowSize.Small,
            Icon = WarningIcon,
            Title = "Warning",
            Content = error,
            IsBlocking = blocking,
            OnClose = onClose,
        });
    }

    public void AddInfo(string message, string title = null, bool blocking = false, Action onClose = null)
    {
        var error = Instantiate(ErrorPrefab);
        var text = error.GetComponentInChildren<TextMeshProUGUI>();
        text.text = message;
        var image = error.GetComponentInChildren<Image>();
        image.sprite = InfoIcon;
        SpawnWindow(new()
        {
            Size = WindowSize.Small,
            Icon = InfoIcon,
            Title = title ?? "Info",
            Content = error,
            IsBlocking = blocking,
            OnClose = onClose,
        });
    }

    public void ShowChatNotification()
    {
        var notif = Instantiate(ChatNotificationPrefab, transform);
    }

    public void SetHovering(bool hovering)
    {
        this.hovering = hovering;
    }

    public void SetLoading(bool loading)
    {
        this.loading = loading;
    }

    private void SetDefaultCursor()
    {
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
    }

    public void SetHoverCursor()
    {
        Cursor.SetCursor(cursorHover, hotspot, CursorMode.Auto);
    }

    public void SetLoadingCursor()
    {
        Cursor.SetCursor(cursorLoad, hotspot, CursorMode.Auto);
    }

    void OnGUI()
    {
        Event e = Event.current;

        // Handle mouse clicks for audio feedback
        if (e.type == EventType.MouseDown && (e.button == 0 || e.button == 1))
        {
            if (mouseClicks != null && mouseClicks.Length > 0)
            {
                audioSource.clip = mouseClicks[UnityEngine.Random.Range(0, mouseClicks.Length)];
                audioSource.Play();
            }
        }
        // Handle key presses for audio feedback
        else if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.None) return; // Do not play sound for modifier keys like Shift, Ctrl, etc.

            if (e.keyCode == KeyCode.Space)
            {
                if (spaceStrokes != null && spaceStrokes.Length > 0)
                {
                    audioSource.clip = spaceStrokes[UnityEngine.Random.Range(0, spaceStrokes.Length)];
                    audioSource.Play();
                }
            }
            else if (keyStrokes != null && keyStrokes.Length > 0)
            {
                audioSource.clip = keyStrokes[UnityEngine.Random.Range(0, keyStrokes.Length)];
                audioSource.Play();
            }
        }
    }
}
