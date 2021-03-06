﻿using System;
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
            transform.localScale = new Vector3(1,1,1);
        }
        else if (Direction < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
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
        var rayCastHit = Physics2D.BoxCast(BottomCollider.bounds.center, BottomCollider.bounds.size, 0f, Vector2.down, rayDistance, PlatformLayerMask);
        IsGrounded = (rayCastHit.collider != null);
    }

    private void DebugMovimentacaoVertical()
    {
        // float rayDistance = GetRayDistance();
        // Color rayColor = (IsGrounded) ? Color.red : Color.green;
    }

    private float GetRayDistance()
    {
        return 0.2f;
    }

    private void JumpAnimation()
    {
        Animator.SetBool("IsGrounded", IsGrounded);
        //Animator.SetFloat("JumpImpulse", Rigidbody.velocity.y);
    }
}
