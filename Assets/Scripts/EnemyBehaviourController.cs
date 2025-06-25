using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System.Collections;

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

    [SerializeField] private GameObject bulletPool;
    [SerializeField] private float bulletSpeed = 5.0f;
    [SerializeField] private float bulletDuration = 5.0f;
    private int specialAttackIteration = 0;
    private bool isAttackDelayActive = false;
    private float ActiveAttackDelay = 2.0f;
    private float ActiveSpecialAttackDelay = 10.0f;
    [SerializeField] private float AttackDelay = 2.0f;
    [SerializeField] private float SpecialAttackDelay = 10.0f;

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
                StartSpecialAttackCountDown();
                Chase();
            }

            IdleOrPatrol();
        }
        else if (currState == EnemyState.CHASE)
        {
            ActiveSpecialAttackDelay -= Time.deltaTime;
            if (ActiveSpecialAttackDelay < 0.0f)
            {
                esc.SpecialAttack();
                SpecialAttack();
                return;
            }

            if (GetDistanceToPlayer().magnitude > chaseDistance)
            {
                esc.OutOfRange();
                IdleOrPatrol();
            }
            Chase();
        }
        else if (currState == EnemyState.ATTACK || currState == EnemyState.SPECIAL_ATTACK)
        {
            return;
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
        Vector3 normalizedDir = Vector3.Normalize(GetDistanceToPlayer());
        moveDir = normalizedDir;
        MoveEnemy();
    }

    private void Attack(GameObject go)
    {
        go.GetComponent<HealthController>().TakeDamage();
        SetAttackDelay();
    }

    private void SpecialAttack()
    {
        //Shoot Bullets
        specialAttackIteration = 0;
        StartCoroutine(BulletHell());
        Debug.Log("Shooting Bullets");
    }

    private void StartSpecialAttackCountDown()
    {
        ActiveSpecialAttackDelay = SpecialAttackDelay;
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

    private IEnumerator BulletHell()
    {

        while (true)
        {
            GameObject bul1 = bulletPool.transform.GetChild(0 + specialAttackIteration * 5).gameObject;
            bul1.SetActive(true);
            bul1.GetComponent<BulletDamage>().ShootBullet(transform.position, GetRandomDirection(), bulletSpeed, bulletDuration);
            GameObject bul2 = bulletPool.transform.GetChild(1 + specialAttackIteration * 5).gameObject;
            bul2.SetActive(true);
            bul2.GetComponent<BulletDamage>().ShootBullet(transform.position, GetRandomDirection(), bulletSpeed, bulletDuration);
            GameObject bul3 = bulletPool.transform.GetChild(2 + specialAttackIteration * 5).gameObject;
            bul3.SetActive(true);
            bul3.GetComponent<BulletDamage>().ShootBullet(transform.position, GetRandomDirection(), bulletSpeed, bulletDuration);
            GameObject bul4 = bulletPool.transform.GetChild(3 + specialAttackIteration * 5).gameObject;
            bul4.SetActive(true);
            bul4.GetComponent<BulletDamage>().ShootBullet(transform.position, GetRandomDirection(), bulletSpeed, bulletDuration);
            GameObject bul5 = bulletPool.transform.GetChild(4 + specialAttackIteration * 5).gameObject;
            bul5.SetActive(true);
            bul5.GetComponent<BulletDamage>().ShootBullet(transform.position, GetRandomDirection(), bulletSpeed, bulletDuration);

            if (specialAttackIteration >= 4)
            {
                StartSpecialAttackCountDown();
                esc.ChasePlayer();
                yield break;
            }

            specialAttackIteration++;

            yield return new WaitForSeconds(0.2f);
        }

        
    }

    private Vector2 GetRandomDirection()
    {
        Vector2 dir = Vector2.up; //vector with a lenght of one
        return RotateVector(dir, Random.Range(0.0f, 360.0f));
    }

    private Vector2 RotateVector(Vector2 vec, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        float newX = vec.x * cos - vec.y * sin;
        float newY = vec.x * sin + vec.y * cos;

        return new Vector2(newX, newY);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Attack(col.gameObject);
        esc.Attack();
    }
}
