using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change to hover cursor when mouse enters the UI element
        OSManager.Instance.SetHovering(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert to default cursor when mouse exits
        OSManager.Instance.SetHovering(false);
    }

    public void OnDestroy()
    {
        OSManager.Instance.SetHovering(false);
    }

    public void OnDisable()
    {
        OSManager.Instance.SetHovering(false);
    }

}