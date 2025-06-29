using TMPro;
using UnityEngine;

public class CommandPrompt : MonoBehaviour
{
    public GameObject CommandLinePrefab;

    public GameObject ResponsePrefab;

    private CommandLine lastCommandLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var lineObject = Instantiate(CommandLinePrefab, transform);
        lastCommandLine = lineObject.GetComponent<CommandLine>();
    }

    public void Submit(string text)
    {
        if (text.ToLower() == "killtask")
        {
            OSHelpers.DestroyWindow(gameObject);
        }
        else
        {
            SpawnResponse("ERROR unrecognized command: " + text, true);
            SpawnNewLine();
        }
    }

    private void SpawnResponse(string res, bool isError)
    {
        var responseObject = Instantiate(ResponsePrefab, transform);
        if (responseObject.TryGetComponent<TextMeshProUGUI>(out var resText))
        {
            resText.text = res;
            resText.color = isError
                ? Color.darkRed
                : Color.white;
        }
    }

    private void SpawnNewLine()
    {
        if (lastCommandLine != null)
        {
            lastCommandLine.Disable();
        }
        var lineObject = Instantiate(CommandLinePrefab, transform);
        lastCommandLine = lineObject.GetComponent<CommandLine>();
    }
}
