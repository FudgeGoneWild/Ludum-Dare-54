using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Health_Controller : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] int damage = 1;

    [SerializeField] ParticleSystem hurtParticles;
    [SerializeField] Color32 hurtColor;
    private Rigidbody2D rb;
    private SpriteRenderer rb_sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb_sprite = rb.GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript>().damage, collision.gameObject.GetComponent<BulletScript>().knockBack);
            ParticleSystem hurtVSX = Instantiate(hurtParticles, collision.transform.position, Quaternion.identity);
            Destroy(hurtVSX, 0.2f);
        }
    }

    public void TakeDamage(int damage, float knockback)
    {
        health -= damage;
        rb.AddRelativeForce(Vector2.down * knockback, ForceMode2D.Impulse);
        StartCoroutine(nameof(flashHurt));
        if (health <= 0)
        {
            DestroyObject(gameObject);
        }
    }

    IEnumerator flashHurt()
    {
        rb_sprite.color = hurtColor;
        yield return new WaitForSecondsRealtime(0.2f);
        rb_sprite.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
