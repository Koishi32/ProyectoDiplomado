using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuPausa : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] Transform myObjectMenu;
    void Awake()
    {
        // Ensure time scale is reset to 1 when the scene loads
        Time.timeScale = 1f;
        myObjectMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        // Toggle pause with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        myObjectMenu.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        myObjectMenu.gameObject.SetActive(false);
    }

    public void LoadScene(int scenIndex)
    {
        Time.timeScale = 1f; // Ensure time scale is reset before loading a new scene
        SceneManager.LoadScene(scenIndex);
    }
    public void AudioWorks() { 
    
    }

}
