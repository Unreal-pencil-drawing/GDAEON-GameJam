using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float speed;
    public float damage;
    public float resistance_time;
    public bool isResistance;
    private float resistance_timer;
    private GameObject _player;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Vector3 scale;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        scale = transform.localScale;
    }

    private void Move() 
    {
    }


    void Start()
    {
    }

    private float getDictanceToPlayer() => Vector3.Distance(_player.transform.position, transform.position);

    public void TakeDamage(float damageValue) {
        if (!isResistance) {
            health -= damageValue;
            isResistance = true;
            resistance_timer = resistance_time;
            Debug.Log("Get Hit");
        }
    }

    void Update()
    {
        // if (getDictanceToPlayer() < 1) {
        //     TakeDamage(1);
        // }
        if (resistance_timer > 0) {
            resistance_timer -= Time.deltaTime;
        } else {
            isResistance = false;
        }

        Move();
        LocalScaleRotate();
    }

    void LocalScaleRotate() {
        if (_player.transform.position.x > transform.position.x) {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        else
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
        
    }
}
