using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtPlayer : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab,Fallfire;
    [SerializeField] Transform FirePoint;
    [SerializeField] DronController ControlRef;
    [SerializeField] float fireForce = 20.0f;
    [SerializeField] Transform fallPoint,PivotTolook;
    [SerializeField] AudioSource MyAudioFire;
    bool fall=false;
     Vector3 PlayerPos;
    Sound a;
    private void Start()
    {
        fall = false;
    }
    public void Fire() {
        a = AudioManager.Instance.ReturnSFXSound("DronAttack");
        MyAudioFire.clip = a.clip;
        MyAudioFire.Play();
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
        bullet.GetComponent<Transform>().LookAt(PlayerPos);
        Vector3 direction = (PlayerPos - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().AddForce((direction * fireForce), ForceMode.Impulse);
        Destroy(bullet,3.0f);
    }
    private void FixedUpdate()
    {
        PlayerPos = ControlRef.transformPlayer.position;
        FaceTarget(PlayerPos);
        if (fall) {
            Fallfire.SetActive(true);
            //var b = GameObject.Instantiate(Fallfire, fallPoint.position, Quaternion.identity);
            MyAudioFire.Stop();
            a = AudioManager.Instance.ReturnSFXSound("DronD");
            MyAudioFire.clip = a.clip;
            MyAudioFire.Play();
            //Destroy(b,5.0f);
            Destroy(this.transform.parent.transform.parent.gameObject,4.0f);
            this.gameObject.SetActive(false);
        }
    }
    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - PivotTolook.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        PivotTolook.rotation = Quaternion.Slerp(PivotTolook.rotation, rotation,1.0f);
    }
    public void DyingAnime() {
        fall = true;
    }

}
