using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> groundTile;
    Vector3 nextSpawnPoint;
    void Start()
    {
        for(int i = 0; i < 10;i++)
        {
            if(i < 3)
            {
                SpawnTile(false);
            }
            else
            {
                SpawnTile(true);
            }
        }
    }

    public void SpawnTile(bool spawnItems)
    {
        int indexGr = Random.Range(0,groundTile.Count);
        GameObject temp = Instantiate(groundTile[indexGr],nextSpawnPoint,Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        if(spawnItems)
        {
            temp.GetComponent<GroundTile>().SpawnCoins();
            temp.GetComponent<GroundTile>().SpawnObtacle();
            temp.GetComponent<GroundTile>().SpawnPowerUp();
        }
    }

    void Update()
    {
        
    }
}
