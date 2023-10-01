using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb_Controller : MonoBehaviour
{
    [SerializeField] int amount = 1;
    [SerializeField] AudioClip pickUpsound;
    AudioManager manager;
    private Player_Health_Controller health_controller;
    // Start is called before the first frame update
    private void Start()
    {
        manager = FindAnyObjectByType<AudioManager>();
        health_controller = FindAnyObjectByType<Player_Health_Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            manager.PlaySFX(pickUpsound);
            health_controller.HealthPickup(amount);
            DestroyObject(gameObject);
        }
        
    }
}
