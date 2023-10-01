using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

public class Item_Compiler : MonoBehaviour
{
    private PlayerUpgrade_Manager _manager;
    [SerializeField] Item_Data item_Data;
    [SerializeField] LayerMask player;

    [SerializeField] GameObject popUp;

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

        //tolllie
        if (collision.gameObject.layer == 8)
        {
          
            GameObject currpopup = Instantiate(popUp, transform.position, Quaternion.identity);
            currpopup.transform.GetChild(0).GetComponent<TMP_Text>().SetText(item_Data.description);
            Destroy(currpopup, 5f);
            _manager.item_Data = item_Data;
            _manager.UpGrade();
            Destroy(gameObject);
        }

    }
}
