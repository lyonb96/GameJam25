using UnityEngine;
using System.Collections.Generic;

public class TaskBar : MonoBehaviour
{
    [Tooltip("The list of program icons on the taskbar. Use AddProgram and RemoveProgram to modify this list at runtime.")]
    [SerializeField]
    private List<GameObject> programs = new List<GameObject>();

    // Positioning constants
    private const float startX = -600;
    private const float programWidth = 135f;
    private const float spacing = 5;
    private const float yPos = 1f;

    void Start()
    {
        RepositionPrograms();
    }

    public void AddProgram(GameObject program)
    {
        if (program != null && !programs.Contains(program))
        {
            programs.Add(program);
            program.transform.SetParent(this.transform, false);
            RepositionPrograms();
        }
    }

    public void RemoveProgram(GameObject program)
    {
        if (program != null && programs.Remove(program))
        {
            RepositionPrograms();
        }
    }

    private void RepositionPrograms()
    {
        programs.RemoveAll(item => item == null);

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
