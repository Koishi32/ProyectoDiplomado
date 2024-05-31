using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DumbZombie : MonoBehaviour
{
    Animator enemyAnimator;
    NavMeshAgent agent;
    public Vector2 patrolArea;
    Vector3 patroldestinationPoint;
    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetBool("Death", false);
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator.SetBool("isAlive", true);
    }
    void Start()
    {
        VaryPatrol();
        InvokeRepeating("VaryPatrol", 0.5f,4);
        agent.SetDestination(patroldestinationPoint);
    }
    void VaryPatrol()
    {
        patroldestinationPoint = transform.position +
                            (Vector3.right * Random.Range(-patrolArea.x, patrolArea.x)) +
                            (Vector3.forward * (Random.Range(-patrolArea.y, patrolArea.y)));
        agent.SetDestination(patroldestinationPoint);

    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
}
