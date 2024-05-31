using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Rendering;

public class MainUIManager : MonoBehaviour
{
    public GameObject HUDPanel;
    public GameObject pausePanel;

    public AudioMixer audioMixer;

    public TextMeshProUGUI killsText;
    public TextMeshProUGUI lifesText;
    public Slider globalSlider;
    public Slider SFxSlider;
    public Slider MusicSlider;
    public Slider AmbientSlider;

    private int kills;
    private int lifes;
    private float globalVolume;
    private float musicVolume;
    private float SFxVolume;
    private float ambientVolume;

    public Sprite[] lifeTypes;
    public Image lifesImage;
    public Animator uiAnimator;
    private float lifesParameter;

    public string fileName;
    private AudioSettings audioSettings;
    void Start()
    {
        ShowHUD();
        kills = 0;
        lifes = 4;
        lifesParameter = lifes / 4;
        audioSettings = new AudioSettings();
        //lifesImage.sprite = lifeTypes[0];
        //GetVolumes();
        LoadAudioSettings();
    }

    public void CleanPanel()
    {
        HUDPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void ShowHUD()
    {
        CleanPanel();
        HUDPanel.SetActive(true);
        Time.timeScale = 1.0f;
    }
    public void ShowPause()
    {
        CleanPanel();
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1.0f;
        //Application.Quit();
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            Kills();
        if (Input.GetKeyDown(KeyCode.L))
            Damage();


        killsText.text = "Kills x" + kills;
        lifesText.text = "Lifes x" + lifes;
        lifesParameter = lifes / 4f;
        uiAnimator.SetFloat("Lifes", lifesParameter);
    }

    void Damage()
    {
        lifes--;
    }
    void Kills()
    {
        kills++;
    }

    public void SetGlobalVolume(float volume)
    {
        globalVolume = volume;
        audioMixer.SetFloat("GlobalVolume", globalVolume);
    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        audioMixer.SetFloat("MusicVolume", musicVolume);
        SaveAudioSettings();
    }
    public void SetAmbientVolume(float volume)
    {
        ambientVolume = volume;
        audioMixer.SetFloat("AmbientVolume", ambientVolume);
        SaveAudioSettings();
    }
    public void SetSFxVolume(float volume)
    {
        SFxVolume = volume;
        audioMixer.SetFloat("SFxVolume", SFxVolume);
        SaveAudioSettings();
    }

    public void GetVolumes()
    {
        audioMixer.GetFloat("GlobalVolume", out globalVolume);
        audioMixer.GetFloat("MusicVolume", out musicVolume);
        audioMixer.GetFloat("AmbientVolume", out ambientVolume);
        audioMixer.GetFloat("SFxVolume", out SFxVolume);

        globalSlider.value = globalVolume;
        MusicSlider.value = musicVolume;
        AmbientSlider.value = ambientVolume;
        SFxSlider.value = SFxVolume;
    }

    public void SaveAudioSettings()
    {
        /*PlayerPrefs.SetFloat("GlobalVolume", globalVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("AmbientVolume", ambientVolume);
        PlayerPrefs.SetFloat("SFxVolume", SFxVolume);*/
        audioSettings.globalVolume = globalVolume;
        audioSettings.musicVolume = musicVolume;
        audioSettings.ambientVolume = ambientVolume;
        audioSettings.sfxVolume = SFxVolume;
        //PlayerPrefs.Save();
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + fileName, false);
        sw.Write(JsonUtility.ToJson(audioSettings));
        sw.Close();
    }

    public void LoadAudioSettings()
    {
        if (File.Exists(fileName))
        {
            Debug.Log("Persistent data path " + Application.persistentDataPath);
            StreamReader sr = new StreamReader(Application.persistentDataPath + "/" + fileName);
            string data = sr.ReadToEnd();
            audioSettings = JsonUtility.FromJson<AudioSettings>(data);
            sr.Close();
            globalVolume = audioSettings.globalVolume;
            musicVolume = audioSettings.musicVolume;
            ambientVolume = audioSettings.ambientVolume;
            SFxVolume = audioSettings.sfxVolume;
        }
        else
        {
            audioSettings.globalVolume = 0f;
            audioSettings.musicVolume = 0f;
            audioSettings.ambientVolume = 0f;
            audioSettings.sfxVolume = 0f;
        }
        /*if (PlayerPrefs.HasKey("GlobalVolume"))
        {
            globalVolume = PlayerPrefs.GetFloat("GlobalVolume");
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            SFxVolume = PlayerPrefs.GetFloat("SFxVolume");
            ambientVolume = PlayerPrefs.GetFloat("AmbientVolume");
        }
        else
        {
            globalVolume = 0f;
            musicVolume = 0f;
            SFxVolume = 0f;
            ambientVolume = 0f;
        }*/

        globalSlider.value = globalVolume;
        MusicSlider.value = musicVolume;
        AmbientSlider.value = ambientVolume;
        SFxSlider.value = SFxVolume;
    }
}

public class AudioSettings
{
    public float globalVolume;
    public float musicVolume;
    public float ambientVolume;
    public float sfxVolume;
}
