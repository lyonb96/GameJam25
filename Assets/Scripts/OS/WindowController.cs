using UnityEngine;
using UnityEngine.EventSystems; // Required for event system interfaces
using UnityEngine.UI; // Required for Button

public class WindowController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("The height of the area at the top that serves as the drag handle.")]
    public float dragHandleHeight = 25f;

    [Header("UI References")]
    [Tooltip("Reference to the close button. Will be auto-fetched if not assigned.")]
    private Button closeButton;
    [Tooltip("Reference to the title bar image. Will be auto-fetched if not assigned.")]
    private Image winBarImage;

    [Header("Focus Visuals")]
    [SerializeField] private Sprite activeWinBarSprite;
    [SerializeField] private Sprite inactiveWinBarSprite;

    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;
    private bool isFocused = false;

    public static event System.Action<WindowController> OnWindowFocused;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Auto-fetch the close button
        if (closeButton == null)
        {
            Transform buttonTransform = transform.Find("CloseButton");
            if (buttonTransform != null)
            {
                closeButton = buttonTransform.GetComponent<Button>();
            }
        }
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseWindow);
        }

        // Auto-fetch the WinBar image
        if (winBarImage == null)
        {
            Transform winBarTransform = transform.Find("WinBar");
            if (winBarTransform != null)
            {
                winBarImage = winBarTransform.GetComponent<Image>();
            }
        }

        OnWindowFocused += HandleWindowFocus;
    }

    private void OnDestroy()
    {
        OnWindowFocused -= HandleWindowFocus;
    }

    private void Start()
    {
        // Make this window focused when it's created.
        OnWindowFocused?.Invoke(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        OnWindowFocused?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            if (localPoint.y > rectTransform.rect.yMax - dragHandleHeight)
            {
                isDragging = true;
                canvasGroup.alpha = 0.9f;
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                // If not in the drag handle, cancel the drag so OnDrag and OnEndDrag are not called.
                eventData.pointerDrag = null;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            ClampToScreen();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            canvasGroup.alpha = 1f; 
            canvasGroup.blocksRaycasts = true; 
        }
    }

    private void CloseWindow()
    {
        Destroy(gameObject);
    }

    private void HandleWindowFocus(WindowController focusedWindow)
    {
        bool hasFocus = (focusedWindow == this);
        if (isFocused == hasFocus) return; // No change in focus state

        isFocused = hasFocus;

        if (winBarImage != null && activeWinBarSprite != null && inactiveWinBarSprite != null)
        {
            winBarImage.sprite = isFocused ? activeWinBarSprite : inactiveWinBarSprite;
        }
    }

    private void ClampToScreen()
    {
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        Rect canvasRect = canvasRectTransform.rect;
        Rect windowRect = rectTransform.rect;

        // Y-axis: Cannot go off-screen.
        float minY = canvasRect.yMin + (windowRect.height * rectTransform.pivot.y);
        float maxY = canvasRect.yMax - (windowRect.height * (1 - rectTransform.pivot.y));

        // X-axis: Can go off-screen by at most half its width.
        float halfWindowWidth = windowRect.width / 2f;
        float minX = canvasRect.xMin - halfWindowWidth + (windowRect.width * rectTransform.pivot.x);
        float maxX = canvasRect.xMax + halfWindowWidth - (windowRect.width * (1 - rectTransform.pivot.x));

        rectTransform.anchoredPosition = new Vector2(
            Mathf.Clamp(anchoredPosition.x, minX, maxX),
            Mathf.Clamp(anchoredPosition.y, minY, maxY)
        );
    }
}