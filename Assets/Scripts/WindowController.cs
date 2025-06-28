using UnityEngine;
using UnityEngine.EventSystems; // Required for event system interfaces
using UnityEngine.UI; // Required for Button

public class WindowController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("The height of the area at the top that serves as the drag handle.")]
    public float dragHandleHeight = 20f;

    [Tooltip("Reference to the close button. Will be auto-fetched if not assigned.")]
    [SerializeField] private Button closeButton;

    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private CanvasGroup canvasGroup;
    private bool isDragging = false; // Flag to check if a valid drag is in progress


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        // Find the root Canvas to handle scaling correctly
        canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Auto-fetch the close button if not assigned in the inspector
        if (closeButton == null)
        {
            Transform buttonTransform = transform.Find("CloseButton");
            if (buttonTransform != null)
            {
                closeButton = buttonTransform.GetComponent<Button>();
            }
        }

        // Add a listener to the close button if it exists
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseWindow);
        }
    }

    // Called when the drag begins
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = false;

        // Convert the mouse's screen position to the local position within the RectTransform
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            // Check if the click was in the top 'dragHandleHeight' area.
            // rect.height gives the total height of the element.
            // localPoint.y is relative to the pivot, so the top edge is at rect.yMax.
            if (localPoint.y > rectTransform.rect.yMax - dragHandleHeight)
            {
                isDragging = true;
                canvasGroup.alpha = 0.6f;
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                // If not in the drag handle, cancel the drag so OnDrag and OnEndDrag are not called.
                eventData.pointerDrag = null;
            }
        }
    }

    // Called every frame while the object is being dragged
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Update the position of the RectTransform.
            // We use eventData.delta / canvas.scaleFactor to make the movement
            // independent of the canvas's scale mode.
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            ClampToScreen();
        }
    }

    // Called when the drag ends
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            canvasGroup.alpha = 1f; // Reset transparency
            canvasGroup.blocksRaycasts = true; // Re-enable raycast blocking
        }
    }

    // Destroys the window GameObject when called
    private void CloseWindow()
    {
        Destroy(gameObject);
    }

    // Clamps the window to the screen boundaries based on the defined rules.
    private void ClampToScreen()
    {
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        // Get canvas and window dimensions
        Rect canvasRect = canvasRectTransform.rect;
        Rect windowRect = rectTransform.rect;

        // Calculate the min and max positions for the window's pivot.
        // This takes the window's pivot point into account for accurate boundary detection.

        // Y-axis: Cannot go off-screen.
        float minY = canvasRect.yMin + (windowRect.height * rectTransform.pivot.y);
        float maxY = canvasRect.yMax - (windowRect.height * (1 - rectTransform.pivot.y));

        // X-axis: Can go off-screen by at most half its width.
        float halfWindowWidth = windowRect.width / 2f;
        float minX = canvasRect.xMin - halfWindowWidth + (windowRect.width * rectTransform.pivot.x);
        float maxX = canvasRect.xMax + halfWindowWidth - (windowRect.width * (1 - rectTransform.pivot.x));

        // Apply the clamped position.
        rectTransform.anchoredPosition = new Vector2(
            Mathf.Clamp(anchoredPosition.x, minX, maxX),
            Mathf.Clamp(anchoredPosition.y, minY, maxY)
        );
    }
}