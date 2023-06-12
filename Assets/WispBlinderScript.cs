using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispBlinderScript : MonoBehaviour
{
    private GameObject globalLight;
    private LogicScript logic;

    private float blindnessDuration = 3f;
    private bool isBlind = false;
    // Start is called before the first frame update
    void Start()
    {
        // Find and store the primary 2D light source in the scene
        globalLight = GameObject.FindGameObjectWithTag("GlobalLight");
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartBlindness();
        }
    }

    private void StartBlindness()
    {
        if (!isBlind && globalLight != null)
        {
            isBlind = true;
            globalLight.SetActive(false); // Turn off the 2D light
            logic.StartCoroutine(StopBlindness(blindnessDuration));
        }
    }

    IEnumerator StopBlindness(float duration)
    {
        yield return new WaitForSeconds(duration);

        isBlind = false;
        if (globalLight != null)
        {
            globalLight.SetActive(true); // Restore the 2D light intensity
        } 
    }
}
