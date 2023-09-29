using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim_Controller : MonoBehaviour
{
    private Vector3 mouse_POS;
    private float angle;

    private void Update()
    {
        mouse_POS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        AimAtMouse();
    }

    private void AimAtMouse()
    {
        Vector3 aimDirection = (mouse_POS - transform.position).normalized;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public float GetAngle()
    {
        return angle;
    }
}
