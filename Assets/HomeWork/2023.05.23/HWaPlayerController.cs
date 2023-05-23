using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HWaPlayerController : MonoBehaviour
{
    [SerializeField] private float movePower;
    [SerializeField] private float jumpPower;
    [SerializeField] private float maxSpeed;
    private Rigidbody2D rb;
    public bool isGround;
    private Vector2 inputDir;
    private Animator anim;
    private new SpriteRenderer renderer;

    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Move();
    }
    private void FixedUpdate()
    {
        GroundCheck();
    }
    private void Move()
    {
        // 움직이는 최고속력
        if (inputDir.x < 0 && rb.velocity.x > -maxSpeed)
            rb.AddForce(Vector2.right * inputDir.x * movePower, ForceMode2D.Force);
        else if (inputDir.x > 0 && rb.velocity.x < maxSpeed)
            rb.AddForce(Vector2.right * inputDir.x * movePower, ForceMode2D.Force);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
        anim.SetFloat("MoveSpeed", Mathf.Abs(inputDir.x)); // mathf.Abs = 절대값
        if (inputDir.x > 0)
            renderer.flipX = false;
        else if (inputDir.x < 0)
            renderer.flipX = true;
    }

    private void OnJump(InputValue value)
    {
        Jump();
    }
    // 콜리더를 사용하여 땅에 닿았는지 닿지않았는지 확인한다
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        isGround = true;
        anim.SetBool("IsGrounded", true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
        anim.SetBool("IsGrounded", false);
    }
    // 레이저형태의 콜리더를 생성해서 땅에 닿았는지 안닿았는지 확인한다
    private void GroundCheck()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);
        if (raycastHit.collider != null)
        {
            isGround = true;
            anim.SetBool("IsGrounded", true);
            Debug.DrawRay(transform.position, new Vector3(raycastHit.point.x, raycastHit.point.y, 0), Color.red);
        }
        else
        {
            isGround = false;
            anim.SetBool("ISGrounded", false);
            Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.green);
        }
        
    }
}
