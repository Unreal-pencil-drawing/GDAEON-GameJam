using UnityEngine;

[AddComponentMenu("Attack")]

public class Attack : MonoBehaviour
{
    void FixedUpdate(){
        RotateAttackArea();
    }

    void RotateAttackArea(){
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Boss") {
            other.GetComponent<Boss>().TakeDamage(1);   
        }
    }
}
