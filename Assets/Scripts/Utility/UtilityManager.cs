using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
    
}