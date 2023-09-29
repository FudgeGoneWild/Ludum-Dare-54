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
    [SerializeField] public Sprite gunSprite;
    [SerializeField] public int layerIndex;
    [SerializeField] public int layerWeight;

    [Header("Prefabs")]
    [SerializeField] public GameObject bulletPrefab;
}
