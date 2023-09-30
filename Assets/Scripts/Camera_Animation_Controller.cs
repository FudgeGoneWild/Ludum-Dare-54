using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Animation_Controller : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseShake(string shakename)
    {
        if (shakename == "LightShake")
        {
            LightShake();
        }
        if (shakename == "MediumShake")
        {
            MediumShake();
        }
        if(shakename == "HeavyShake")
        {
            HeavyShake();
        }
    }

    public void LightShake()
    {
        _animator.SetTrigger(nameof(LightShake));
    }

    public void MediumShake()
    {
        _animator.SetTrigger(nameof(MediumShake));
    }

    public void HeavyShake()
    {
        _animator.SetTrigger(nameof(HeavyShake));
    }
}
