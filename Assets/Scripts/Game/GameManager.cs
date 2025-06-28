using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CPUControl CPU { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CPU = GetComponentInChildren<CPUControl>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
