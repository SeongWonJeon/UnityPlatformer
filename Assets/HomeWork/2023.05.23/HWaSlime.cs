using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class HWaSlime : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform groundChack;
    [SerializeField] LayerMask GroundMask;

    private Rigidbody slime;

    private void Awake()
    {
        slime = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Move();
        if (!IsGround())
        {
            Trun();
        }
    }

    private void Trun()
    {
        transform.Rotate(Vector3.up, 180);
    }

    private void Move()
    {
        slime.velocity = new Vector2(transform.right.x * -moveSpeed, slime.velocity.y);
    }
    private bool IsGround()
    {
        return Physics2D.Raycast(groundChack.position, Vector3.down, 1.5f, GroundMask);
    }
}
