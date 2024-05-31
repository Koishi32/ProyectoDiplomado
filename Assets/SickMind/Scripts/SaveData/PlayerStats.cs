using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Char",menuName= "ScriptableObjects/CharacterMain")]
public class PlayerStats : ScriptableObject
{
    public int life_actual;
    public int exp_actual;
    public int Level;
    public int stamina_actual;
    public int MybaseLife;
    public int DaysHappened;
    public int currentClip_P, currentAmmo_P;
    public int currentClip_S, currentAmmo_S;
    public int HoursLeftOfDay;

    public bool CanIuseShotgun;
    public bool CanIuseExtra;

    public float MusicVolume;
    public float SFXVolume;

    public int MybaseLife_max;
    public int stamina_Max;
    public int life_max;
    public int exp_max;
    public int maxClipSize_P;
    public int maxClipSize_S;
    public void SetMaxvalues()
    {
        MybaseLife_max = 100;
        maxClipSize_P = 15;
        maxClipSize_S = 6;
        switch (Level)
        {
            case 0:
                life_max = 100;
                stamina_Max = 100;
                exp_max = 100;
                break;
            case 1:
                life_max = 120;
                stamina_Max = 110;
                exp_max = 150;
                break;
            case 2:
                life_max = 140;
                stamina_Max = 120;
                exp_max = 200;
                break;
            case 3:
                life_max = 160;
                stamina_Max = 130;
                exp_max = 300;
                break;
            case 4:
                life_max = 180;
                stamina_Max = 140;
                exp_max = 400;
                break;
            case 5:
                life_max = 200;
                stamina_Max = 150;
                exp_max = 500;
                break;
        }
    }
    public void SetLevel0values() {
        Level = 0;
        life_actual=100;
        exp_actual=0;
        stamina_actual=100;
        MybaseLife=100;
        DaysHappened=0;
        currentClip_P=0;
        currentAmmo_P=0;
        currentClip_S = 0;
        currentAmmo_S = 0;
        HoursLeftOfDay = 12;
        CanIuseShotgun = false;
        CanIuseExtra = false;
        MusicVolume = 1;
        SFXVolume = 1;

}
}
