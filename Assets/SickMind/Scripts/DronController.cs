using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DronController : MonoBehaviour
{
    bool isAlive;
    [SerializeField] Animator myAnimator;
    public NavMeshAgent agent;
    public Transform transformPlayer;
    [SerializeField] float detectRangeAttack;
    AudioSource myAudioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        myAudioSource=this.GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        hasShot = false;
        isAlive = true;
        myAnimator.SetBool("Alive", true);
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {
            agent.SetDestination(transformPlayer.position);
            myAnimator.SetFloat("Speed", agent.velocity.sqrMagnitude);
            CheckObjetiveDistance();
        }
    }
    bool hasShot=false;
    // Update is called once per frame
    void CheckObjetiveDistance()
    {
        if (((transform.position - transformPlayer.position).magnitude < detectRangeAttack))
        {
            if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
                if (!hasShot) {
                    Invoke("SendShot", 2);
                    hasShot = true;
                }
                
            }
                
        }
    }
    void SendShot() {
        myAnimator.SetTrigger("Attack");
        hasShot = false;
    }
    public void ActivateDEATHAnim() {
        if (isAlive)
        {
            isAlive = false;
            myAudioSource.Stop();
            myAnimator.ResetTrigger("Attack");
            myAnimator.SetBool("Alive", false);

            this.gameObject.GetComponent<BoxCollider>().enabled = false;

            agent.SetDestination(transform.position);
//            Debug.Log("Setting Death Drone");

            myAnimator.SetTrigger("Death");
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.enabled = false;
            Destroy(this);
            //Destroy(this.gameObject, 8.0f);
        }
    }
    public void ActivateHurtAnim() {
        
    }
}
