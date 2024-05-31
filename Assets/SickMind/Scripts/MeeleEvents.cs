using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleEvents : MonoBehaviour
{
    [SerializeField] CharacterControl AnimReference;
    [SerializeField] Animator TentaculoHabilidadT;
    [SerializeField] Animator TentaculoHabilidadV;
    public void verificaCombo()
    {
        if (AnimReference.canP)
        {
            AnimReference.nroP = 0;
            AnimReference.canP = false;
            AnimReference.CharAnimator.SetInteger("A", AnimReference.nroP);
        }
        else
        {
            if (AnimReference.nroP > 1)
            {
                AnimReference.CharAnimator.SetInteger("A", AnimReference.nroP);
            }
        }
    }
    public void canPTrue()
    {
        AnimReference.canP = true;
    }
    public void SetPistolStandingFalse()
    {
        AnimReference.CharAnimator.SetBool("PistolShotOver", true);
        //Debug.Log("Pistol Animation Over");
    }

    public void SetShotgunStandingFalse()
    {
        AnimReference.CharAnimator.SetBool("ShotgunShotOver", true);
        //Debug.Log("Shotgun Animation Over");
    }
    public void FireCurrentGun() {
        AnimReference.FireCurrentGun();
    }
    
    public void ActivateTentaculo (){
        TentaculoHabilidadT.SetTrigger("TentaculoSkill");
    }
    public void ActivateVenom()
    {
        TentaculoHabilidadV.SetTrigger("VenomSkill");
    }
}
