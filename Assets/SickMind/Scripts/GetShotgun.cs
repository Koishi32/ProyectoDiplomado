using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShotgun : MonoBehaviour
{
    [SerializeField] Transform E_Icon;
    bool checkInput;
    [SerializeField]gameManager GameManagerRef;
    [SerializeField] Transform Shotgun;
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
        if (checkInput)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManagerRef.GetShotgun();
                Destroy(Shotgun.gameObject, 0.1f);
                Destroy(this.gameObject);
            }
        }
    }
}
