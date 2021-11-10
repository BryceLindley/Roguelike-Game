using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource levelMusic, gameOverMusic, levelChange, caveAmbient, characterSelection;

    public AudioSource[] sfx;




    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void CharacterSelect()
    {
        characterSelection.Play();

    }
    public void AmbientCave()
    {
        caveAmbient.Play();
        levelMusic.Stop();
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();

        gameOverMusic.Play();
    }

    public void LevelSoundChange()
    {
        levelMusic.Stop();

        levelChange.Play();
    }


    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

  
    public void PlaySFX(string name)
    {
        foreach (AudioSource audioSource in sfx)
        {
            if (audioSource.name == name)
        {
        
                audioSource.Stop();
                audioSource.Play();
                break;
            }
        }
    }
}
    

