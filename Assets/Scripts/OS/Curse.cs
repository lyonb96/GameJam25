using UnityEngine;

public class Curse : MonoBehaviour
{
    public Texture2D defaultCursor;

    public Texture2D hoverCursor;

    public Texture2D loadCursor;

    public void SetCursor()
    {
        OSManager osManager = OSManager.Instance;
        osManager.cursor = defaultCursor;
        osManager.cursorHover = hoverCursor;
        osManager.cursorLoad = loadCursor;
    }
}
