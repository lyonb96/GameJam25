using UnityEngine;

public static class OSHelpers
{
    /// <summary>
    /// Destroys the window a game object resides in. Useful for killing a window from inside its content
    /// </summary>
    /// <param name="obj"></param>
    public static void DestroyWindow(GameObject obj)
    {
        var window = obj.GetComponentInParent<WindowController>();
        window.CloseWindow();
    }
}