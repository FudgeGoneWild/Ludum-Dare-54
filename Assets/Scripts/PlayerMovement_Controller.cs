using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Controller : MonoBehaviour
{
    [SerializeField] AudioClip m_Clip;
    private AudioManager m_AudioManager;

    [Header("Movement Properties")]
    [SerializeField] public float speed = 10f;
    [SerializeField] public float speedboost = 0;
    [SerializeField] private float dashStrengh;
    [SerializeField] public float dashStrenghBoost = 0;
    [SerializeField] private float dashCooldown = 0.2f;
    [SerializeField] public float setDashReady = 2;
    [SerializeField] public float dashCooldownBoost = 0;

    private Rigidbody2D body;
    private TrailRenderer lineRenderer;
    private Vector2 axisMovement;
    private bool canDash = true;
    private bool dashReady = true;


    private Camera_Animation_Controller Cameracontroller;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioManager = FindAnyObjectByType<AudioManager>();
        Cameracontroller = FindObjectOfType<Camera_Animation_Controller>();
        body = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<TrailRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        axisMovement.x = Input.GetAxisRaw("Horizontal");
        axisMovement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && canDash && dashReady)
        {
            StartCoroutine(nameof(FreezeFrame));
            Dash();
        }
    }

    private void FixedUpdate()
    {
        RigidbodyVelocity();   
    }


    private void RigidbodyVelocity()
    {
       if (canDash)
        {
            //body.velocity = axisMovement * speed;
            body.AddForce(axisMovement * (speed + speedboost) , ForceMode2D.Force);
            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -speed, speed),Mathf.Clamp(body.velocity.y, -speed, speed));
        }
    }

    public void UpgradeDashCooldown()
    {
        if (dashCooldown < 0)
        {
            setDashReady -= dashCooldownBoost;
        }
    }

    public void UpgradeDashStrengh()
    {
        dashStrengh += dashStrenghBoost;
    }

    private void Dash()
    {
        m_AudioManager.PlaySFX(m_Clip);
        Cameracontroller.MediumShake();
        StartCoroutine(nameof(DashCoolDown));
        StartCoroutine(nameof(SetDashReady));
        body.AddForce(axisMovement.normalized * dashStrengh, ForceMode2D.Impulse);
        lineRenderer.enabled = true;

    }

    IEnumerator SetDashReady()
    {
        dashReady = false;
        yield return new WaitForSecondsRealtime(setDashReady);
        dashReady = true;
    }

    IEnumerator DashCoolDown()
    {
        canDash = false;
        yield return new WaitForSecondsRealtime(dashCooldown);
        body.velocity = Vector2.zero;
        lineRenderer.enabled = false;
        canDash = true;
    }

    IEnumerator FreezeFrame()
    { 
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.05f);
        Time.timeScale = 1.0f;
    }


}
