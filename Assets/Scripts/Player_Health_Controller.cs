using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health_Controller : MonoBehaviour
{
    [Header("Player Health Properties")]
    [SerializeField] private int Maxhealth = 5;
    [SerializeField] private int currHealth = 5;
    [SerializeField] public int healthBoost = 0; //upgrade health

    [SerializeField] Color32 flashColor;
    [SerializeField] bool invincible;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer gunspriteRenderer;
    [SerializeField] private ParticleSystem playerHurtVFX;

    [SerializeField] GameObject healthUI;

    private Camera_Animation_Controller camera_Animation;

    // Start is called before the first frame update

    private void Start()
    {
        camera_Animation = FindAnyObjectByType<Camera_Animation_Controller>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && !invincible)
        {
            StartCoroutine(nameof(FreezeFrame));
            //give damage

            Debug.Log("Take damage from " + collision.gameObject.name);
        }
    }

    private IEnumerator FreezeFrame()
    {
        ParticleSystem currHurtVFX = Instantiate(playerHurtVFX,transform.position, Quaternion.identity);
        Destroy(currHurtVFX, 0.5f);
        gunspriteRenderer.color = flashColor;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
        camera_Animation.HeavyShake();
        StartCoroutine(nameof(Invincible));
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

    public void UpdateMaxHealth()
    {
        Maxhealth += healthBoost;
        healthUI.GetComponent<Slider>().maxValue = Maxhealth;
    }

    public void HealthPickup(int heal)
    {
        currHealth += heal;
        currHealth = Mathf.Clamp(currHealth, 0, Maxhealth);
        healthUI.GetComponent<Slider>().value = currHealth;
    }


    public void TakeDamage(int damage)
    {
        if (!invincible) { currHealth -= damage; }
        healthUI.GetComponent<Slider>().value = currHealth;
        if (currHealth <= 0)
        {
            //play end cutscene;
            //restart game
        }
    }
}
