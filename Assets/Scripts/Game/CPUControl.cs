using TMPro;
using UnityEngine;

public class CPUControl : MonoBehaviour
{
    public int Health { get; set; } = 3;

    public TextMeshProUGUI HealthText { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.SetText(Health.ToString());
    }
}
