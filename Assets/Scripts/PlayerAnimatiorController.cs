using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    private Rigidbody rb;

    public void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        animator.SetFloat("CharacterSpeed", rb.linearVelocity.magnitude);
        animator.SetBool("IsGrounded", movement.IsGrounded);

        //if (Input.GetButtonUp("Fire1"))
       // {
        //    animator.SetTrigger("doRoll");
       // }
    }
}
