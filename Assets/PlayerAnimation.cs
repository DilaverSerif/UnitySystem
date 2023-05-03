using System;
using _SYSTEMS_._Character_Controller_;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private void Awake()
    {
        if(animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        animator.SetFloat(MoveSpeed, InputController.Instance.MovementDirection().magnitude);
    }
}
