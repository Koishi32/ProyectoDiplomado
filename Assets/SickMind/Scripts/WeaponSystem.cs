using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponSystem : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab_P, bulletPrefab_S;
    [SerializeField] Transform firePoint_P,firePoint_S,protaTransform;
    [SerializeField] float fireForce = 20.0f;
    [SerializeField] PlayerStats myStats;
    [SerializeField] TextMeshProUGUI AmunitionTextP; [SerializeField] TextMeshProUGUI AmunitionTextS;
    [SerializeField]float FireDelay;
    // Start is called before the first frame update
    private void Start()
    {
        AmunitionTextP.text = myStats.currentClip_P+"/"+ myStats.currentAmmo_P;
        AmunitionTextS.text = myStats.currentClip_S + "/" + myStats.currentAmmo_S;
    }
    public void Fire(int IndexItemRecived) {

        switch (IndexItemRecived)
        {
            case 1: // pistol
                //Invoke("PistolFire", FireDelay);
                PistolFire();
                break;
            case 2: // shotgun
                //Invoke("ShotgunFire", FireDelay);
                ShotgunFire();
                break;
            default:
                Debug.Log("Need valid index for gun !!!");
                break;
        }    
    }
    public void Reload(int IndexItemRecived)
    {

        switch (IndexItemRecived)
        {
            case 1: // pistol
                PistolReload();
                break;
            case 2: // shotgun
                ShotgunReload();
                break;
            default:
                Debug.Log("Need valid index for Reloading gun !!!");
                break;
        }
    }
    void PistolFire() {
        if (myStats.currentClip_P > 0)
        {
            Sound a = AudioManager.Instance.ReturnSFXSound("Pistol");

            GameObject bullet = Instantiate(bulletPrefab_P, firePoint_P.position, Quaternion.identity);
            bullet.GetComponent<Transform>().forward = protaTransform.forward;
            bullet.GetComponent<Rigidbody>().AddForce((new Vector3(protaTransform.forward.x, 0, protaTransform.forward.z)* fireForce), ForceMode.Impulse);
            AudioSource objectAudio = bullet.GetComponent<AudioSource>();
            objectAudio.clip = a.clip;
            objectAudio.Play();
            myStats.currentClip_P--;
            AmunitionTextP.text = myStats.currentClip_P + "/" + myStats.currentAmmo_P;
        }
    }
    void ShotgunFire()
    {
        if (myStats.currentClip_S > 0)
        {
            Sound a = AudioManager.Instance.ReturnSFXSound("Shotgun");
            GameObject bullet = Instantiate(bulletPrefab_S, firePoint_S.position, Quaternion.identity);
            bullet.GetComponent<Transform>().forward = protaTransform.forward;
            bullet.GetComponent<Rigidbody>().AddForce((new Vector3(protaTransform.forward.x, 0, protaTransform.forward.z) * fireForce), ForceMode.Impulse);
            AudioSource objectAudio = bullet.GetComponent<AudioSource>();
            objectAudio.clip = a.clip;
            objectAudio.Play();
            myStats.currentClip_S--;
            AmunitionTextS.text = myStats.currentClip_S + "/" + myStats.currentAmmo_S;
        }
    }
     void PistolReload() {
        int reloadAmmount = myStats.maxClipSize_P - myStats.currentClip_P;
        reloadAmmount = (myStats.currentAmmo_P - reloadAmmount) >= 0 ? reloadAmmount : myStats.currentAmmo_P;
        myStats.currentClip_P += reloadAmmount;
        myStats.currentAmmo_P -= reloadAmmount;
        AmunitionTextP.text = myStats.currentClip_P + "/" + myStats.currentAmmo_P;
    }
     void ShotgunReload()
    {
        int reloadAmmount = myStats.maxClipSize_S - myStats.currentClip_S;
        reloadAmmount = (myStats.currentAmmo_S - reloadAmmount) >= 0 ? reloadAmmount : myStats.currentAmmo_S;
        myStats.currentClip_S += reloadAmmount;
        myStats.currentAmmo_S -= reloadAmmount;
        AmunitionTextS.text = myStats.currentClip_S + "/" + myStats.currentAmmo_S;
    }
    public void AddAmmoShotgun (int ammoAmount) {
        myStats.currentAmmo_S += ammoAmount;

        AmunitionTextS.text = myStats.currentClip_S + "/" + myStats.currentAmmo_S;
    }
    public void AddAmmoPistol(int ammoAmount)
    {
        myStats.currentAmmo_P += ammoAmount;

        AmunitionTextP.text = myStats.currentClip_P + "/" + myStats.currentAmmo_P;
    }
}
