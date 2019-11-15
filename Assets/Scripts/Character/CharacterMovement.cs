using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private GameObject playerObject = null;
    private Rigidbody2D rigidbodyObject = null;
    private Transform trans = null;
    private SpriteRenderer rendererObject = null;

    private const float speed = 4.0f;
    private bool isHeadingRight = true;
    private bool isJumpping = false;
    private bool isReleasedJump = true;
    private bool isJumpPressed = false;
    private Vector2 velco = new Vector2();
    public void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        trans = playerObject.GetComponent<Transform>();
        rigidbodyObject = playerObject.GetComponent<Rigidbody2D>();
        rendererObject = playerObject.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        isJumpPressed = Input.GetKey(KeyCode.W);
    }

    public void FixedUpdate()
    {
        float inXAxis = Input.GetAxis("Horizontal");
        float inYAxis = isJumpPressed ? 1.0f : 0.0f;
        float deltaX = inXAxis * speed;
        if (deltaX != 0.0f) isHeadingRight = inXAxis > 0.0f;
        rendererObject.flipX = !isHeadingRight;
        Vector2 oldVel = rigidbodyObject.velocity;
        if (inYAxis > 0.0f)
        {
            if (!isJumpping&&isReleasedJump)
            {
                isJumpping = true;
                isReleasedJump = false;
                oldVel.y = 3.0f;
                rigidbodyObject.AddForce(new Vector2(0.0f, 500.0f));
            }
        }
        else
        {
            isReleasedJump = true;
        }
        velco.x = deltaX;
        velco.y = oldVel.y;
        rigidbodyObject.velocity = velco;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Terrain"))
        {
            isJumpping = false;
            Debug.Log("Touched"+Time.time.ToString());
        }
    }

}
