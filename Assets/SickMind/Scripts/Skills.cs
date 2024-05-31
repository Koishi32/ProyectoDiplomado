using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skills : MonoBehaviour
{
    [SerializeField] PlayerStats myStats; // To use Stamina
    Animator PlayerAnim;
    int CurrentSkillActive;
    public bool SkillToActivate;
    [SerializeField] int staminaSkill1, staminaSkill2, staminaSkill3, staminaSkill4;
    [SerializeField] RawImage TentacleSkill, AcidSkill, HealSkill, BombardSkill;
    [SerializeField] GameObject PrefabSkill1, PrefabSkill2;
    [SerializeField] Transform SkillInstancePos, Skill2InstancePos;
    public bool CanUseSkill = true;
    // Update is called once per frame
    private void Start()
    {
        ResetColor();
        SkillToActivate = false;
        CurrentSkillActive = 0;
        PlayerAnim = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (!CanUseSkill) {
            return;
        }
        if (SkillToActivate)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (CurrentSkillActive == 1)
                {
                    returnToNormal();
                    Debug.Log("Cancelling Skill");
                }
                else
                {
                    ResetColor();
                    CurrentSkillActive = 1;
                    TentacleSkill.color = Color.red;
                }

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (CurrentSkillActive == 2)
                {
                    returnToNormal();
                    Debug.Log("Cancelling Skill");
                }
                else
                {
                    ResetColor();
                    CurrentSkillActive = 2;
                    AcidSkill.color = Color.red;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            { // Just Activate the healing Effect
                if (CurrentSkillActive == 3)
                {
                    returnToNormal();
                    //Debug.Log("Cancelling Skill");
                }
                else
                {
                    ResetColor();
                    CurrentSkillActive = 3;
                    HealSkill.color = Color.red;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (CurrentSkillActive == 4)
                {
                    returnToNormal();
                    //Debug.Log("Cancelling Skill");
                }
                else
                {
                    ResetColor();
                    CurrentSkillActive = 4;
                    BombardSkill.color = Color.red;
                }
            }
        }else
        { // A skill key was pressed
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SkillToActivate = true;
                StopMessage();
                CurrentSkillActive = 1;
                TentacleSkill.color = Color.red;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SkillToActivate = true;
                StopMessage();
                CurrentSkillActive = 2;
                AcidSkill.color = Color.red;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            { // Just Activate the healing Effect
                SkillToActivate = true;
                StopMessage();
                CurrentSkillActive = 3;
                HealSkill.color = Color.red;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SkillToActivate = true;
                StopMessage();
                CurrentSkillActive = 4;
                BombardSkill.color = Color.red;

            }
        }

        CheckCurrentSkill();
    }

    void CheckCurrentSkill() {
        if (CurrentSkillActive == 0) {
            return;
        }
        int resto = 0;
        if (Input.GetMouseButtonDown(1)) {
            switch (CurrentSkillActive)
            {
                
                case 1:
                     resto = myStats.stamina_actual - staminaSkill1;
                    if (resto >= 0)
                    {
                        myStats.stamina_actual = resto;
                        skill1();
                    }
                    else {
                       // Debug.Log("NotEnoungStamina");
                        returnToNormal();
                    }
                    break;
                case 2:
                     resto = myStats.stamina_actual - staminaSkill2;
                    if (resto >= 0)
                    {
                        myStats.stamina_actual = resto;
                        skill2();
                    }
                    else
                    {
                        //Debug.Log("NotEnoungStamina");
                        returnToNormal();
                    }
                    break;
                case 3:
                    resto = myStats.stamina_actual - staminaSkill3;
                    if (resto >= 0)
                    {
                        myStats.stamina_actual = resto;
                        skill3();
                    }
                    else
                    {
                       // Debug.Log("NotEnoungStamina");
                        returnToNormal();
                    }
                    break;
                case 4:
                    resto = myStats.stamina_actual - staminaSkill4;
                    if (resto >= 0)
                    {
                        myStats.stamina_actual = resto;
                        skill4();
                    }
                    else
                    {
                        //Debug.Log("NotEnoungStamina");
                        returnToNormal();
                    }
                    break;
            }
        }
    }
    public void returnToNormal()
    {
        PlayerAnim.ResetTrigger("SkillT");
        PlayerAnim.ResetTrigger("SkillV");
        ResetColor();
        CurrentSkillActive = 0;
        SkillToActivate = false;

    }
    void skill1()
    {
        UpdateUIMessage();
        PlayerAnim.SetTrigger("SkillT");
        AudioManager.Instance.PlaySFX("Skill1");
    }
    void skill2()
    {
        UpdateUIMessage();
        PlayerAnim.SetTrigger("SkillV");
        var acido= GameObject.Instantiate(PrefabSkill1, SkillInstancePos.position,Quaternion.identity);
        acido.GetComponent<Transform>().forward = this.gameObject.transform.forward;
        Destroy(acido,5.0f);
    }
    void skill3()
    {
        int maxLife = myStats.life_max;
        int lifeToRecover = myStats.life_actual + (maxLife / 2);
        lifeToRecover = Mathf.Clamp(lifeToRecover,0,maxLife);
        myStats.life_actual = lifeToRecover;
        returnToNormal();
        UpdateUIMessage();
        AudioManager.Instance.PlaySFX("Skill3");
        //Debug.Log("HEAL MYSELF");
    }
    void skill4()
    {
        UpdateUIMessage();
        var missile = GameObject.Instantiate(PrefabSkill2, Skill2InstancePos.position, Quaternion.identity);
        missile.GetComponentInChildren<missileBehav>().StartFall();
        //Debug.Log("BOMBARD THERE");
        returnToNormal();
    }

    void ResetColor() {
        TentacleSkill.color = Color.green;
        AcidSkill.color = Color.green; 
        HealSkill.color = Color.green; 
        BombardSkill.color = Color.green;
    }

    void UpdateUIMessage() {
        SendMessageUpwards("UpdateUI", SendMessageOptions.RequireReceiver);
    }
    void StopMessage()
    {
        SendMessageUpwards("StopMovement", SendMessageOptions.RequireReceiver);
    }
}
