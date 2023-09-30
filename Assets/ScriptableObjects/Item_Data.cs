using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Data", menuName = "Item Data", order = 1)]
public class Item_Data : ScriptableObject
{
    [SerializeField] public string ItemName;
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public string ItemBoost;
    [SerializeField] public float amount;
}
