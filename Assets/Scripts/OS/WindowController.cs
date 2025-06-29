using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.UI; 
using TMPro; // Required for TextMeshPro
using System.Linq;

public class WindowController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("The height of the area at the top that serves as the drag handle.")]
    public float dragHandleHeight = 25f;

    // Component references
    private Button closeButton;
    private Image icon;
    private TextMeshProUGUI title;
    private GameObject panel;

    // Transform and Canvas references
    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;

    public OSWindow window;

    public void SetOSWindow(OSWindow window)
    {
        this.window = window;
        title.SetText(window.Title);
        icon.sprite = window.Icon;
        // TODO: instantiate window content
        var windowContent = Instantiate(window.Content, panel.transform);
        if (windowContent.TryGetComponent<RectTransform>(out var tf))
        {
            tf.anchorMin = Vector2.zero;
            tf.anchorMax = Vector2.one;
            tf.sizeDelta = Vector2.zero;
            tf.pivot = Vector2.one / 2.0F;
            tf.position = Vector3.zero;
            tf.localScale = Vector3.one;
            tf.offsetMax = Vector3.zero;
            tf.offsetMin = Vector3.zero;
        }
    }

    private void Awake()
    {
        title = GetComponentInChildren<TextMeshProUGUI>();
        icon = GetComponentsInChildren<Image>()
            .First(i => i.name == "Icon");
        panel = GetComponentsInChildren<CanvasRenderer>()
            .First(i => i.name == "Panel")
            .gameObject;

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Auto-fetch UI elements from children
        closeButton = GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(CloseWindow);

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

    public void OnFocus()
    {
        // OSManager.Instance.FocusWindow(this);
        transform.SetAsLastSibling();
    }

    private void CloseWindow()
    {
        Destroy(gameObject);
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
