using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;
    public GameObject deathScreen;
    public GameObject pauseMenu;
    public Image fadeScreen;
    public float fadeSpeed = .50F;
    private Color fadeWhite;
    private Color fadeBlack;
    private float white = 0.0F;
    private float black = 1.0F;
    public Text coinText;

    private bool fadeIn, fadeOut;
    public string newGameScene, mainMenuScene;
    


    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeOut = true;
        fadeIn = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, fadeScreen.color.a * Mathf.MoveTowards(fadeScreen.color.a, 0.0F, fadeSpeed * Time.deltaTime));
            while (fadeScreen.color.a > 0.0F)
            {
                fadeOut = false;
            }
        }

        if (fadeIn)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, fadeScreen.color.a * Mathf.MoveTowards(fadeScreen.color.a, 1.0F, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == black)
            {
                fadeIn = false;
            }
        }


      
    }
    public void StartFadeToBlack()
    {
        fadeIn = true;
        fadeOut = false;
    }


    public void NewGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }

}
