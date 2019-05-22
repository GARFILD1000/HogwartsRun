using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameManager gameManager;
    public CharacterMovement characterMovement;

    public void Pause()
    {
        gameObject.SetActive(true);
        gameManager.canPlay = false;
        characterMovement.StopMovement();
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        gameManager.canPlay = true;
        characterMovement.StartMovement();
    }

    public void BackToMenu()
    {
        gameManager.BackToMenu();
    }
}
