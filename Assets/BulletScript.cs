using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] public float knockBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyObject(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
