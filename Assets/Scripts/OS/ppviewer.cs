using UnityEngine;
using TMPro;

public class ppviewer : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = NarrativeScript.Instance.pp.ToString();
    }
}
