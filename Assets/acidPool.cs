using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class acidPool : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] bool candamage;
    [SerializeField] Color32 acid;
    
    // Start is called before the first frame update
    void Start()
    {
        DestroyObject(gameObject, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (candamage)
            {

                StartCoroutine(nameof(ResetDamage), collision);
                collision.GetComponent<Player_Health_Controller>().TakeDamage(damage);
            }
        }
    }


    IEnumerator ResetDamage(Collider2D player)
    {
        player.GetComponent<SpriteRenderer>().color = acid;
        candamage = false;
        yield return new WaitForSecondsRealtime(1f);
        candamage = true;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
