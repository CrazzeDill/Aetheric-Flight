using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawnerScript : MonoBehaviour
{
    public GameObject bat;
    public GameObject wisp;

    public float spawnTimeMod = 0f;
    private float minY = -3.5f;
    private float maxY = 3f;
    private LogicScript logic;

    private bool wispStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        StartCoroutine(SpawnBats());
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.elapsedTime >= 30 && !wispStarted)
        {
            StartCoroutine(SpawnWisp());
            wispStarted = true;
        }
    }

    public void ActivateHateReact()
    {
        spawnTimeMod += 0.5f;
    }
        

    IEnumerator SpawnBats()
    {
        float delay = Random.Range(2-2*spawnTimeMod,5-5*spawnTimeMod); // Random time delay 
        yield return new WaitForSeconds(delay);

        // Spawn the prefab
        GameObject prefabInstance = Instantiate(bat, new Vector3(10f,Random.Range(minY,maxY),0f), Quaternion.identity);

        // Start the coroutine again for the next spawn
        StartCoroutine(SpawnBats());
    }

    IEnumerator SpawnWisp()
    {
        float delay = Random.Range(5 - 5 * spawnTimeMod, 10 - 10 * spawnTimeMod); // Random time delay 
        yield return new WaitForSeconds(delay);

        // Spawn the prefab
        GameObject prefabInstance = Instantiate(wisp, new Vector3(10f, Random.Range(minY, maxY), 0f), Quaternion.identity);

        // Start the coroutine again for the next spawn
        StartCoroutine(SpawnWisp());
    }
}
