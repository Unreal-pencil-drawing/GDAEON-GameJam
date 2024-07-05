using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Rendering;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;
    public float resistance_time;
    public float dash_cooldown;
    public bool isResistance;
    public float dash_coefficient;
    public float distanceToPlayer;
    // Внутренние переменные
    private float resistance_timer;
    private float dash_timer;
    private bool isDash;
    private bool isDead;
    private float elapsedTime;
    private Vector3 endPosition;

    // Ссылки на объекты
    private GameObject _player;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Vector3 scale;
    private void Awake()
    {
        _player = GameObject.Find("Player");
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        scale = transform.localScale;
        dash_timer = dash_cooldown;
    }

    private Vector3 ReturnToField(Vector3 position) {
        if (position.y <= -9.1f) {
            position.y = -9.1f;
        }
        if (position.y >= 10f) {
            position.y = 10f;
        }
        if (position.x <= -18.9f) {
            position.x = -18.9f;
        }
        if (position.x >= 18.9f) {
            position.x = 18.9f;
        }
        return position;
    }

    private void Move() 
    {
        if (dash_timer <= 0 && GetDictanceToPlayer() > 10) 
            Dash();

        if (!isDash) 
        {
            Vector3 position = Vector2.MoveTowards(transform.position, _player.transform.position, speed);
            transform.position = ReturnToField(position);
        } 
        else 
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * dash_coefficient);
            if (elapsedTime >= 0.5f) {
                isDash = false;
                isResistance = false;
            }
            dash_timer = dash_cooldown;
        }
    }


    private void Start()
    {
    }

    private float GetDictanceToPlayer() => Vector3.Distance(_player.transform.position, transform.position);

    public void TakeDamage(float damageValue) {
        if (!isResistance) 
        {
            health -= damageValue;
            isResistance = true;
            resistance_timer = resistance_time;
            Debug.Log("Get Hit");
        }
    }

    private void Death() {

    }

    private void Dash() {
        Vector3 direction = _player.transform.position - transform.position;
        direction.z = 0;
        endPosition = transform.position + GetDictanceToPlayer() * 0.8f * direction.normalized;        
        isResistance = true;
        isDash = true;
        elapsedTime = 0;
        endPosition = ReturnToField(endPosition); 
    }

    private void UpdateTimers() {
        if (resistance_timer > 0) 
            resistance_timer -= Time.deltaTime;
        else
            isResistance = false;

        if (dash_timer > 0) 
            dash_timer -= Time.deltaTime;            
    }

    private void Update()
    {
        UpdateTimers();
        Move();
        LocalScaleRotate();
        distanceToPlayer = GetDictanceToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player") {
            other.GetComponentInChildren<HealthManager>().takeDamage();   
        }
    }

    private void LocalScaleRotate() {
        if (_player.transform.position.x > transform.position.x) 
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        else 
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }
}
