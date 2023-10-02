using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Health_Controller : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioClip deathSound;
    [SerializeField] int health = 3;
    [SerializeField] float damage = 1;
    [SerializeField] GameObject popup;
    [SerializeField] int points;

    [SerializeField] ParticleSystem hurtParticles;
    [SerializeField] ParticleSystem dieParticles;
    [SerializeField] Color32 hurtColor;
    [SerializeField] GameObject itemDrop;
    [SerializeField] GameObject healthpickup;
    private Rigidbody2D rb;
    private SpriteRenderer rb_sprite;
    private GameStart_Controller controller;
    private Points_Controller points_Controller;

    [SerializeField] GameObject acidPool;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        rb_sprite = rb.GetComponent<SpriteRenderer>();
        controller = FindAnyObjectByType<GameStart_Controller>();
        points_Controller = FindAnyObjectByType<Points_Controller>();

        damage += controller.currWave * 0.10f;
    }

    private void OnEnable()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript>().damage, collision.gameObject.GetComponent<BulletScript>().knockBack);
            ParticleSystem hurtVSX = Instantiate(hurtParticles, collision.transform.position, Quaternion.identity);
            Destroy(hurtVSX, 0.2f);
        }

        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Player_Health_Controller>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage, float knockback)
    {
        health -= damage;
        rb.AddRelativeForce(Vector2.down * knockback, ForceMode2D.Impulse);
        StartCoroutine(nameof(flashHurt));
        if (health <= 0)
        {
            Instantiate(acidPool, transform.position, Quaternion.identity);
            points_Controller.UpdatePoints(points);
            GameObject currpopup = Instantiate(popup, transform.position, Quaternion.identity);
            currpopup.transform.GetChild(0).GetComponent<TMP_Text>().SetText(points.ToString());
            Destroy(currpopup, 5f);
            ParticleSystem currDieParticles = Instantiate(dieParticles, transform.position, Quaternion.identity);
            Destroy(currDieParticles, 1f);
            DropItem();
            FindAnyObjectByType<Camera_Animation_Controller>().HeavyShake();
            audioManager.PlaySFX(deathSound);
            DestroyObject(gameObject);
        }
    }

    void DropItem()
    {
        if (Random.RandomRange(0,120) < 30)
        {
            GameObject currItem = Instantiate(itemDrop, transform.position, Quaternion.identity);
        }

        if (Random.RandomRange(0, 120) < 15)
        {
            GameObject currItem = Instantiate(healthpickup, transform.position, Quaternion.identity);
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
