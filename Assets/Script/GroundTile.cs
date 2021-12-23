using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] List<GameObject> wayPoints;
    [SerializeField] int coinsToSpawn = 5;
    [SerializeField]  int powerUpSpawmRate;
    [SerializeField] GameObject coinPrefabs;
    [SerializeField] List<GameObject> PickUpPrefabs;

    void Start()
    {
        groundSpawner = FindObjectOfType<GroundSpawner>();
    }

    void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject,20);
    }


    void Update()
    {
        
    }

    public void SpawnObtacle()
    {
        int obstacleSpawnIndex = Random.Range(2,5);
        int obstacleIndex = Random.Range(0, obstacles.Count);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        Instantiate(obstacles[obstacleIndex], spawnPoint.position, Quaternion.identity, transform);

        switch(obstacleSpawnIndex)
        {
            case 2: 
            Instantiate(wayPoints[0],transform.GetChild(5).transform.position,Quaternion.identity);
            break;
            case 3: 
            Instantiate(wayPoints[1],transform.GetChild(6).transform.position,Quaternion.identity);
            break;
            case 4: 
            Instantiate(wayPoints[2],transform.GetChild(7).transform.position,Quaternion.identity);
            break;
        }
    }

    public void SpawnCoins()
    {
        for(int i = 0; i < coinsToSpawn;i++)
        {
            GameObject temp = Instantiate(coinPrefabs,transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    public void SpawnPowerUp()
    {
        for(int i = 0; i < powerUpSpawmRate;i++)
        {
            GameObject temp = Instantiate(PickUpPrefabs[Random.Range(0,PickUpPrefabs.Count)],transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        if(point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }

    
}
