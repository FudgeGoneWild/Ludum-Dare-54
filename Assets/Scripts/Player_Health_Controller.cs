using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health_Controller : MonoBehaviour
{
    [Header("Player Health Properties")]
    [SerializeField] private int Maxhealth = 5;
    [SerializeField] private int currHealth = 5;

    [SerializeField] Color32 flashColor;
    [SerializeField] bool invincible;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer gunspriteRenderer;
    // Start is called before the first frame update

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && !invincible)
        {
            StartCoroutine(nameof(Invincible));
            //give damage

            Debug.Log("Take damage from " + collision.gameObject.name);
        }
    }

    private IEnumerator Invincible()
    {
        invincible = true;
        yield return new WaitForSecondsRealtime(0.15f);
        gunspriteRenderer.color = flashColor;
        spriteRenderer.color = flashColor;
        yield return new WaitForSecondsRealtime(0.15f);
        gunspriteRenderer.color = Color.white;
        spriteRenderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.15f);
        gunspriteRenderer.color = flashColor;
        spriteRenderer.color = flashColor;
        yield return new WaitForSecondsRealtime(0.15f);
        gunspriteRenderer.color = Color.white;
        spriteRenderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.15f);
        gunspriteRenderer.color = flashColor;
        spriteRenderer.color = flashColor;
        yield return new WaitForSecondsRealtime(0.15f);
        gunspriteRenderer.color = Color.white;
        spriteRenderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.15f);
        gunspriteRenderer.color = flashColor;
        spriteRenderer.color = flashColor;
        yield return new WaitForSecondsRealtime(0.15f);
        gunspriteRenderer.color = Color.white;
        spriteRenderer.color = Color.white;
        invincible = false;
    }

    public void HealthPickup(int healthboost)
    {
        currHealth += healthboost;
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        if (currHealth <= 0)
        {
            //play end cutscene;
            //restart game
        }
    }
}