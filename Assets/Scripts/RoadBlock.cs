using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    GameManager gameManager;
    Vector3 moveVector;

    public GameObject coinsObject;
    public int coinsChance;
    bool coinsSpawn;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        moveVector = new Vector3(-1, 0, 0);
        coinsSpawn = Random.Range(0, 101) <= coinsChance;
        if (coinsObject != null)
        {
            coinsObject.SetActive(coinsSpawn);
        }
    }

    void Update()
    {
        if (gameManager.canPlay)
        {
            transform.Translate(moveVector * Time.deltaTime * gameManager.moveSpeed);
        }
    }

    void CoinsEvent(bool activate)
    {
        if (activate)
        {
            coinsObject.SetActive(true);
            return;
        }

        if (!coinsSpawn)
            coinsObject.SetActive(false);
    }

    private void OnDestroy()
    {
    }
}
