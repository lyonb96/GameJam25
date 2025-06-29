using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private OSManager osManager;

    void Start()
    {
        // Fetch the OSManager instance from the scene
        osManager = FindAnyObjectByType<OSManager>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change to hover cursor when mouse enters the UI element
        osManager?.SetHoverCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert to default cursor when mouse exits
        osManager?.SetDefaultCursor();
    }

    public void OnDestroy()
    {
        osManager?.SetDefaultCursor();
    }
}