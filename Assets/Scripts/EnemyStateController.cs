using UnityEngine;
using System.Collections.Generic;

public enum EnemyState
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK,
    SPECIAL_ATTACK
}

public class EnemyStateController : MonoBehaviour
{
    private EnemyState state = EnemyState.IDLE;
    private Animator animator;

    [SerializeField] private EnemyBehaviourController ebc;
    [SerializeField] private List<Color> stateColors;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public EnemyState GetState()
    {
        return state;
    }

    public void ChasePlayer()
    {
        state = EnemyState.CHASE;
        DisableAllAnimatorBools();
        animator.SetBool("IsChasing", true);
    }

    public void Attack()
    {
        state = EnemyState.ATTACK;
        DisableAllAnimatorBools();
        animator.SetBool("IsAttacking", true);
    }

    public void SpecialAttack()
    {
        state = EnemyState.SPECIAL_ATTACK;
        DisableAllAnimatorBools();
        animator.SetBool("IsSpecialAttacking", true);
    }

    // Randomly choose from idle and patrol when player is out of range
    public void OutOfRange()
    {
        DisableAllAnimatorBools();

        int randint = Random.Range(0, 2);
        if (randint == 0)
        {
            state = EnemyState.IDLE;
            animator.SetBool("IsIdle", true);
        }
        else
        {
            state = EnemyState.PATROL;
            animator.SetBool("IsPatroling", true);
        }
    }

    private void DisableAllAnimatorBools()
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsPatroling", false);
        animator.SetBool("IsChasing", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsSpecialAttacking", false);
    }
}
