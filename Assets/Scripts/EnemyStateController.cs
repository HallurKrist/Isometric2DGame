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

    [SerializeField] private EnemyBehaviourController ebc;

    [SerializeField] private List<Color> stateColors;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = stateColors[0];
    }

    public EnemyState GetState()
    {
        return state;
    }

    public void ChasePlayer()
    {
        state = EnemyState.CHASE;
        sr.color = stateColors[2];
    }

    public void Attack()
    {
        state = EnemyState.ATTACK;
        sr.color = stateColors[3];
    }

    // Randomly choose from idle and patrol when player is out of range
    public void OutOfRange()
    {
        int randint = Random.Range(0, 2);
        if (randint == 0)
        {
            state = EnemyState.IDLE;
            sr.color = stateColors[0];
        }
        else
        {
            state = EnemyState.PATROL;
            sr.color = stateColors[1];
        }
    }
}
