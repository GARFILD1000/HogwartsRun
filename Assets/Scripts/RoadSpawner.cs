using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{

    public GameObject[] roadBlockPrefabs;
    public GameObject startBlock;
    public Transform player;
    public CharacterMovement characterMovement;

    List<GameObject> currentBlocks = new List<GameObject>();
    float startBlockXPosition = 0;
    float blockXPosition = 0;
    float blockLength = 0;
    int blocksCount = 0;

    void Start()
    {
        blockLength = 34.5f;
        startBlockXPosition = player.position.x;
        blocksCount = 10; 
        StartGame();
    }
    
    void LateUpdate()
    {
        CheckForSpawn();   
    }


    void CheckForSpawn()
    {
        if (currentBlocks[0].transform.position.x - player.position.x < -blockLength - 5)
        {
            SpawnBlock();
            DestroyBlock();
        }
    }

    void SpawnBlock()
    {
        GameObject block;
        Vector3 blockPosition;
        if (currentBlocks.Count > 0)
        {
            block = Instantiate(roadBlockPrefabs[Random.Range(0, roadBlockPrefabs.Length)], transform);
            blockPosition = currentBlocks[currentBlocks.Count - 1].transform.position + new Vector3(blockLength, 0, 0);
        }
        else
        {
            block = Instantiate(startBlock, transform); ;
            blockPosition = new Vector3(startBlockXPosition,0,0);
        }
        block.transform.position = blockPosition;
        currentBlocks.Add(block);
    }

    void DestroyBlock()
    {
        Destroy(currentBlocks[0]);
        currentBlocks.RemoveAt(0);
    }

    public void StartGame()
    {
        blockXPosition = startBlockXPosition;

        foreach(var go in currentBlocks)
        {
            Destroy(go);
        }
        currentBlocks.Clear();
        
        for (int i = 0; i < blocksCount; i++)
        {
            SpawnBlock();
        }

        player.GetComponent<CharacterMovement>().ResetPosition();
    }
}
