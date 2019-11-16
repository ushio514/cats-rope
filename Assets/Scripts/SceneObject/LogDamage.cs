using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDamage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Thorn")||collision.gameObject.tag.Contains("DogMove"))
        {
            Destroy(collision.gameObject);
        }
    }

}
