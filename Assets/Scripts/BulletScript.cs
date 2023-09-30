using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] public float knockBack;

    [SerializeField] private ParticleSystem bulletSparks;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        ParticleSystem currsparks = Instantiate(bulletSparks, transform.position, Quaternion.identity);
        Destroy(currsparks, 0.2f);
        DestroyObject(gameObject, 0.1f);
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
