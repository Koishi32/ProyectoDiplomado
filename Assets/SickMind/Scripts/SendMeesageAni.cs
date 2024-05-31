using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMeesageAni : MonoBehaviour
{
    [SerializeField] Skills skill_ref;

    public void SkillFinished(){
        skill_ref.returnToNormal();
    }

}
