using UnityEngine;
using System.Collections.Generic;

public class TaskBar : MonoBehaviour
{
    [Tooltip("The list of program icons on the taskbar. Use AddProgram and RemoveProgram to modify this list at runtime.")]
    [SerializeField]
    private List<GameObject> programs = new List<GameObject>();

    private GameObject panel;

    // Positioning constants
    private const float startX = 0;
    private const float programWidth = 135f;
    private const float spacing = 5;
    private const float yPos = 0f;

    void Start()
    {
        panel = transform.Find("Panel").gameObject;
        RepositionPrograms();
    }

    public void AddProgram(GameObject program)
    {
        if (program != null && !programs.Contains(program))
        {
            programs.Add(program);
            program.transform.SetParent(panel.transform, false);
            if (program.TryGetComponent<RectTransform>(out var tf))
            {
                var pivot = new Vector2(0.0F, 0.5F);
                tf.anchorMin = pivot;
                tf.anchorMax = pivot;
                tf.pivot = pivot;
            }
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
