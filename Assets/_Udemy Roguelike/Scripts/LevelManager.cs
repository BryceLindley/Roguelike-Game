﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;

    public bool isPaused;

    public int currentCoins;

    public Room CurrentRoom { get; set; }

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        UIController.instance.coinText.text = currentCoins.ToString();
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        PlayerController.instance.canMove = false;
        UIController.instance.StartFadeToBlack();
        AudioManager.instance.PlayLevelWin();
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(nextLevel);
        PlayerController.instance.canMove = true;

    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);

            isPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);

            isPaused = false;

            Time.timeScale = 1f;
        }
    }

    public void GetCoins(int amount)
    {
        currentCoins += amount;
        UIController.instance.coinText.text = currentCoins.ToString();

    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;
        if (currentCoins < 0)
        {
            currentCoins = 0;
        }
        UIController.instance.coinText.text = currentCoins.ToString();
    }
}
