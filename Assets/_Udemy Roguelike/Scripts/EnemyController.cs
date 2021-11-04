using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyHit;
    public Rigidbody2D theRB;
    public float moveSpeed;
    public Animator enemyAnim;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    public int health = 150;
    public GameObject[] deathSplatters;


    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    public float shootRange;
    public SpriteRenderer theBody;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if Objects are on Screen by checking if Sprite Renderer is on Screen by creating a reference
        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;



            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX("Shoot2");
                }
            }

            //Using moving/not moving if statement
            if (moveDirection != Vector3.zero)
            {
                enemyAnim.SetBool("isMoving", true);
            }
            else
            {
                enemyAnim.SetBool("isMoving", false);
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }
    }


    public void LosingHealth(int damage)
    {
        health -= damage;

        Instantiate(enemyHit, transform.position, transform.rotation);

        AudioManager.instance.PlaySFX("Enemy Hurt");

        if (health <= 0)
        {

            AudioManager.instance.PlaySFX("Enemy Death");

            Destroy(gameObject);

            int selectedSplatter = Random.Range(0, deathSplatters.Length);

            int rotation = Random.Range(0, 4);

            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

            if (shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);

                if (dropChance < itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                }

            }
        }

    }
}

