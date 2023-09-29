using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Shooting_Controller : MonoBehaviour
{
    [Header("Gun Properties")]
    [SerializeField] Gun_Data gun_Data;

    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] int maxAmmo;
    [SerializeField] int currAmmo;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate;
    [SerializeField] float knockBack;

    bool canFire = true;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetGunProperties();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canFire && currAmmo != 0)
        {
            Vector3 shootDirection;
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;
            GameObject currBullet = Instantiate(bullet, firePoint.transform.position, Quaternion.identity);
            currBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * bulletSpeed, shootDirection.y * bulletSpeed);
            currAmmo--;
            ApplyKnockBack();
            StartCoroutine(nameof(FireRateDelay));
        }
    }

    IEnumerator FireRateDelay()
    {
        canFire = false;
        yield return new WaitForSecondsRealtime(fireRate);
        canFire = true;
    }

    void ApplyKnockBack()
    {
        rb.AddRelativeForce(Vector2.left * knockBack, ForceMode2D.Impulse);
    }



    private void SetGunProperties() //set gun properties from scriptable object
    {
        firePoint.transform.position = gun_Data.firePointPOS;
        bullet = gun_Data.bulletPrefab;
        maxAmmo = gun_Data.MaxAmmo;
        currAmmo = maxAmmo;
        bulletSpeed = gun_Data.bulletSpeed;
        fireRate = gun_Data.fireRate;
        knockBack = gun_Data.knockBack;
    }

}
