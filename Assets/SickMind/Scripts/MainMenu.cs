using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField]SaveToJason LoaderData;
    [SerializeField]PlayerStats MyStats;
    bool LoadAnotherSceneAllowed=true;
    [SerializeField] GameObject WarningSave;
    public void Loadlevel(int levelIndex) {
        if (LoadAnotherSceneAllowed) {
            MyStats.Level = 0;
            MyStats.SetLevel0values();
            MyStats.SetMaxvalues();
            LoadAnotherSceneAllowed = false;
            SceneManager.LoadScene(levelIndex);
        }
        
    }
    public void LoadBaseLevelWithStats(int levelIndex) {
        
        if (LoadAnotherSceneAllowed)
        {
            if (!LoaderData.CheckIfSaveExist()) {
                WarningSave.SetActive(true);
                return;
            }
            LoadAnotherSceneAllowed = false;
            //LoadFromSaveToSO_PlayerStats();
            SceneManager.LoadScene(levelIndex);
        }
    }
    private void Awake()
    {
        Time.timeScale = 1;
        LoadFromBeginign();
        LoadAnotherSceneAllowed = true;
        WarningSave.SetActive(false);
    }

    void LoadFromBeginign() {
        if (!LoaderData.CheckIfSaveExist())
        {
            MyStats.Level = 0;
            MyStats.SetLevel0values();
            MyStats.SetMaxvalues();
            return;
        }
        LoadFromSaveToSO_PlayerStats();
    }
    public void LoadFromSaveToSO_PlayerStats() {
        LoaderData.LoadFromJson();
        MyStats.SetMaxvalues();
        
    }
    private void Start()
    {
        AudioManager.Instance.LoadFromSOToAudioMixer();
        AudioManager.Instance.MenuMusic();
    }

    public void Exit() { 
        Application.Quit();
    }
}
