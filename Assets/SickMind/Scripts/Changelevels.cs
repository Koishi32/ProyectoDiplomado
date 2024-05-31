using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Changelevels : MonoBehaviour
{
    [SerializeField] Transform E_Icon;
    bool checkInput;

    private void Start()
    {
        E_Icon.gameObject.SetActive(false);
        checkInput = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            E_Icon.gameObject.SetActive(true);
            checkInput = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            E_Icon.gameObject.SetActive(false);
            checkInput = false;

        }
    }
    private void Update()
    {
        if (checkInput) // next time use inheritance... 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(1);
                Destroy(this.gameObject);
            }
        }
    }
}
