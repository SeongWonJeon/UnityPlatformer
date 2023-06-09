using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
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
        // 이방법은 물체에 부딪혀도 최고속력으로 날아가는게 있어서 아래의 방법으로 만들었다.
        /* if (rb.velocity.x > maxSpeed)
         *    rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
         */

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

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);  
        Debug.DrawRay(transform.position, new Vector3(hit.point.x, hit.point.y,0)-transform.position, Color.red);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            isGround = true;
            anim.SetBool("IsGrounded", true);
        }
        else
        {
            isGround = false;
            anim.SetBool("IsGrounded", false);
        }
        Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.green);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Monster"))
        {
            // 여기서는 몬스터랑 충돌했을 때
        }
        else if ("item을 하면 item이랑 충돌했을 때")
    }*/

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        anim.SetBool("IsGrounded", true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
        anim.SetBool("IsGrounded", false);
    }*/
}
