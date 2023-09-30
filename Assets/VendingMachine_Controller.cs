using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine_Controller : MonoBehaviour
{
    [SerializeField] Vector3 box_Size;
    [SerializeField] LayerMask player;
    [SerializeField] Transform spot;

    private bool isActive = true;
    [SerializeField] GameObject gunCompiler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapBox(transform.position, box_Size, 0f, player))
        {
            if (Input.GetKeyDown(KeyCode.E) && isActive)
            {
                StartCoroutine(nameof(Reset));
                GameObject gunCompiled = Instantiate(gunCompiler, spot.position, Quaternion.EulerRotation(0,0,transform.rotation.z));
            }
        }
    }

    private IEnumerator Reset()
    {
        isActive = false;
        yield return new WaitForSecondsRealtime(5f);
        isActive = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, box_Size);
    }
}
