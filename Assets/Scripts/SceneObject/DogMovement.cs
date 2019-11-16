using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    private GameObject dog = null;
    private Rigidbody2D dogRigid = null;
    private Transform dogTrans = null;
    private SpriteRenderer dogRender = null;

    private bool isHitWallL = false;
    private bool isHitWallR = false;
    private float dogSpeed = 2.5f;
    private readonly Vector2 horiz = new Vector2(1.0f, 0.0f);
    private Vector2 vel = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.FindWithTag("DogMove");
        dogTrans = dog.GetComponent<Transform>();
        dogRigid = dog.GetComponent<Rigidbody2D>();
        dogRender = dog.GetComponent<SpriteRenderer>();
        vel.y = 0.0f;
        vel.x = -dogSpeed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        dogRigid.velocity = vel;
        if(isHitWallL)
        {
            dogRender.flipX = isHitWallL;
            isHitWallL = false;
            vel.x = dogSpeed;
        }
        if (isHitWallR)
        {
            isHitWallR = false;
            dogRender.flipX = isHitWallR;
            vel.x = -dogSpeed;
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D cpoint in collision.contacts)
        {
            if ((Vector2.Dot(cpoint.normal, horiz) == 1.0f) && collision.gameObject.tag.Contains("Terrain"))
            {
                isHitWallL = true;
                //Debug.Log("DogWallL");
                return;
            }
            if ((Vector2.Dot(cpoint.normal, horiz) == -1.0f) && collision.gameObject.tag.Contains("Terrain"))
            {
                isHitWallR = true;
                //Debug.Log("DogWallR");
                return;
            }
        }
    }
}
