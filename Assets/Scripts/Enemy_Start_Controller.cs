using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Start_Controller : MonoBehaviour
{
    private AIDestinationSetter aIDestinationSetter;
    // Start is called before the first frame update
    void Start()
    {
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aIDestinationSetter.target = FindAnyObjectByType<PlayerAim_Controller>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
