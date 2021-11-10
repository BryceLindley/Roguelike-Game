using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D bulletRB;
    public GameObject impactEffect;
    public int damageToGive = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = transform.right * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        AudioManager.instance.PlaySFX("Impact");

        if (gameObject != null){
            other.GetComponent<EnemyController>().LosingHealth(damageToGive);
        } 
  
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
