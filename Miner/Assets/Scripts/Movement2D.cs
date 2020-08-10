using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private float jumpForce = 2f;

    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        Jump();
        FlipSprite();
    }

    private void HorizontalMovement()
    {
        var movementInput = Input.GetAxis("Horizontal");
        var movementValue = movementInput * movementSpeed * Time.deltaTime * 100;

        var velocity = _rigidBody2D.velocity;
        Vector2 playerVelocity = new Vector2(movementValue, velocity.y);
        velocity = playerVelocity;
        _rigidBody2D.velocity = velocity;

        var playerHasHorizontalSpeed = Mathf.Abs(velocity.x) > Mathf.Epsilon;
        _animator.SetBool(IsMoving, playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rigidBody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FlipSprite()
    {
        var playerHasHorizontalSpeed = Mathf.Abs(_rigidBody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(_rigidBody2D.velocity.x), 1f);
        }
    }
}