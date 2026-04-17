using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum States
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
    [SerializeField] public States state;

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
                // IEnumerator DoAttack()
                break;
            case States.recovery:
                // IEnumerator Recover()
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
        agent.enabled = true;
        agent.SetDestination(new Vector3(
                player.position.x,
                player.position.y,
                player.position.z
        ));

        if (Vector3.Distance(transform.position, player.position) >= sightRange)
        {
            wanderTimer = 1;
            state = States.wander;
        }
        else if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            state = States.attack;
            StartCoroutine(DoAttack());
        }
    }

    IEnumerator DoAttack()
    {
        agent.enabled = false;
        rb.AddForce(10 * Vector3.Normalize(new Vector3(
               player.position.x - transform.position.x,
               (player.position.y - transform.position.y) * 2,
               player.position.z - transform.position.z
        )) + new Vector3(0, 5, 0), ForceMode.Impulse);
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Physics.Raycast(transform.position, Vector3.down, 1.1f));
        state = States.recovery;
        StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(recoveryTime - 0.25f);
        state = States.pursue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FPSController>())
        {
            if (state == States.attack)
            {
                Debug.Log("Damaged!");
            }
            else
            {
                Debug.Log("Collided w/o Damage.");
            }
        }
    }
}
