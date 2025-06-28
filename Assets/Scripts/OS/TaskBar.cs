using UnityEngine;
using System.Collections.Generic;

public class TaskBar : MonoBehaviour
{
    [Tooltip("The list of program icons on the taskbar. Use AddProgram and RemoveProgram to modify this list at runtime.")]
    [SerializeField]
    private List<GameObject> programs = new List<GameObject>();

    // Positioning constants
    private const float startX = -584f;
    private const float programWidth = 135f;
    private const float spacing = 5;
    private const float yPos = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initial positioning for any programs added in the Inspector.
        RepositionPrograms();
    }

    /// <summary>
    /// Adds a program to the taskbar and repositions all items.
    /// </summary>
    /// <param name="program">The program's GameObject to add.</param>
    public void AddProgram(GameObject program)
    {
        if (program != null && !programs.Contains(program))
        {
            programs.Add(program);
            // Ensure the program is a child of the taskbar for correct positioning.
            program.transform.SetParent(this.transform, false);
            RepositionPrograms();
        }
    }

    /// <summary>
    /// Removes a program from the taskbar and repositions the remaining items.
    /// </summary>
    /// <param name="program">The program's GameObject to remove.</param>
    public void RemoveProgram(GameObject program)
    {
        if (program != null && programs.Remove(program))
        {
            RepositionPrograms();
        }
    }

    /// <summary>
    /// Arranges all program icons on the taskbar according to the layout rules.
    /// </summary>
    private void RepositionPrograms()
    {
        // Remove any null entries from the list first to avoid gaps.
        programs.RemoveAll(item => item == null);

        // Position the remaining valid programs.
        for (int i = 0; i < programs.Count; i++)
        {
            RectTransform rt = programs[i].GetComponent<RectTransform>();
            if (rt != null)
            {
                // Calculate the new X position for the current program.
                float newX = startX + i * (programWidth + spacing);
                rt.anchoredPosition = new Vector2(newX, yPos);
            }
        }
    }
}
