using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WispScript : MonoBehaviour
{
    private float stopMove = -10f;
    private Vector3 targetPosition;
    private float moveSpeed;


    private void Start()
    {
        

        // Define the target position
        targetPosition = new Vector3(stopMove, transform.position.y, transform.position.z);

        // Move the instantiated GameObject gradually to the target position
        moveSpeed = 5f; // Adjust the speed of movement

    }

    private void Update()
    {
        
        if (transform != null && transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        }
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WeaponHit"))
        {
            //Destroy(boxCollider);
            Destroy(gameObject);
            //Istantiate(destroyEffect, transform.position, Quaternion.identity);
        }
    }
}
