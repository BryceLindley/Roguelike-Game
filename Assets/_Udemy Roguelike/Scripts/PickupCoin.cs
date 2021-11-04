using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    
    public int gem = 5;
    public int goldCoin = 1;
    public float waitToBeCollected;
    

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToBeCollected <= 0)
        {
            LevelManager.instance.GetCoins(goldCoin);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(5);
        }
    }

}