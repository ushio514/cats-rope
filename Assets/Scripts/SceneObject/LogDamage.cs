using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDamage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("dog"))
        {
            GameObject dog = GameObject.Find("dog");
            Destroy(dog);
        }
    }

}
