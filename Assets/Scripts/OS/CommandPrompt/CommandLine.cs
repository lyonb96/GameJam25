using TMPro;
using UnityEngine;

public class CommandLine : MonoBehaviour
{
    public TMP_InputField Input;

    private CommandPrompt prompt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prompt = GetComponentInParent<CommandPrompt>();
        Input.onSubmit.AddListener(Submit);
        Input.ActivateInputField();
    }

    public void Submit(string text)
    {
        prompt.Submit(text);
    }

    public void Disable()
    {
        Input.interactable = false;
    }
}
