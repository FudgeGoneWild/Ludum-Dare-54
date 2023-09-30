using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Gun_Data", menuName = "Gun Data", order = 1)]
public class Gun_Data : ScriptableObject
{
    [Header("Gun Properties")]
    [SerializeField] public string gunName;
    [SerializeField] public int MaxAmmo;
    [SerializeField] public float fireRate;
    [SerializeField] public float knockBack;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float gunSpray;
    [SerializeField] public Vector3 firePointPOS;
    [SerializeField] public bool shotgun = false;
    [Header("Animations")]
    [SerializeField] public Sprite armsWithGun;
    [SerializeField] public Sprite gunSprite;
    [SerializeField] public AudioClip gunSound;
    [SerializeField] public AudioClip gunEmptySound;
    [SerializeField] public string shakeString;

    [Header("Prefabs")]
    [SerializeField] public GameObject bulletPrefab;
}
