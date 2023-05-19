using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    
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
        animator.SetFloat(MoveSpeed, characterController.velocity.magnitude);
    }
}
