using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 10f;   
    [SerializeField] private GameObject spawnRedEnemy;
    [SerializeField] private GameObject spawnBlueEnemy;
    [SerializeField] private Transform spawnObject;

    private int counterRedEnemy = 1;
    private int spawnCount = 0;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (spawnCount < 30) 
        {
            yield return new WaitForSeconds(spawnDelay);
            if (spawnDelay > 6f)
            {
                Instantiate(spawnRedEnemy, randomPosition(0.1f), Quaternion.identity, spawnObject);
                spawnDelay -= 2f;
            }
            else if (spawnDelay <= 6f)
            {
                Instantiate(spawnRedEnemy, randomPosition(0.1f), Quaternion.identity, spawnObject);
                counterRedEnemy++;
                if (counterRedEnemy >= 4)
                {
                    Instantiate(spawnBlueEnemy, randomPosition(0.32f), Quaternion.identity, spawnObject);
                    counterRedEnemy = 0;
                }
            }
            spawnCount++;       
        } 
    }

    private Vector3 randomPosition(float yValue)
    {
        float minRadius = 4.5f;
        float naxRadius = 1f;
        float randomRadius = Random.Range(minRadius, naxRadius);
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float randomX = randomRadius * Mathf.Cos(randomAngle);
        float randomZ = randomRadius * Mathf.Sin(randomAngle);

        Vector3 randomPosition = new Vector3(randomX, yValue, randomZ);
        return randomPosition;
    }
    
}
