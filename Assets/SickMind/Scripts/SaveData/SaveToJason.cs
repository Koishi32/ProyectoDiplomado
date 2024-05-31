using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.CompilerServices;
public class SaveToJason : MonoBehaviour
{
    [SerializeField] PlayerStats myStats;

    public void SaveToJson()
    {
        DataPlayer data = TransferFromSOtoData();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/DataPlayerFile.json", json);
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/DataPlayerFile.json");
        DataPlayer data = JsonUtility.FromJson<DataPlayer>(json);
        TransferFromDataToSo(data);
    }

    public DataPlayer TransferFromSOtoData()
    {
        DataPlayer data = new DataPlayer();
        data.life_actual = myStats.life_actual;
        data.exp_actual = myStats.exp_actual;
        data.Level = myStats.Level;
        data.stamina_actual = myStats.stamina_actual;
        data.MybaseLife = myStats.MybaseLife;
        data.DaysHappened = myStats.DaysHappened;
        data.currentClip_P = myStats.currentClip_P;
        data.currentAmmo_P = myStats.currentAmmo_P;
        data.currentClip_S = myStats.currentClip_S;
        data.currentAmmo_S = myStats.currentAmmo_S;
        data.HoursLeftOfDay = myStats.HoursLeftOfDay;
        data.CanIuseShotgun = myStats.CanIuseShotgun;
        data.CanIuseExtra = myStats.CanIuseExtra;
        data.MusicVolume= myStats.MusicVolume;
        data.SFXVolume=myStats.SFXVolume;
        return data;
    }
    public void TransferFromDataToSo(DataPlayer data) {
        myStats.life_actual = data.life_actual;
        myStats.exp_actual = data.exp_actual;
        myStats.Level = data.Level;
        myStats.stamina_actual = data.stamina_actual;
        myStats.MybaseLife = data.MybaseLife;
        myStats.DaysHappened = data.DaysHappened;
        myStats.currentClip_P = data.currentClip_P;
        myStats.currentAmmo_P = data.currentAmmo_P;
        myStats.currentClip_S = data.currentClip_S;
        myStats.currentAmmo_S = data.currentAmmo_S;
        myStats.HoursLeftOfDay = data.HoursLeftOfDay;
        myStats.CanIuseShotgun=data.CanIuseShotgun;
        myStats.CanIuseExtra = data.CanIuseExtra;
        myStats.MusicVolume= data.MusicVolume;
        myStats.SFXVolume=data.SFXVolume;
    }
    public bool CheckIfSaveExist() {
        string fullPath= Application.dataPath + "/DataPlayerFile.json";
        if (File.Exists(fullPath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
