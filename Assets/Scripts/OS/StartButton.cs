using UnityEngine;
using UnityEngine.UI; 

public class StartButton : MonoBehaviour
{
    public GameObject startmenu;
    public Image btn;

    public Sprite active;
    public Sprite inactive;

    public void ToggleStart()
    {
        if (startmenu.active == true)
        {
            startmenu.active = false;
            btn.sprite = inactive;
        }
        else
        {
            startmenu.active = true;
            btn.sprite = active;
        }
    }
}
