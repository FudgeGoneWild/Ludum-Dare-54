using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun_Data", menuName = "Gun Data", order = 1)]
public class Gun_Data : ScriptableObject
{
    [Header("Gun Properties")]
    [SerializeField] public string gunName;
    [SerializeField] public int MaxAmmo;
    [SerializeField] public float fireRate;
    [SerializeField] public float knockBack;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public Vector3 firePointPOS;

    [Header("Animations")]
    [SerializeField] public Sprite armsWithGun;
    [SerializeField] public Sprite gunSprite;

    [Header("Prefabs")]
    [SerializeField] public GameObject bulletPrefab;
}
