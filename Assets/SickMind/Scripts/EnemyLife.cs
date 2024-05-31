using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    int Life;
    public EnemyType enemyType;
    EnemyController enemyControl;
    //DronController dronController;
    int ExpRecivedFromEnemyDeath;
    void Start()
    {
        switch (enemyType) {
            case EnemyType.DRON:
                Life = 100;
                ExpRecivedFromEnemyDeath = 10;
                //dronController =this.GetComponent<DronController>();
                //DamageDealt = 5;
                break;
            case EnemyType.ZOMBIE:
                Life = 50;
                ExpRecivedFromEnemyDeath = 5;
                enemyControl = this.GetComponent<EnemyController>();
                //DamageDealt = 5;
                break;
        }
    }

    // Update is called once per frame

    public void ReciveDamage(int Dmg) {
        Life -= Dmg;
        if (enemyType == EnemyType.ZOMBIE)
        {
            enemyControl.agent.isStopped = true;
        }
        //Debug.Log("take dmg");
        checkLife();
    }
    public void SetAcidVariable() {
        enemyControl.IsDeathByAcid = true;
    }
    public void ReciveDamageGun(int Dmg) {
        //Debug.Log("take dmg Gun");
        Life -= Dmg;
        checkLife();
    }

    void checkLife() {
        if (Life <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>().PlayerGetExp(ExpRecivedFromEnemyDeath);
            SendCountEnmeydead();
            SendMessage("ActivateDEATHAnim");
            Destroy(this);
            //return;
        }
        else
        {
            //enemyControl.ActivateHurtAnim();
            SendMessage("ActivateHurtAnim");
            
        }
    }

    void SendCountEnmeydead() {
        if (enemyType == EnemyType.DRON) {
            GameObject.FindGameObjectWithTag("EnemManager").GetComponent<EnemyGenerator>().CurrentDronReduction();
        } else if (enemyType == EnemyType.ZOMBIE) {
            GameObject.FindGameObjectWithTag("EnemManager").GetComponent<EnemyGenerator>().CurrentZombieReduction();
        }

    }
    public enum EnemyType
    {
        DRON,
        ZOMBIE,
    }
}
