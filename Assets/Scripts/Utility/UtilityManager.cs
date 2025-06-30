using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Uncomment if using Unity's SceneManager

public class UtilityManager : MonoBehaviour
{


    [Header("UTILITY MANAGER")]
    public static UtilityManager UM;
    public GameObject PostProcessing;


    [HideInInspector] public float SFXVolumeModifier = 1;
    public AudioManager audioManager;

    void Awake()
    {
        if (UtilityManager.UM != null && UtilityManager.UM != this)
        {
            Destroy(UtilityManager.UM.gameObject);
        }
        UM = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PostProcessing = GameObject.Find("PostProcessing");
        audioManager = GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayGlobalClip(Clip clip)
    {
        audioManager.PlayClip(clip);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }

    public void StartGame()
    {
        // Logic to start the game, e.g., loading the main scene
        Debug.Log("Starting game...");
        SceneManager.LoadScene("Desktop"); // Uncomment when using Unity's SceneManager
    }
    
}