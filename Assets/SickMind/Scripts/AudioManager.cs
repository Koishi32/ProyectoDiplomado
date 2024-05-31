using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    [SerializeField] AudioMixer myMixer;
    [SerializeField] PlayerStats myStats;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { 
            Destroy(gameObject);
        }
    }
    public void PlayMusic(string name) { //Play for Player Main
        Sound s = Array.Find(musicSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Not found");
        }
        else {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void MenuMusic() {
        PlayMusic("Menu");
    }
    public void BaseMusic()
    {
        PlayMusic("Base");
    }
    public void CityMusic()
    {
        PlayMusic("City");
    }
    public void NocheMusic()
    {
        PlayMusic("Noche");
    }
    public void PlaySkill4() {
        Sound s = Array.Find(sfxSounds, x => x.Name == "Skill4");
        sfxSource.clip = s.clip;
        sfxSource.Play();
    }

    public void PlaySFX(string name) //Play for Player
    {
        Sound s = Array.Find(sfxSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Not found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void MusicVolume(float SliderValue) {
        
        float MixerFloatGet = Mathf.Log10(SliderValue) * 20;
        myStats.MusicVolume =SliderValue;
        myMixer.SetFloat("MusicVolume", MixerFloatGet);
    }
    public void SfxVolume(float SliderValue) {
        float MixerFloatGet = Mathf.Log10(SliderValue) * 20;
        myStats.SFXVolume = SliderValue;
        myMixer.SetFloat("SFXVolume",MixerFloatGet);
    }
    public void LoadFromSOToAudioMixer() { 
        float MixerFloatGetMusic= Mathf.Log10(myStats.MusicVolume) * 20;
        myMixer.SetFloat("MusicVolume", MixerFloatGetMusic);

        float MixerFloatGetSFX = Mathf.Log10(myStats.SFXVolume) * 20;
        myMixer.SetFloat("SFXVolume", MixerFloatGetSFX);
    }
    public Vector2 GetSliderValuesStoredInSO() {
        return new Vector2(myStats.MusicVolume,myStats.SFXVolume);
    }
    public Sound ReturnSFXSound(string SFXName) {
        Sound s = Array.Find(sfxSounds, x => x.Name == SFXName);
        if (s == null)
        {
            Debug.Log("Not found");
            return null;
        }
        return s;
    }
}

/* public AudioSource AmbientAudioSource;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangeAudioTrack() { 
        
    }*/
