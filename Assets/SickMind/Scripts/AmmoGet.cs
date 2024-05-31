using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoGet : MonoBehaviour
{
    [SerializeField]GameObject E_Icon;
    bool checkInput;
    WeaponSystem weaponSystem;
    [SerializeField] AmmoType type;
    private void Start()
    {
        E_Icon.SetActive(false);
        checkInput = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player") {
            E_Icon.SetActive(true);
            checkInput = true;
            weaponSystem =other.GetComponent<WeaponSystem>();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            E_Icon.SetActive(false);
            checkInput = false;

        }
    }
    private void Update()
    {
        if (checkInput) {
            if (Input.GetKeyDown(KeyCode.E)) {
                switch (type) {
                    case AmmoType.PISTOL:
                        weaponSystem.AddAmmoPistol(30);
                        Destroy(this.gameObject);
                        break;
                    case AmmoType.SHOTGUN:
                        weaponSystem.AddAmmoShotgun(12);
                        Destroy(this.gameObject);
                        break;

                }
            }
        }
    }

    enum AmmoType { 
    PISTOL,
    SHOTGUN
    }
}
