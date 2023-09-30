using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Compiler : MonoBehaviour
{
    private PlayerUpgrade_Manager _manager;
    [SerializeField] Item_Data item_Data;
    [SerializeField] LayerMask player;

    [SerializeField] List<Item_Data> items; 
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _manager = FindAnyObjectByType<PlayerUpgrade_Manager>();

        item_Data = items[Random.Range(0, items.Count)];
        spriteRenderer.sprite = item_Data.itemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _manager.item_Data = item_Data;
        _manager.UpGrade();
        Destroy(gameObject);
    }
}
