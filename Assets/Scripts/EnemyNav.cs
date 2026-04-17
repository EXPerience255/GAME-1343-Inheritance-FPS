using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

enum States
{
    wander,
    pursue,
    attack,
    recovery
}

public class EnemyNav : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform player;

    [SerializeField] float wanderRange;
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;
    [SerializeField] float recoveryTime;
    [SerializeField] States state;

    Vector3 startingLocation;
    float wanderTimer = 0;

    void Start()
    {
        startingLocation = transform.position;
        state = States.wander;
    }

    void Update()
    {
        switch (state)
        { 
            case States.wander:
                UpdateWander();
                break;
            case States.pursue:
                UpdatePersue();
                break;
            case States.attack:
                //UpdateAttack();
                break;
            case States.recovery:
                //UpdateRecovery();
                break;
        }
    }

    void UpdateWander()
    {
        if (wanderTimer <= 0)
        {
            wanderTimer = 3;
            agent.SetDestination(new Vector3(
                startingLocation.x + Random.Range(-wanderRange, wanderRange),
                startingLocation.y,
                startingLocation.z + Random.Range(-wanderRange, wanderRange)
            ));
        }
        else { wanderTimer -= Time.deltaTime; }
        
        if (Vector3.Distance(transform.position, player.position) <= sightRange)
        {
            state = States.pursue;
        }
    }

    void UpdatePersue()
    {
        agent.SetDestination(new Vector3(
                player.position.x,
                startingLocation.y,
                player.position.z
        ));

        if (Vector3.Distance(transform.position, player.position) >= sightRange)
        {
            wanderTimer = 1;
            state = States.wander;
        }
    }
}
