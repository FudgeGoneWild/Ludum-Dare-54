using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Data", menuName = "Item Data", order = 1)]
public class Item_Data : ScriptableObject
{
    [SerializeField] string ItemName;
    [SerializeField] float boostAmount;
    [SerializeField] Sprite itemSprite;
}
