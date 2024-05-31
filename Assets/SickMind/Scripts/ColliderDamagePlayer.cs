using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamagePlayer : MonoBehaviour
{
    /// <summary>
    /// Se daran causa daño meele
    /// </summary>
    [SerializeField] int DamageDealt = 15;
    [SerializeField]TypeDamage myType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (myType == TypeDamage.PERPETUALMEELE)
            {
                //Debug.Log("Enemy hited meele");
                other.gameObject.GetComponent<EnemyLife>().ReciveDamage(DamageDealt);
                //other.gameObject.GetComponent<EnemyLife>().CheckLife();
            }
            else if (myType == TypeDamage.TEMPORALBULLET)
            {
                //Debug.Log("Enemy hited shot");
                other.gameObject.GetComponent<EnemyLife>().ReciveDamageGun(DamageDealt);
                //Trigger visual bullet effect
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<BoxCollider>().enabled = false;
                Destroy(this.gameObject, 2);
            }
        }
    }
    private void Start()
    {
        switch (myType) {
            case TypeDamage.TEMPORALBULLET:
                Destroy(this.gameObject,2.5f);
                break;
        }
    }
    enum TypeDamage { 
        PERPETUALMEELE, //Only register a Trigger hit
        TEMPORALBULLET
    }
}
