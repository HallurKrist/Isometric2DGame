using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{

    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnAttack(InputValue value)
    {
        Debug.Log("playerAttack");
        animator.SetBool("IsAttacking", true);
        animator.SetBool("IsIdle", false);
    }

    public void OnAttackEnd()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsIdle", true);
    }
}
