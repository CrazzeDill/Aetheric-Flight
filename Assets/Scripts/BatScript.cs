using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float stopMove = -2f;
    private Vector3 targetPosition;
    private float moveSpeed;
    public ParticleSystem destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Define the target position
        targetPosition = new Vector3(stopMove, transform.position.y, transform.position.z);

        // Move the instantiated GameObject gradually to the target position
        moveSpeed = 5f; // Adjust the speed of movement
    }

    // Update is called once per frame
    void Update()
    {

        if (transform != null && transform.position != targetPosition)
        {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        }
    }

    private void DestroyMe()
    {
        Destroy(gameObject);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WeaponHit"))
        {
            Destroy(boxCollider);
            animator.Play("Bat_Death");
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
    }

}
