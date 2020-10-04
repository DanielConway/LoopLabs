using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject goToSpawn;

    public GameObject spawnedGO;

    public Transform spawnPoint;

    public float spawnInterval;

    public bool waitingToSpawn = false;

    void Start()
    {
        GameObject temp = Instantiate(goToSpawn, spawnPoint.transform.position,Quaternion.identity);
        spawnedGO = temp;
    }
    private void Update()
    {
        if (spawnedGO != null && waitingToSpawn == false)
        {
            if (Vector3.Distance(spawnedGO.transform.position , spawnPoint.position) > 1)
            {
                StartCoroutine(spawnTicker());
            }
        }
    }

    IEnumerator spawnTicker()
    {
        waitingToSpawn=true;
        yield return new WaitForSeconds(spawnInterval);
        GameObject temp = Instantiate(goToSpawn, spawnPoint.transform.position,Quaternion.identity);
        spawnedGO = temp;
        waitingToSpawn=false;
    }
}
