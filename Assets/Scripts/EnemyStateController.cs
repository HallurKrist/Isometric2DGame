using UnityEngine;
using System.Collections.Generic;

public enum EnemyState
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyStateController : MonoBehaviour
{
    private EnemyState state = EnemyState.IDLE;

    private SpriteRenderer sr;
    private Animator animator;

    [SerializeField] private EnemyBehaviourController ebc;

    [SerializeField] private List<Color> stateColors;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //sr.color = stateColors[0];
    }

    public EnemyState GetState()
    {
        return state;
    }

    public void ChasePlayer()
    {
        state = EnemyState.CHASE;
        //sr.color = stateColors[2];

        DisableAllAnimatorBools();
        animator.SetBool("IsChasing", true);
    }

    public void Attack()
    {
        state = EnemyState.ATTACK;
        //sr.color = stateColors[3];

        DisableAllAnimatorBools();
        animator.SetBool("IsAttacking", true);
    }

    // Randomly choose from idle and patrol when player is out of range
    public void OutOfRange()
    {
        DisableAllAnimatorBools();

        int randint = Random.Range(0, 2);
        if (randint == 0)
        {
            state = EnemyState.IDLE;
            //sr.color = stateColors[0];

            animator.SetBool("IsIdle", true);
        }
        else
        {
            state = EnemyState.PATROL;
            //sr.color = stateColors[1];

            animator.SetBool("IsPatroling", true);
        }
    }

    private void DisableAllAnimatorBools()
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsPatroling", false);
        animator.SetBool("IsChasing", false);
        animator.SetBool("IsAttacking", false);
    }
}
