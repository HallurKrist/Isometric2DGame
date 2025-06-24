using UnityEngine;

public class EnemyBehaviourController : MonoBehaviour
{
    [SerializeField] private EnemyStateController esc;

    [SerializeField] private GameObject player;

    [SerializeField] float chaseDistance = 10.0f;
    [SerializeField] float attackDistance = 0.5f;

    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        EnemyState currState = esc.GetState();

        if ((currState == EnemyState.IDLE) || (currState == EnemyState.PATROL))
        {
            if (GetDistanceToPlayer().magnitude <= chaseDistance)
            {
                esc.ChasePlayer();
                Chase();
            }

            IdleOrPatrol();
        }
        else if (currState == EnemyState.CHASE)
        {
            if (GetDistanceToPlayer().magnitude > chaseDistance)
            {
                esc.OutOfRange();
                IdleOrPatrol();
            }
            Chase();
        }
        else if (currState == EnemyState.ATTACK)
        {
            Attack();
        }
        else
        {
            Debug.LogError("current state is not found");
        }
    }

    private void IdleOrPatrol()
    {
        if (esc.GetState() != EnemyState.IDLE)
        {
            //Patrol
        }
    }

    private void Chase()
    {
        //Chase behaviour 

        if (GetDistanceToPlayer().magnitude < attackDistance)
        {
            esc.Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Enemy Attack!");
        esc.ChasePlayer();
    }

    public Vector3 GetDistanceToPlayer()
    {
        return player.transform.position - transform.position;
    }
}
