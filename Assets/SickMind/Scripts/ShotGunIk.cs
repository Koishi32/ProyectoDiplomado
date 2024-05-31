using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class ShotGunIk : MonoBehaviour
{
    Animator Anima;
    [SerializeField]Rig rig;
    [SerializeField] TwoBoneIKConstraint rig_pistol;
    [SerializeField] Transform TransForm_Target_Shotgun;
    [SerializeField] Transform TransForm_Target_Pistol;
    float ObjetiveWeightGeb,SmoothWeight;
    Vector3 orgPos;
    private void Awake()
    {
        Anima = this.GetComponent<Animator>();
    }
    private void Start()
    {
        orgPos = TransForm_Target_Pistol.position;
    }
    private void Update()
    {
        ForGeneralRig();
        //GunStance();
    }

    void ForGeneralRig() {
        rig.weight = Mathf.Lerp(rig.weight, SmoothWeight, Time.deltaTime * 20);
        if (Anima.GetCurrentAnimatorStateInfo(1).IsName("ShotgunShot") || Anima.GetCurrentAnimatorStateInfo(1).IsName("PistolShot"))
        {
            //Debug.Log("Set IK for shotgun anim");
            SmoothWeight = ObjetiveWeightGeb;
        }
        else
        {
            SmoothWeight = 0f;
        }
    }
    public void setObjetiveWeight0() {
        ObjetiveWeightGeb = 0;
    }
    public void setObjetiveWeight1()
    {
        ObjetiveWeightGeb = 1;
    }
    void GunStance() {

        
        if (Anima.GetCurrentAnimatorStateInfo(1).IsName("PistolShot"))
        {
            rig_pistol.data.target.position = orgPos;
        }
        else if (Anima.GetCurrentAnimatorStateInfo(1).IsName("ShotgunShot"))
        {
            rig_pistol.data.target.position = TransForm_Target_Pistol.position;
        }
    }
}
