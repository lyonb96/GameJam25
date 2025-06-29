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
            Destroy(gameObject);
        }
        else
        {
            Instantiate(ResponsePrefab, transform);
            SpawnNewLine();
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
