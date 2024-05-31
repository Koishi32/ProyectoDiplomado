using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MissionDesk : MonoBehaviour
{

    [SerializeField] GameObject SignalE,LoadWarning,MapTimeManagerCanvas;
    [SerializeField] PlayerStats myStats; // FOr the hours of day
    [SerializeField] SaveToJason mySaver;
    [SerializeField] gameManager MangerRef;
    public const int CostExplore = 5;
    public const int CostRepair = 1;
    public const int CostRest = 2;
    bool checkInput;
    [SerializeField] TextMeshProUGUI HorasText;
    [SerializeField] TextMeshProUGUI Days;
    private void Awake()
    {
        HorasText.text = ""+myStats.HoursLeftOfDay;
        Days.text = "Dias pasados: " + myStats.DaysHappened;
        MapTimeManagerCanvas.SetActive(false);
        LoadWarning.SetActive(false);
        SignalE.SetActive(false);
        checkInput = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SignalE.SetActive(true);
            checkInput = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SignalE.SetActive(false);
            MapTimeManagerCanvas.SetActive(false);
            checkInput = false;

        }
    }
    private void Update()
    {
        if (checkInput)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                checkInput = false;
                MapTimeManagerCanvas.SetActive(true);
            }
        } else if (!checkInput) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                checkInput = true;
                MapTimeManagerCanvas.SetActive(false);
            }
        }
    }

    public void RestAndSaveStats()
    {
        if (myStats.HoursLeftOfDay >= CostRest)
        {
            myStats.HoursLeftOfDay -= CostRest;
            myStats.life_actual = myStats.life_max;
            myStats.stamina_actual = myStats.stamina_Max;
            mySaver.SaveToJson();
            HorasText.text = "" + myStats.HoursLeftOfDay;
            MangerRef.updatePlayerGUI();
        }
    }
    public void RepairBase()
    {
        if (myStats.HoursLeftOfDay >= CostRepair) {
            myStats.HoursLeftOfDay -= CostRepair;
            int totalRepaired=myStats.MybaseLife + 10;
            myStats.MybaseLife = Mathf.Clamp(totalRepaired,0,myStats.MybaseLife_max);
            HorasText.text = "" + myStats.HoursLeftOfDay;
            MangerRef.updateLifeUI();
        }
    }
    public void GoAheadNight() {
        HorasText.text = "" + myStats.HoursLeftOfDay;
        LoadWarning.SetActive(true);
        SceneManager.LoadScene(2);
    }
    public void ExploreCityLev1()
    {
        if (myStats.HoursLeftOfDay >= CostExplore)
        {
            myStats.HoursLeftOfDay -= CostExplore;
            LoadWarning.SetActive(true);
            HorasText.text = "" + myStats.HoursLeftOfDay;
            SceneManager.LoadScene(3);
        }
    }
    public void ExploreCityLev2()
    {
        //Debug.Log("Demo no Incluye Lev 2");
    }
    public void ExploreCityLev3()
    {
       // Debug.Log("Demo no Incluye Lev 3");
    }


}
