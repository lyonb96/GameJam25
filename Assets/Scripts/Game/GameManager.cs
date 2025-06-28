using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CPUControl CPU { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CPU = GetComponentInChildren<CPUControl>();
    }

    // Update is called once per frame
    void Update()
    {
        var pressed = Input.GetMouseButtonDown(0);
        if (pressed)
        {
            CPU.Health -= 1;
        }
    }
}
