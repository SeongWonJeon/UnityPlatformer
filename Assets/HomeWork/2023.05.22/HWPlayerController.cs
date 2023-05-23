using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HWPlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector2 inputDir;
    private SpriteRenderer render;
    [SerializeField] private float movePower;
    [SerializeField] private float maxSpeed;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        rigidBody.AddForce(Vector2.right * inputDir.x * movePower * Time.deltaTime, ForceMode2D.Force);
    }
    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();

        if (inputDir.x > 0)
            render.flipX = false;
        else if (inputDir.x < 0)
            render.flipX = true;
    }

}
