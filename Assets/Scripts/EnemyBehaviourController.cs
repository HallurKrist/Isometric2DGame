using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class EnemyBehaviourController : MonoBehaviour
{
    [SerializeField] private EnemyStateController esc;

    [SerializeField] private GameObject player;

    [SerializeField] float chaseDistance = 10.0f;
    [SerializeField] float attackDistance = 0.5f;

    [SerializeField] private GameObject[] waypoints;
    private int index = 0;

    [SerializeField] private float speed = 5.0f;

    private GameObject target = null;

    private Rigidbody2D rb;

    private Vector2 moveDir = Vector2.zero;

    private bool isAttackDelayActive = false;
    private float ActiveAttackDelay = 2.0f;
    [SerializeField] private float AttackDelay = 2.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ResetVelocity();

        if (isAttackDelayActive)
        {
            ActiveAttackDelay -= Time.deltaTime;
            if (ActiveAttackDelay <= 0)
            {
                isAttackDelayActive = false;
                esc.ChasePlayer();
            }
            return;
        }

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
            Debug.Log("Partoling");
            
            if (target == null) target = waypoints[index];

            Vector3 normalizedDir = Vector3.Normalize(GetDistanceToTarget());
            moveDir = normalizedDir;
            MoveEnemy();

            Debug.Log(moveDir);

            // If its within a certain distance we expect it to be at the waypoint and therefore has to go to the next
            if (GetDistanceToTarget().magnitude < 0.1f)
            {
                NextWaypoint();
            }
        }
    }

    private void Chase()
    {
        //Chase behaviour 

        Debug.Log("Chasing");

        Vector3 normalizedDir = Vector3.Normalize(GetDistanceToPlayer());
        moveDir = normalizedDir;
        MoveEnemy();

        /*if (GetDistanceToPlayer().magnitude < attackDistance)
        {
            esc.Attack();
        }*/
    }

    private void Attack()
    {
        Debug.Log("Enemy Attack!");
        SetAttackDelay();
    }

    private void MoveEnemy()
    {
        rb.linearVelocity = new Vector2(moveDir.x * speed, moveDir.y * speed);
    }

    private void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    private void NextWaypoint()
    {
        index++;
        index = index % waypoints.Length;
        target = waypoints[index];
    }

    private void SetAttackDelay()
    {
        isAttackDelayActive = true;
        ActiveAttackDelay = AttackDelay;
    }

    public Vector3 GetDistanceToPlayer()
    {
        return player.transform.position - transform.position;
    }

    public Vector3 GetDistanceToTarget()
    {
        return target.transform.position - transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        esc.Attack();
    }
}
