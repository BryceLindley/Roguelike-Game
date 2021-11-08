using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    public Rigidbody2D playerRB;
    public Transform gunArm;
    public Animator anim;
    //public GameObject bulletToFire;
    //public Transform firePoint;
    //public float timeBetweenShots;
    //private float shotCounter;
    private Vector2 moveInput;
    private Camera theCam;
    public SpriteRenderer bodySR;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvinciblility = .5f;
    private float dashCoolCounter;
    [HideInInspector]
    public float dashCounter;
    [HideInInspector]
    public bool canMove = true;
    public List<Guns> availableGuns = new List<Guns>();
    [HideInInspector]
    public int currentGun;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        activeMoveSpeed = moveSpeed;
        UIController.instance.currentGun.sprite = availableGuns[currentGun].gunUI;
        UIController.instance.gunText.text = availableGuns[currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (canMove)
            {
                playerDashCounter();
                playerMovement();
                playerGunControls();
                playerFootMovement();
            } else { 
                playerRB.velocity = Vector2.zero;
                anim.SetBool("isMoving", false);
            }
           
        }
    }

    private void playerDashCounter()
    {
        if (dashCounter <= 0)
        {
        }
        else
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
                activeMoveSpeed = dashSpeed;
            dashCounter = dashLength;

            anim.SetTrigger("dash");
            AudioManager.instance.PlaySFX("Player Dash");
            PlayerHealthController.instance.MakeInvincible(dashInvinciblility);
        }
    }

    private void playerMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        playerRB.velocity = moveInput * activeMoveSpeed;

        //Where mouse cursor currently is
        Vector3 mousePos = Input.mousePosition;
        //Where player currently is
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            gunArm.localScale = new Vector3(1f, 1f, 1f);
        }

        //rotate gun arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void playerGunControls()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
            AudioManager.instance.PlaySFX("Shoot1");
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
                AudioManager.instance.PlaySFX("Shoot1");
            }
        }*/
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(availableGuns.Count > 0)
            {
                currentGun++;
                if(currentGun >= availableGuns.Count)
                {
                    currentGun = 0;
                }

                SwitchGun();

            } else
            {
                Debug.LogError("Player has no guns!");
            }
        }

    
    }

    private void playerFootMovement()
    {
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }


    public void SwitchGun()
    {
        foreach(Guns theGun in availableGuns)
        {
            theGun.gameObject.SetActive(false);
        }
        availableGuns[currentGun].gameObject.SetActive(true);
        UIController.instance.currentGun.sprite = availableGuns[currentGun].gunUI;
        UIController.instance.gunText.text = availableGuns[currentGun].weaponName;
    }
}




