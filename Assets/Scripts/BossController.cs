using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public BossAction[] actions;
    private Vector2 newDirection;
    private int currentAction;
    private float actionCounter;
    private float shotCounter;
    public Rigidbody2D theRB;
    public int currentHealth;
    public GameObject deathEffect, hitEffect;
    public GameObject levelExit;
    public BossSequences[] sequences;
    public int currentSequence;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;
        UIController.instance.bossHealthBar.maxValue = currentHealth;
        UIController.instance.bossHealthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;
            //handle movement
            newDirection = Vector2.zero;

            if (actions[currentAction].shouldMove)
            {
                if (actions[currentAction].shouldChasePlayer)
                {
                    newDirection = PlayerController.instance.transform.position - transform.position;
                    newDirection.Normalize();
                }

                if (actions[currentAction].moveToPoint && Vector3.Distance(transform.position, actions[currentAction].pointToMove.position) > 5.0F)
                {
                    newDirection = actions[currentAction].pointToMove.position - transform.position;
                    newDirection.Normalize();
                }
            }
            theRB.velocity = newDirection * actions[currentAction].moveSpeed;
        } else
        {
            currentAction++;
            if(currentAction >= actions.Length)
            {
                currentAction = 0;
            }
            actionCounter = actions[currentAction].actionLength;

            //handle shooting
            if (actions[currentAction].shoulShoot)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;
                    foreach(Transform t in actions[currentAction].shotPoints)
                    {
                        Instantiate(actions[currentAction].itemToShoot, t.position, t.rotation);
                    }
                }
            }
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(deathEffect, transform.position, transform.rotation); 
            if (Vector3.Distance(PlayerController.instance.transform.position, levelExit.transform.position) < 2.0F)
            {
                levelExit.transform.position += new Vector3(4.0F, 0, 0);
            }

            levelExit.SetActive(true);
            UIController.instance.bossHealthBar.gameObject.SetActive(false);
        } else
        {
            if (currentHealth <= sequences[currentSequence].endSequenceHealth && currentSequence < sequences.Length - 1)
            {
                currentSequence++;
                actions = sequences[currentSequence].actions;
                currentAction = 0;
                actionCounter = actions[currentAction].actionLength;
            }
        }
        UIController.instance.bossHealthBar.value = currentHealth;
    } 
    


}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;
    public bool shouldMove;
    public bool shouldChasePlayer;
    public bool moveToPoint;
    public float moveSpeed;
    public Transform pointToMove;

    public bool shoulShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;


}

[System.Serializable] 
public class BossSequences
{
    [Header("Sequence")]
    public BossAction[] actions;
    public int endSequenceHealth;
}