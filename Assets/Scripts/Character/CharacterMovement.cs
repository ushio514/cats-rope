using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    private GameObject playerObject = null;
    private Rigidbody2D rigidbodyObject = null;
    private Transform trans = null;
    private SpriteRenderer rendererObject = null;

    private const float speed = 5.0f;
    private bool isProcInput = true;
    private bool isHeadingRight = true;
    private bool isJumpping = false;
    private bool isReleasedJump = true;
    private bool isJumpPressed = false;
    private Vector2 velco = new Vector2();
    private readonly Vector2 verVec = new Vector2(0.0f, 1.0f);
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
        if (!isProcInput) return;
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

    public void OnCollisionStay2D(Collision2D collision)
    {
        foreach(ContactPoint2D cpoint in collision.contacts)
        {
            // Debug.DrawRay(cpoint.point, cpoint.normal, Color.red, 10.0f);
            if (Mathf.Abs(Vector2.Dot(cpoint.normal, verVec)) < 0.0f) 
            {
                return;
            }
        }
        if (collision.gameObject.tag.Contains("Terrain"))
        {
            if (rigidbodyObject.velocity.y != 0.0f) return;
            isJumpping = false;
            // Debug.Log("Touched"+Time.time.ToString());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Thorn"))
        {
            Debug.Log("DIED!");
            isProcInput = false;
            rigidbodyObject.velocity = new Vector2(0.0f, 7.0f);
            playerObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.FindWithTag("MainCamera").GetComponent<CameraFollower>().following = false;
            StartCoroutine("DeathDelay");
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Start");
    }
}
