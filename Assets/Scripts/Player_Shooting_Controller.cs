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
    [SerializeField] float spray;

    [Header("Gun compile properties")]
    [SerializeField] private GameObject gun_Anchor;

    [Header("PickUpRadius")]
    [SerializeField] float pickup_R;
    [SerializeField] LayerMask gun;

    bool canFire = true;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetGunProperties(gun_Data);
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        PickUpGun();
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
            currBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x + Random.Range(shootDirection.x - spray, shootDirection.x + spray) * bulletSpeed, shootDirection.y + Random.Range(shootDirection.y - spray, shootDirection.y + spray) * bulletSpeed);
            currAmmo--;
            ApplyKnockBack();
            StartCoroutine(nameof(FireRateDelay));
        }
    }

    private void PickUpGun()
    {
        if (Physics2D.OverlapCircle(transform.position, pickup_R, gun) == true)
        {
            if (Input.GetKey(KeyCode.E))
            {

                gun_Data = Physics2D.OverlapCircle(transform.position, pickup_R, gun).GetComponent<Gun_Compiler_Controller>().currGun;
                GameObject gunObject = Physics2D.OverlapCircle(transform.position, pickup_R, gun).gameObject;

                DestroyObject(gunObject);

                SetGunProperties(gun_Data);
                Debug.Log("Setting up new gun " + gun_Data.name);
            }
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



    private void SetGunProperties(Gun_Data currGun) //set gun properties from scriptable object
    {

        firePoint.transform.localPosition = currGun.firePointPOS;
        bullet = currGun.bulletPrefab;
        maxAmmo = currGun.MaxAmmo;
        currAmmo = maxAmmo;
        bulletSpeed = currGun.bulletSpeed;
        fireRate = currGun.fireRate;
        knockBack = currGun.knockBack;
        spray = currGun.gunSpray;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pickup_R);
    }

}
