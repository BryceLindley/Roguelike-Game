using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject Barrel;
    public GameObject Explosion;
    private AudioSource source;
    [SerializeField]
    private float range;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.X))
        {
            Explode();
        }  
    }

    private void Awake()
    {
       Barrel.SetActive(true);
        Explosion.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    public void Explode()
    {

        Explosion.SetActive(true);
        source.Play();
        Barrel.SetActive(false);
    }

    
}
