using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAttackArea : MonoBehaviour
{
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10);
    }
}
