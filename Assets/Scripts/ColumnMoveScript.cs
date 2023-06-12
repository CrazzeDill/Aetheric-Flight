using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    private float maxSpeed;
    public float deadZone = -25;
    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 16f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * Mathf.Min(moveSpeed,maxSpeed)) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
