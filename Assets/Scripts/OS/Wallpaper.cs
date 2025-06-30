using UnityEngine;

public class Wallpaper: MonoBehaviour
{
    Material wallpaperMat;
    [SerializeField] Texture2D[] wallpapers;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWallpaper(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWallpaper(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWallpaper(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetWallpaper(3);
        }
    }
    
    public void SetWallpaper(int index)
    {
        wallpaperMat = GetComponent<Renderer>().material;
        wallpaperMat.SetTexture("_MainTex", wallpapers[index]);
    }

}
