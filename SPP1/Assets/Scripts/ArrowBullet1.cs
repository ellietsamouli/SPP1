using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBullet1 : MonoBehaviour
{
    public float life = 3;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is not the bullet itself
        if (collision.gameObject != gameObject)
        {
            Destroy(collision.gameObject); // Destroy the collided object (assuming it's not the bullet)
        }

        // Check if the bullet is not colliding with the character
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }


}
