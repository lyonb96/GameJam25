using UnityEngine;
using UnityEngine.Events;

public class MouseHover : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onMouseHoverEnter;
    public UnityEvent onMouseHoverExit;

    private void OnMouseEnter()
    {
        Debug.Log("Mouse Hover Entered");
        onMouseHoverEnter?.Invoke();
    }

    private void OnMouseExit()
    {
        onMouseHoverExit?.Invoke();
    }
}
