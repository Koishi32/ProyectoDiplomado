using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamageEnemy : MonoBehaviour
{
    [SerializeField] int DamageDealt = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Player hited");
            other.gameObject.GetComponent<CharacterControl>().ReciveDamage(DamageDealt);
            //other.gameObject.GetComponent<EnemyLife>().CheckLife();
        }
        else if (other.tag == "base") {
            //Debug.Log("Base Hitted");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>().RecibeDamage(DamageDealt);

        }
    }
}
