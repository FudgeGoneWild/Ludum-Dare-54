using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Compiler_Controller : MonoBehaviour
{
    [Header("Guns")]
    [SerializeField] List<Gun_Data> gun_Datas = new List<Gun_Data>();
    [SerializeField] public Gun_Data currGun;
    SpriteRenderer spriteRenderer;
    public bool canPickup = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator Start()
    {
        for (int i = 0; i < gun_Datas.Count; i++)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            spriteRenderer.sprite = gun_Datas[i].gunSprite;
        }
        for (int i = 0; i < gun_Datas.Count; i++)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            spriteRenderer.sprite = gun_Datas[i].gunSprite;
        }

        currGun = gun_Datas[Random.Range(0, gun_Datas.Count)];   
        canPickup = true;
        spriteRenderer.sprite = currGun.gunSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
