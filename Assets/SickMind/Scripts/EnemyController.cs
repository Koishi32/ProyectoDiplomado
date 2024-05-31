using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Animator enemyAnimator;
    public NavMeshAgent agent;
    public Transform transformPlayer, BaseTranform;
    Vector3 patroldestinationPoint;
    Vector3 currentObjetive;
    gameManager GameController;
    public EnemyState currentState;

    [SerializeField] float detectPlayerRange;
    [SerializeField] float detectHouseRange;
    [SerializeField] float detectRangeAttack;
    [SerializeField] Vector2 patrolArea;
    public bool isAlive,isChasingPlayer;
    bool IsExploring;
    [SerializeField] AudioSource MyAudioSource;
    Sound a;
    [SerializeField] DisolveZ disolveEffect;
    private void Awake()
    {

        enemyAnimator = GetComponent<Animator>();
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>();
        MyAudioSource = this.GetComponent<AudioSource>();
        enemyAnimator.SetBool("Death", false);
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        IsDeathByAcid = false;
        IsExploring = false;
        enemyAnimator.SetBool("isAlive", true);
        BaseTranform = GameController.ReturnRandomBaseTransform();
        isAlive = true;
        isChasingPlayer = false;
        currentState = EnemyState.FIND_OBJETIVE;

        if (SceneManager.GetActiveScene().buildIndex > 2)
        { //we are at exploring
            VaryObjetive();
            InvokeRepeating("VaryObjetive", 4.0f, 12.0f);
            IsExploring = true;
        }
        else {
            VaryObjetive();
            InvokeRepeating("VaryObjetive", 2f,12);
            IsExploring= false;
        }
        
    }
    void VaryObjetive() {
        if (!isAlive)
        { return; }
            if ((Random.Range(0, 100) > 75))
        {
            isChasingPlayer = true;
        }
        else {
            isChasingPlayer = false;
        }
        currentState = EnemyState.FIND_OBJETIVE;
    }
    
    void Update()
    {
        if (!isAlive)
        {
            CancelInvoke("VaryObjetive");
            return;
        }
        else {
            if (enemyAnimator != null && isAlive)
            {
                enemyAnimator.SetFloat("Speed", agent.velocity.sqrMagnitude);
            }
            else {
                enemyAnimator.SetFloat("Speed", -1);
            }

            //Debug.Log(currentState.ToString() + "Is chasing player: " + isChasingPlayer);
            CheckObjetiveDistance();
            switch (currentState)
            {
                case EnemyState.FIND_OBJETIVE:
                    FindObjetive();
                    break;
                case EnemyState.CHASE:
                    currentObjetive = transformPlayer.position;
                    agent.SetDestination(currentObjetive);
                    break;
            }
        } 
    }

    void CheckObjetiveDistance() {
        bool PlayerWithingRangeAttack = ((transform.position - transformPlayer.position).magnitude < detectRangeAttack);
        bool baseWithingRangeAttack = ((transform.position - BaseTranform.position).magnitude < detectHouseRange);
        if (PlayerWithingRangeAttack || baseWithingRangeAttack)
        {
            if (IsExploring) {
                if (PlayerWithingRangeAttack) {
                    enemyAnimator.SetTrigger("Attack");
                    //AudioAttack();

                } else if (baseWithingRangeAttack) {
                    isChasingPlayer = true;
                    FindObjetive();
                }
            }
            else
            {
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(1).IsName("Attack") || !enemyAnimator.GetCurrentAnimatorStateInfo(1).IsName("Hurt")) {
                    enemyAnimator.SetTrigger("Attack");
                    //AudioAttack();
                }
                    
            }
        }
        if ((currentState == EnemyState.FIND_OBJETIVE && (transform.position - transformPlayer.position).sqrMagnitude < detectPlayerRange))
        {
            currentState = EnemyState.CHASE;
            isChasingPlayer = true;
        }
        else if ((currentState == EnemyState.CHASE && (transform.position - transformPlayer.position).sqrMagnitude > detectPlayerRange))
        {
            currentState = EnemyState.FIND_OBJETIVE;
            VaryObjetive();
        }
    }
    public void AudioAttack() {
        if (Random.Range(0f, 1f) < 0.5)
        {
            a = AudioManager.Instance.ReturnSFXSound("ZombieAttack");
        }
        else
        {
            a = AudioManager.Instance.ReturnSFXSound("ZombieAttack2");
        }
        MyAudioSource.Stop();
        MyAudioSource.clip = a.clip;
        MyAudioSource.Play();
    }
    void FindObjetive() {
        if (isChasingPlayer)
        {
            currentObjetive = transformPlayer.position;
            agent.SetDestination(currentObjetive);
        }
        else
        {
            if (IsExploring) {
                patroldestinationPoint = BaseTranform.position +
                                (Vector3.right * Random.Range(-patrolArea.x, patrolArea.x)) +
                                (Vector3.forward * (Random.Range(-patrolArea.y, patrolArea.y)));
                agent.SetDestination(patroldestinationPoint);
            }
            else
            {
                currentObjetive = BaseTranform.position;
                agent.SetDestination(currentObjetive);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, detectPlayerRange);
    }
    public void ActivateHurtAnim()
    {
        if (isAlive)
        {
            if (!enemyAnimator.GetCurrentAnimatorStateInfo(1).IsName("Hurt"))
            {
                enemyAnimator.ResetTrigger("Hurt");
                enemyAnimator.ResetTrigger("Attack");
                enemyAnimator.SetTrigger("Hurt");
                a = AudioManager.Instance.ReturnSFXSound("ZombieHurt");
                MyAudioSource.Stop();
                MyAudioSource.clip = a.clip;
                MyAudioSource.Play();
        }
        }
    }
    public bool IsDeathByAcid = false;
    public void ActivateDEATHAnim()
    {
        if (isAlive) {

            currentState = EnemyState.NOTHING;
            isAlive = false;
            enemyAnimator.ResetTrigger("Hurt");
            enemyAnimator.ResetTrigger("Attack");
            enemyAnimator.SetBool("isAlive", false);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            agent.SetDestination(transform.position);
            //Debug.Log("Setting Death");
            enemyAnimator.SetBool("Death", true);
            MyAudioSource.Stop();
            a = AudioManager.Instance.ReturnSFXSound("ZombieD");
            MyAudioSource.clip = a.clip;
            MyAudioSource.Play();
            if (IsDeathByAcid) {
                disolveEffect.StartDisolving();
            }
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.enabled = false;
            Destroy(this.gameObject, 8.0f);
        }
        
    }
    public void AllowMovement()
    {
        if (isAlive)
        {
            agent.isStopped = false;
            enemyAnimator.ResetTrigger("Attack");
        }
    }
}

public enum EnemyState
{ 
    FIND_OBJETIVE,
    CHASE,
    NOTHING
}
