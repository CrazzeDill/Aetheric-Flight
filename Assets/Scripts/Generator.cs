using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject pipe;
    public GameObject grid;
    public float spawnRate = 5;
    private float timer = 0;
    public float heightOffset = 3;
    public float moveMod = 1f;
    private float playTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            timer = 0;
        }

        if (playTime < 10)
        {
            playTime += Time.deltaTime;
        }
        else
        {
            MultiplyMoveSpeed(1.05f);
            playTime = 0;
        }
    }

    public void MultiplySpawnRate(float mod)
    {
        spawnRate *= mod;
    }
    public void MultiplyMoveSpeed(float mod)
    {
        moveMod *= mod;
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highstPoint = transform.position.y + heightOffset;

        GameObject newPipe = Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highstPoint)), transform.rotation);
        newPipe.transform.parent = grid.transform;
        newPipe.GetComponent<ColumnMoveScript>().moveSpeed = 5f * moveMod;
    }
}
