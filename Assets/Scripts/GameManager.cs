using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public GameObject restartText;

    public GameObject gamestartImage;
    public GameObject startButton;

    private bool isGameover;

    void Start()
    {
        isGameover = false;
        gameoverText.SetActive(false);
        restartText.SetActive(false);
    }
    void Update()
    {
        if(isGameover)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    public void StartGame()
    {
        gameoverText.SetActive(false);
        restartText.SetActive(false);

        gamestartImage.SetActive(true);
        startButton.SetActive(true);
    }
    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);
        restartText.SetActive(true);

        gamestartImage.SetActive(false);
        startButton.SetActive(false);
    }
}
