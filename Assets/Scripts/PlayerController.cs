using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float JumpImpulse;

    [SerializeField]
    private Collider2D BottomCollider;

    [SerializeField]
    private LayerMask PlatformLayerMask;

    private Rigidbody2D Rigidbody;
    private Animator Animator;
    private SpriteRenderer SpriteRenderer;

    private float Direction;
    private bool HasJumped;
    private bool IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimentacaoHorizontal();
        FlipAnimation();
        MovimentacaoVertical();
        JumpAnimation();
    }

    void FixedUpdate()
    {      
        float yVelocity = Rigidbody.velocity.y;
        if(HasJumped)
        {
            yVelocity = JumpImpulse;
            HasJumped = false;
        }

        Rigidbody.velocity = new Vector2(Direction * Speed * Time.deltaTime, yVelocity);
    }

    private void MovimentacaoHorizontal()
    {
        Direction = Input.GetAxisRaw("Horizontal");
        Animator.SetFloat("Speed", Mathf.Abs(Direction));
    }

    private void FlipAnimation()
    {
        if (Direction > 0)
        {
            SpriteRenderer.flipX = false;
        }
        else if (Direction < 0)
        {
            SpriteRenderer.flipX = true;
        }
    }

    private void MovimentacaoVertical()
    {
        SetGrounded();
        DebugMovimentacaoVertical();

        if(Input.GetKeyDown("space") && IsGrounded)
        {
            HasJumped = true;
        }
    }

    private void SetGrounded()
    {
        float rayDistance = GetRayDistance();
        var rayCastHit = Physics2D.Raycast(BottomCollider.bounds.center, Vector2.down, rayDistance, PlatformLayerMask);
        IsGrounded = (rayCastHit.collider != null);
    }

    private void DebugMovimentacaoVertical()
    {
        float rayDistance = GetRayDistance();
        Color rayColor = (IsGrounded) ? Color.red : Color.green;
        Debug.DrawRay(BottomCollider.bounds.center, Vector2.down * rayDistance, rayColor);
    }

    private float GetRayDistance()
    {
        float extraHeightCheck = 0.1f;
        return BottomCollider.bounds.extents.y + extraHeightCheck;
    }

    private void JumpAnimation()
    {
        Animator.SetBool("IsGrounded", IsGrounded);
        //Animator.SetFloat("JumpImpulse", Rigidbody.velocity.y);
    }
}
