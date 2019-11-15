using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject player;
    public bool following = true;
    private Vector3 offset;
    private Vector3 tmp;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (!following) return;
        tmp = player.transform.position + offset;
        //if (tmp.y > 1.0f) tmp.y = 1.0f;
        transform.position = tmp;
    }
}
