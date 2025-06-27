using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAttack(InputValue value)
    {
        animator.SetBool("IsAttacking", true);
        animator.SetBool("IsIdle", false);
    }

    public void OnAttackEnd()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsIdle", true);
    }
}
