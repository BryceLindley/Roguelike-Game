using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GunPickup[] potentialGuns;
    public SpriteRenderer theSR;
    public Sprite chestOpen;

    public GameObject notification;
    private bool canOpen, isOpen;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                int gunSelect = Random.Range(0, potentialGuns.Length);
                Instantiate(potentialGuns[gunSelect], spawnPoint.position, spawnPoint.rotation);
                theSR.sprite = chestOpen;
                isOpen = true;
                transform.localScale = new Vector3(2.0F, 2.0F, 2.0F);
            }
        }

        if (isOpen)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * 2.0F);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            notification.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            notification.SetActive(false);
            canOpen = false;
        }
    }
   
}
