using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerStats Mystats;
    public CharacterControl charControl;
    [SerializeField] Skills charSkills;
    public Transform[] bases;
    [SerializeField] Slider lifeSlider;
    [SerializeField] TextMeshProUGUI lifebaseText;
    [SerializeField] int CurrentLevel;// 1 base, 2 place where figthing is allowed
    [SerializeField] Transform UpLevelWarning;
    [SerializeField] Transform LoseScreen;
    int SceneIndex=0;
    private void Awake()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        if (SceneIndex == 1)
        {
            
            updateLifeUI();
            charControl.IsAllowedToAttack = false;
            charSkills.CanUseSkill = false;
        }
        else if (SceneIndex == 2)
        {
            
            updateLifeUI();
            charControl.IsAllowedToAttack = true;
            charSkills.CanUseSkill = true;
        }
        else if (SceneIndex == 3) {
            
            charControl.IsAllowedToAttack = true;
            charSkills.CanUseSkill = true;
        }
    }

    void Start()
    {
        switch (SceneIndex) {
            case 1:
                AudioManager.Instance.BaseMusic();
                break; 
            case 2:
                AudioManager.Instance.NocheMusic();
                break;
            case 3:
                AudioManager.Instance.CityMusic();
                break;
        }
        LoseScreen.gameObject.SetActive(false);
        if (lifeSlider!=null) {
            lifeSlider.maxValue = 100;
            lifeSlider.value = Mystats.MybaseLife;
        }
        
    }

    // Update is called once per frame


    public void RecibeDamage(int Dmg) {
        Mystats.MybaseLife -= 2;
        updateLifeUI();
        if (Mystats.MybaseLife <= 0) {
            Time.timeScale = 0;
            Debug.Log("GAME OVER");
            LoseScreenShow();
            Invoke("ResetToMainMenu", 3.0f);
        }
    }
    public void LoseScreenShow() {
        LoseScreen.gameObject.SetActive(true);
    }
    void  ResetToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayerGetExp(int expRev) {
        Mystats.exp_actual += expRev;

        if (Mystats.exp_actual>=Mystats.exp_max) {
            int extraExp = Mystats.exp_max - Mystats.exp_actual;
            UpLevelWarning.gameObject.GetComponent<Animator>().SetTrigger("Anim");
            Mystats.exp_actual = extraExp;
            Mystats.Level = Mystats.Level + 1;
            Mystats.SetMaxvalues();
            
        }
        charControl.UpdateUI();


    }
    public Transform ReturnRandomBaseTransform() {
        Transform a = bases[Random.Range(0, bases.Length)];
        return a;
    }
    public void updateLifeUI() {
        lifeSlider.value = Mathf.Clamp(Mystats.MybaseLife, 0, lifeSlider.maxValue);
        lifebaseText.text = Mathf.Clamp(Mystats.MybaseLife, 0, lifeSlider.maxValue) + " / " + lifeSlider.maxValue;
    }
    
    public void GetShotgun() {
        updatePlayerGUI();
        Mystats.CanIuseShotgun = true;
        Mystats.currentAmmo_S = Mystats.currentAmmo_S + 6;
    }
    public void updatePlayerGUI() { //
        charControl.UpdateUI();
    }

    public void RestoreHoursNewDay(){
        Mystats.HoursLeftOfDay = 12;
    }
    
}
