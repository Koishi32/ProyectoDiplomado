using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyLife;

public class ColliderAcid : MonoBehaviour
{
    EnemyLife temp;
    public void CallSoundEffectPoison() {
        AudioManager.Instance.PlaySFX("Skill2");
    } 
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            temp = other.gameObject.GetComponent<EnemyLife>();
            if (other!=null) {
                temp.ReciveDamage(5);
                if (temp.enemyType == EnemyLife.EnemyType.ZOMBIE)
                    temp.SetAcidVariable();
            }
            
        }
    }
    
}
