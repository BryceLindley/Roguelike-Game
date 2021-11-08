using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [SerializeField]  GameObject shopHint;
    bool inBuyZone;
    [SerializeField] bool isHealthRestore, isHealthUpgrade, isWeapon;
    public int itemCost;
    public int healthUpgradeAmount;

    public Guns[] potentialGuns;
    private Guns theGun;
    public SpriteRenderer gunSprite;
    public Text infoText;
    

    // Start is called before the first frame update
    void Start()
    {
        if (isWeapon)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];
            gunSprite.sprite = theGun.gunShopSprite;
            infoText.text = theGun.weaponName + "\n - " + theGun.itemCost + " Gold - ";
            itemCost = theGun.itemCost;
        }
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

                    if (isWeapon)
                    {
                        Guns gunClone = Instantiate(theGun);
                        gunClone.transform.parent = PlayerController.instance.gunArm;
                        gunClone.transform.position = PlayerController.instance.gunArm.position;
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunClone.transform.localScale = Vector3.one;

                        PlayerController.instance.availableGuns.Add(gunClone);
                        PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;
                        PlayerController.instance.SwitchGun();
                        AudioManager.instance.PlaySFX("Pickup Health");
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
