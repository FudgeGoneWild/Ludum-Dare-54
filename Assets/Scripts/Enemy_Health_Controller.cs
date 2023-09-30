using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Health_Controller : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] int damage = 1;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript>().damage, collision.gameObject.GetComponent<BulletScript>().knockBack);

        }
    }

    public void TakeDamage(int damage, float knockback)
    {
        health -= damage;
        rb.AddRelativeForce(Vector2.down * knockback, ForceMode2D.Impulse);
        if (health <= 0)
        {
            DestroyObject(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
