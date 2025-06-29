using TMPro;
using UnityEngine;

public class CommandPrompt : MonoBehaviour
{
    public GameObject CommandLinePrefab;

    public GameObject ResponsePrefab;

    private CommandLine lastCommandLine;

    public string TerminateCommand { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var lineObject = Instantiate(CommandLinePrefab, transform);
        lastCommandLine = lineObject.GetComponent<CommandLine>();
    }

    public void Submit(string text)
    {
        if (text.ToLower() == TerminateCommand)
        {
            OSHelpers.DestroyWindow(gameObject);
        }
        else if (TerminateCommand != null && TerminateCommand.StartsWith("sudo") && TerminateCommand.EndsWith(text.ToLower()))
        {
            SpawnResponse("ERROR unauthorized. Try using the 'sudo' option", true);
            SpawnNewLine();
        }
        else
        {
            // TODO: Normal command handlers
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
