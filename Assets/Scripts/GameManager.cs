using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public BoxCollider2D floor;
    public GameObject magnetSigil;

    [Space(30)]
    public AudioSource area1;
    public AudioSource area2;
    public AudioSource outro;
    [Range(0,1)]
    public float defaultMusicVolume = 0.2f;
    public Slider musicSlider;

    [Space(30)]
    public GameObject outroPanel;

    [Space(30)]
    public AudioSource[] sfxSources;
    public Slider sfxSlider;
    [Range(0, 1)]
    public float defaultSfxVolume = 0.2f;

    [Space(30)]
    public Toggle fullscreenToggle;
    public Toggle windowedToggle;
    public int isFullScreen = 1;

    [Space(30)]
    public PanelGroupBehavior panelGroup;
    public Animator quitPanel;

    public override void Awake()
    {
        base.Awake();
        Pickup.onPickup += OnPickup;

        SetMusicVolume();
        SetSfxVolume();
        SetScreenMode();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log($"Update: {!panelGroup.isAnyPanelOpen()}");
            if (!panelGroup.isAnyPanelOpen())
            {
                quitPanel.SetBool("isOpen",!quitPanel.GetBool("isOpen"));
            }
        }
    }

    public void CloseQuitPanel()
    {
        quitPanel.SetBool("isOpen", false);
    }

    private void SetScreenMode(bool fromUI = false)
    {
        int fullScreenMode = PlayerPrefs.GetInt("isFullScreen", 1);
        if(fullScreenMode == 0)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            if (!fromUI)
            {
                fullscreenToggle.isOn = false;
                windowedToggle.isOn = true;
            }
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            if (!fromUI)
            {
                fullscreenToggle.isOn = true;
                windowedToggle.isOn = false;
            }
        }
    }

    private void SetSfxVolume(bool fromUI = false)
    {
        float volume = PlayerPrefs.GetFloat("sfxVolume", defaultSfxVolume);

        foreach (AudioSource sfx in sfxSources)
        {
            sfx.volume = volume;
        }

        if (!fromUI)
        {
            sfxSlider.value = volume;
        }
    }

    void OnPickup(GameObject pickupObj)
    {
        if(pickupObj == magnetSigil)
        {
            //Unlocking Area 2
            floor.size = new Vector2(2, 1);
            floor.offset = new Vector2(-0.5f, 0f);
        }
    }

    void SetMusicVolume(bool fromUI = false)
    {
        float volume = PlayerPrefs.GetFloat("musicVolume",defaultMusicVolume);      //Read from PlayerPrefs

        area1.volume = volume;
        area2.volume = volume;
        outro.volume = volume;

        if (!fromUI)
        {
            musicSlider.value = volume;
        }
    }

    public void musicSliderChange()
    {
        float value = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", value);
        SetMusicVolume(true);
    }

    public void sfxSliderChange()
    {
        float value = sfxSlider.value;
        PlayerPrefs.SetFloat("sfxVolume", value);
        SetSfxVolume(true);
    }

    public void fullScreenToggleChange()
    {
        bool fullScreen = fullscreenToggle.isOn;
        if (fullScreen)
        {
            PlayerPrefs.SetInt("isFullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isFullScreen", 0);
        }
        SetScreenMode(true);
    }

    public void showOutro()
    {
        area1.Stop();
        area2.Stop();

        outroPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
