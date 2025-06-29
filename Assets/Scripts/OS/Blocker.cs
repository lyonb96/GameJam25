using UnityEngine;

public class Blocker : MonoBehaviour
{
    public AudioSource AudioSource;

    public void OnClick()
    {
        AudioSource.Play();
    }
}
