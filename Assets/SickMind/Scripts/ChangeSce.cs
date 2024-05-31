using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSce : MonoBehaviour
{
    public void BackToMenu(int sce) {
        SceneManager.LoadScene(sce);
    }
}
