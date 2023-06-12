using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 2f; // Delay in seconds before the object gets destroyed

    private void Start()
    {
        // Invoke the DestroyObject function after the specified delay
        Invoke("DestroyObject", delay);
    }

    private void DestroyObject()
    {
        // Destroy the game object
        Destroy(gameObject);
    }
}
