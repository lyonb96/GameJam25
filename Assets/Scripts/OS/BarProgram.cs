using UnityEngine;
using UnityEngine.UI; 

public class BarProgram : MonoBehaviour
{

    public TaskBar taskbar;
    public Sprite ImActive;
    public Sprite ImInactive;
    public WindowController window;

    public void SetActive(bool active)
    {
        if (active)
        {
            transform.GetComponent<Image>().sprite = ImActive;
        }
        else
        {
            transform.GetComponent<Image>().sprite = ImInactive;
        }
    }

    public void OnClick()
    {
        window.OnFocus();
    }


    void OnDestroy()
    {
        if (taskbar != null)
        {
            taskbar.RemoveProgram(gameObject);
        }
        Destroy(gameObject);
    }
}
