using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health_Controller : MonoBehaviour
{
    [SerializeField] AudioClip healthClip;
    private AudioManager manager;

    [Header("Player Health Properties")]
    [SerializeField] private float Maxhealth = 5;
    [SerializeField] private float currHealth = 5;
    [SerializeField] public float healthBoost = 0; //upgrade health

    [SerializeField] Color32 flashColor;
    [SerializeField] bool invincible;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer gunspriteRenderer;
    [SerializeField] private ParticleSystem playerHurtVFX;

    [SerializeField] GameObject healthUI;

    [SerializeField] ParticleSystem die;
    private Camera_Animation_Controller camera_Animation;
    private endgame_Controller endgame_Controller;
    private Points_Controller points_Controller;
    // Start is called before the first frame update

    private void Start()
    {
        points_Controller = FindAnyObjectByType<Points_Controller>();
        endgame_Controller = FindAnyObjectByType<endgame_Controller>();
        manager = FindAnyObjectByType<AudioManager>();
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

    public void HealthPickup(float heal)
    {
        currHealth += heal;
        currHealth = Mathf.Clamp(currHealth, 0, Maxhealth);
        healthUI.GetComponent<Slider>().value = currHealth;
    }


    public void TakeDamage(float damage)
    {
        if (!invincible) 
        {
            manager.PlaySFX(healthClip);
            currHealth -= damage; 
        }
        healthUI.GetComponent<Slider>().value = currHealth;
        if (currHealth <= 0)
        {
            camera_Animation.AddComponent<AudioListener>();
            Instantiate(die, transform.position, Quaternion.identity);
            endgame_Controller.EndGame(points_Controller.points);

            gameObject.SetActive(false);
        }
    }
}
