using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Controller : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float dashStrengh;
    [SerializeField] private float dashCooldown = 0.2f;
    [SerializeField] private float setDashReady = 2;

    [SerializeField] private float accelaration;
    [SerializeField] private float decelaration;
    [SerializeField] private float freezeFrameTimer;
    private Rigidbody2D body;

    private Vector2 axisMovement;
    private bool canDash = true;
    private bool dashReady = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axisMovement.x = Input.GetAxisRaw("Horizontal");
        axisMovement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && canDash && dashReady)
        {
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
            body.AddForce(axisMovement * speed, ForceMode2D.Force);
            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -speed, speed),Mathf.Clamp(body.velocity.y, -speed, speed));
        }
    }

    private void Dash()
    {
        StartCoroutine(nameof(DashCoolDown));
        body.AddForce(axisMovement.normalized * dashStrengh, ForceMode2D.Impulse);
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
        canDash = true;
    }


}
