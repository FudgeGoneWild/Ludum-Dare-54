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
    [SerializeField] public int maxAmmoBoost = 1; // max ammo upgrade
    [SerializeField] int currAmmo;
    [SerializeField] float bulletSpeed;
    [SerializeField] public float fireRate;
    [SerializeField] float knockBack;
    [SerializeField] public float sprayBoost = 0.2f; //spray upgrade
    [SerializeField] public float spray;
    [SerializeField] public float FireRateBoost = 1; // fire rate upgrade
    [SerializeField] bool isShotgun;
    [SerializeField] int amountofShots;

    [Header("Gun compile properties")]
    [SerializeField] private GameObject gun_Anchor;
    [SerializeField] private AudioSource audioSouce;
    [SerializeField] private AudioClip gunFireSound;
    [SerializeField] private AudioClip gunEmpty;
    [SerializeField] private string ShakeName;

    [Header("PickUpRadius")]
    [SerializeField] float pickup_R;
    [SerializeField] LayerMask gun;

    [Header("Flash Rendering")]
    [SerializeField] Sprite[] flashes;
    [SerializeField] GameObject flashPoint;

    private PlayerAim_Controller playerAim;
    private Camera_Animation_Controller camera_Controller;
    private SpriteRenderer flashSR;

    bool canFire = true;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        playerAim = GetComponent<PlayerAim_Controller>();
        camera_Controller = FindObjectOfType<Camera_Animation_Controller>();
        rb = GetComponent<Rigidbody2D>();
        SetGunProperties(gun_Data);
        flashSR = flashPoint.GetComponent<SpriteRenderer>();
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
            if (isShotgun)
            {
                for (int i = 0; i < amountofShots; i++)
                {
                    GameObject currBullet = Instantiate(bullet, firePoint.transform.position, Quaternion.EulerAngles(0,0,playerAim.GetAngle()));
                    currBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x + Random.Range(shootDirection.x - spray, shootDirection.x + spray) * bulletSpeed, shootDirection.y + Random.Range(shootDirection.y - spray, shootDirection.y + spray) * bulletSpeed);
                
                }
                currAmmo--;
            }
            else
            {
                GameObject currBullet = Instantiate(bullet, firePoint.transform.position, Quaternion.EulerAngles(0,0,playerAim.GetAngle()));
                currBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x + Random.Range(shootDirection.x - spray, shootDirection.x + spray) * bulletSpeed, shootDirection.y + Random.Range(shootDirection.y - spray, shootDirection.y + spray) * bulletSpeed);
                currAmmo--;
            }
            StartCoroutine(nameof(Flash));
            PlaySound(gunFireSound);
            ApplyKnockBack();
            StartCoroutine(nameof(FireRateDelay));
            camera_Controller.ChooseShake(ShakeName);
        }
        else if (currAmmo == 0 && Input.GetKey(KeyCode.Mouse0) && canFire)
        {
            PlaySound(gunEmpty);
            StartCoroutine(nameof(FireRateDelay));
            //put in something to show ammo is fin;
        }
    }

    private void PlaySound(AudioClip sound)
    {
        audioSouce.clip = sound;
        audioSouce.Play();
    }

    private IEnumerator Flash()
    {
        flashSR.sprite = flashes[Random.Range(0, flashes.Length - 1)];
        flashPoint.SetActive(true);
        yield return new WaitForSecondsRealtime(0.05f);
        flashPoint.SetActive(false);
    }

    private void PickUpGun()
    {
        if (Physics2D.OverlapCircle(transform.position, pickup_R, gun) == true)
        {
            if (Input.GetKey(KeyCode.E) && Physics2D.OverlapCircle(transform.position, pickup_R, gun).GetComponent<Gun_Compiler_Controller>().canPickup)
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
        currAmmo = maxAmmo + maxAmmoBoost;
        bulletSpeed = currGun.bulletSpeed;
        fireRate = currGun.fireRate - FireRateBoost;
        knockBack = currGun.knockBack;
        spray = currGun.gunSpray - sprayBoost;
        isShotgun = currGun.shotgun;
        ShakeName = currGun.shakeString;

        gunFireSound = gun_Data.gunSound;
        gunEmpty = gun_Data.gunEmptySound;
        gun_Anchor.GetComponent<SpriteRenderer>().sprite = currGun.armsWithGun;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pickup_R);
    }

}
