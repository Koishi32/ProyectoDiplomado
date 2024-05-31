using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations.Rigging;
using UnityEngine.AI;
public class CharacterControl : MonoBehaviour
{
    [SerializeField] float velMov;
    //[SerializeField] private LayerMask groundMask;
    Camera mainCamera;
    Rigidbody Myrb;
    [SerializeField] Transform Protagonist;
    private float Xmov, Ymov;
    private Vector3 direction = Vector3.zero;
    public Animator CharAnimator;
    public int nroP;
    public bool canP;
    int CurrentItemEquiep = 0; // 0 for normal meele 1 for pistol, 2 for shotgun
    [SerializeField] PlayerStats myStats;
    public bool IsAlive;
    bool IsIventoryOpen;
    [SerializeField] Slider lifeSlider,stamineSlider,expSlider;
    WeaponSystem weaponSystem;
    Skills SkillSystem;
    [SerializeField] GameObject GunMenu;
    [SerializeField] TextMeshProUGUI Life_PlayerText; [SerializeField] TextMeshProUGUI Exp_PlayerText;
    [SerializeField] TextMeshProUGUI Level_PlayerText; [SerializeField] TextMeshProUGUI Stamina_PlayerText;
    public Transform Sight;
    public bool IsAllowedToAttack=true;
    [SerializeField] Transform WarningXWeapongS, WarningXweaponExtra;
    void Start()
    {
        WarningXWeapongS.gameObject.SetActive(false);
        WarningXweaponExtra.gameObject.SetActive(false);
        LoadMaxSOStats();
        UpdateUI();
        IsAlive = true;
        Myrb = gameObject.GetComponent<Rigidbody>();
        weaponSystem = gameObject.GetComponent<WeaponSystem>();
        SkillSystem = gameObject.GetComponentInChildren<Skills>();
        mainCamera = Camera.main;
        canP = true;
        IsIventoryOpen = false;
        nroP = 0;
    }

    void Update()
    {
        if (!IsAlive || SkillSystem.SkillToActivate) return;
        Xmov = Input.GetAxis("Horizontal");
        Ymov = Input.GetAxis("Vertical");
        InventoryInput();
        if (!IsIventoryOpen  && IsAllowedToAttack) { CheckInventory(); }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        AimCheck();
        if (!IsAlive || SkillSystem.SkillToActivate) {
            return;
        }
        
        if (CharAnimator.GetInteger("A") == 0)
        {
            CheckMovement();
        }
        else {
            if (!IsAllowedToAttack)
            {
                return;
            }
            Myrb.velocity = Vector3.zero;
            if (CharAnimator.GetBool("IsRunning") == true)
            {
                CharAnimator.SetBool("IsRunning", false);
            }
        }


    }
    void CheckInventory() {
        switch (CurrentItemEquiep)
        {
            case 0:
                CheckMeeleInput();
                break;
            case 1:
                {
                    CheckGuninput();
                    break;
                }
            case 2:
                {
                    CheckGuninput();
                    break;
                }
        }
    }
    public void CheckMovement() {

        if (Ymov != 0 || Xmov != 0)
        {
            if (!canP) {
                canP = true;
            }
            CharAnimator.SetBool("IsRunning", true);
            CharAnimator.SetFloat("Speed", Ymov);
            CharAnimator.SetFloat("SpeedLateral", Xmov);
            direction = (Protagonist.transform.forward * Ymov + Protagonist.transform.right * Xmov);
            Vector3 finalVelocity = new Vector3(direction.x, 0, direction.z) * velMov;
            Myrb.velocity = finalVelocity * Time.fixedDeltaTime;

        }
        else {
            Myrb.velocity = Vector3.zero;
            CharAnimator.SetFloat("Speed", 0);
            CharAnimator.SetFloat("SpeedLateral", 0);
            if (CharAnimator.GetBool("IsRunning") == true) {
                CharAnimator.SetBool("IsRunning", false);
            }
        }
    }
    void InventoryInput() {
        AnimatorStateInfo MenuState = GunMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Q)) {
            if (MenuState.IsName("GunMenu"))
            {
                GunMenu.GetComponent<Animator>().SetTrigger("close");
                WarningXWeapongS.gameObject.SetActive(false);
                WarningXweaponExtra.gameObject.SetActive(false);
                IsIventoryOpen = false;
            }
            else if (MenuState.IsName("closeGunMenu") || MenuState.IsName("default")) {
                GunMenu.GetComponent<Animator>().SetTrigger("open");
                IsIventoryOpen = true;
            }
        }
    }

    void CheckMeeleInput() {
        if (Input.GetMouseButtonDown(0)) {
            Myrb.velocity = Vector3.zero;
            if (canP && nroP < 3)
            {
                nroP++;
                if (nroP == 1) {
                    CharAnimator.SetInteger("A", nroP);
                    AudioManager.Instance.PlaySFX("Swing");
                }
                if (nroP==3) {
                    AudioManager.Instance.PlaySFX("Swing");
                }
                canP = false;
            }
        }
        if (CharAnimator.GetCurrentAnimatorStateInfo(0).IsName("Iddle") && nroP == 0) {
            canP = true;
        }
    }
    void CheckGuninput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (CurrentItemEquiep) {
                case 1:
                    CharAnimator.SetTrigger("PistolShot");
                    CharAnimator.SetBool("PistolShotOver", false);
                    break;
                case 2:
                    CharAnimator.SetTrigger("ShotgunShot");
                    CharAnimator.SetBool("ShotgunShotOver", false);
                    break;

            }


        }
        if (Input.GetKeyDown(KeyCode.R)) {
            weaponSystem.Reload(CurrentItemEquiep);
            //Debug.Log("Relaoding item" + CurrentItemEquiep);
        }
    }

    public void FireCurrentGun() {
        weaponSystem.Fire(CurrentItemEquiep);
        //Debug.Log("Firing item" + CurrentItemEquiep);
    }
    
    private void AimCheck()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calcular direccion
            direction = position - transform.position;
            direction.y = 0;

            // Mirar a la direccion del frente
            Protagonist.transform.forward = direction;
            Sight.position = new Vector3(position.x, Sight.position.y, position.z);
        }
    }
    int layerMask = 1 << 3;
    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo,150.0f, layerMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }

    public void ReciveDamage(int DamageTaken) {
        if (IsAlive) {
            //Debug.Log("take dmg");
            myStats.life_actual = myStats.life_actual - DamageTaken;
            lifeSlider.value = Mathf.Clamp(myStats.life_actual, 0, lifeSlider.maxValue);
            if (myStats.life_actual <= 0)
            {
                IsAlive = false;
                this.GetComponentInChildren<RigBuilder>().enabled = false;
                CharAnimator.SetTrigger("Death");
                Myrb.velocity = Vector3.zero;

                //Debug.Log("GAME OVER");
                GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>().LoseScreenShow();
                Invoke("ResetToMainMenu",3.8f);
            }
            Life_PlayerText.text = myStats.life_actual+" / "+myStats.life_max;
        }
    }

    void ResetToMainMenu() {
        SceneManager.LoadScene(0);
    }
    public void SetItemEquiped(int Itemindex) {  // 1 meele, 2 pistosl, 3 shotgun
        int PastItem = CurrentItemEquiep;

        switch  (Itemindex) {
            case 0:
                CurrentItemEquiep = Itemindex;
                break;
            case 1:
                CurrentItemEquiep = Itemindex;
                break;
            case 2:
                if (myStats.CanIuseShotgun)
                {
                    CurrentItemEquiep = Itemindex;
                    ActivateShotgunObjects();

                }
                else {
                    CurrentItemEquiep = PastItem;
                    WarningXWeapongS.gameObject.SetActive(true);

                }
                break;
            case 3:
                if (myStats.CanIuseExtra)
                {
                    CurrentItemEquiep = Itemindex;
                }
                else
                {
                    CurrentItemEquiep = PastItem;
                    WarningXweaponExtra.gameObject.SetActive(true);

                }
                break;
            }
    }

    void ResetGunAnim() {
        CharAnimator.SetBool("ShootPistol", false);
        CharAnimator.SetBool("ShootShotgun", false);
    }
    public void LoadMaxSOStats() {
        myStats.SetMaxvalues();
    }

    public void UpdateUI() {
        Life_PlayerText.text = myStats.life_actual + " / " + myStats.life_max;
        Exp_PlayerText.text = myStats.exp_actual + " / " + myStats.exp_max;
        Level_PlayerText.text = "LV: " + myStats.Level;
        Stamina_PlayerText.text = myStats.stamina_actual + " / " + myStats.stamina_Max;
        lifeSlider.maxValue = myStats.life_max;
        lifeSlider.value = myStats.life_actual;
        stamineSlider.maxValue = myStats.stamina_Max;
        stamineSlider.value = myStats.stamina_actual;
        expSlider.maxValue = myStats.exp_max;
        expSlider.value = myStats.exp_actual;
    }

    public void StopMovement() {
        Myrb.velocity = Vector3.zero;
        if (CharAnimator.GetBool("IsRunning") == true)
        {
            CharAnimator.SetBool("IsRunning", false);
        }
    }
    [SerializeField] Transform barra, pistol, escopeta, pistola_Ico, escopeta_Ico, pistaloAmmo, EscopetaAmmo, Palanca_Ico;
    public void ActivateShotgunObjects()
    {
        barra.gameObject.SetActive(false);
        pistol.gameObject.SetActive(false);
        escopeta.gameObject.SetActive(true);
        pistola_Ico.gameObject.SetActive(false);
        escopeta_Ico.gameObject.SetActive(true);
        pistaloAmmo.gameObject.SetActive(false);
        EscopetaAmmo.gameObject.SetActive(true);
        Palanca_Ico.gameObject.SetActive(false);
    }
}
