using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyGenerator : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField] Transform WinScreen;
    [SerializeField] GameObject zombiePrefab, DronPrefab;
    [SerializeField] PlayerStats myStats;
    [SerializeField]gameManager MygameManager;
    int maxzombie, maxDrones;
    private int currentZs, currentDs;
    public SpamType myType;
    bool CheckEnemies = false;
    private void Awake()
    {
        WinScreen.gameObject.SetActive(false);
        currentZs = 0;
        currentDs = 0;

        CheckEnemies = false;
    }
    void Start()
    {
        
        
        SetMaximusEnemiesToDay();

        switch (myType)
        {
            case SpamType.BASE:
                NightBehaviour();
                break;
            case SpamType.EXPLORER:
                ExploreBehaviour();
                break;
        }
        
    }

    private void Update()
    {
        if (!CheckEnemies) 
            return;
        if (currentDs <= 0 && currentZs <= 0 && MygameManager.charControl.IsAlive) {
            if (myStats.DaysHappened == 4)
            {
                //Debug.Log("Fin demo");
                WinScreen.gameObject.SetActive(true);
                AudioManager.Instance.musicSource.Stop();
                Invoke("SceneFinal", 3.0f);
            }
            else
            {
                myStats.DaysHappened = myStats.DaysHappened + 1;
                myStats.HoursLeftOfDay = 12;
                WinScreen.gameObject.SetActive(true);
               // Debug.Log("Noche terminada: volver a base");
                CheckEnemies = false;
                Invoke("SceneTrasition", 3.0f);
            }
            
        }
    }

    void  SceneTrasition()
    {
        SceneManager.LoadScene(1);
    }
    void SceneFinal()
    {
        SceneManager.LoadScene(4);
    }
    void NightBehaviour() {
        
        SpawnEnemies();
        CheckEnemies = true;
    }
    void ExploreBehaviour() {
        SpawnEnemies();
        CheckEnemies= false;

    }
    void SetMaximusEnemiesToDay() {
        int ActualDay = myStats.DaysHappened;
        switch (ActualDay) {
            case 0:
                maxzombie = 10;
                maxDrones = 0;
                
                break;
            case 1:
                maxzombie = 0;
                maxDrones = 5;
                
                break;
            case 2:
                maxzombie = 8;
                maxDrones = 2;
                
                break;
            case 3:
                maxzombie = 5;
                maxDrones = 10;
                
                break;
            case 4:
                maxzombie = 10;
                maxDrones = 10;
                
                break;
            default: /// rather the end should show up
                maxzombie = 1;
                maxDrones = 1;
                break;

        }
    }
    public void SpawnEnemies() // Spawn All enemies
    {

        for (int i = 0; i < maxzombie; i++)
        {
            Instantiate(zombiePrefab,
                spawnPoints[Random.Range(0, spawnPoints.Length)].position,
                Quaternion.identity);
            currentZs++;
        }
        for (int i = 0; i < maxDrones; i++)
        {
            Instantiate(DronPrefab,
                spawnPoints[Random.Range(0, spawnPoints.Length)].position,
                Quaternion.identity);
            currentDs++;
        }

    }

    public enum SpamType {
        BASE,
        EXPLORER
    }

    public void CurrentZombieReduction(){
        currentZs--;
    }
    public void CurrentDronReduction()
    {
        currentDs--;
    }
}
