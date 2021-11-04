using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    [SerializeField]  GameObject shopHint;
    bool inBuyZone;
    [SerializeField] bool isHealthRestore, isHealthUpgrade, isWeapon;
    public int itemCost;
    public int healthUpgradeAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(LevelManager.instance.currentCoins >= itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);

                    if(isHealthRestore)
                    {
                        PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                    }

                    if (isHealthUpgrade)
                    {
                        PlayerHealthController.instance.IncreaseMaxHealth(healthUpgradeAmount);
                    }

                    gameObject.SetActive(false);
                    inBuyZone = false;
                    AudioManager.instance.PlaySFX(18);
                } else
                {
                    AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shopHint.SetActive(true);
            inBuyZone = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shopHint.SetActive(false);
            inBuyZone = false;
        }
    }
}
