using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    
    /* #region Variables */
    public float health;
    public float speed;
    public float resistanceTime = 1;
    public bool isResistance;
    // Внутренние переменные
    protected float resistanceTimer;
    protected bool isDead;
    public Player _player;
    protected Vector3 scale;
    protected Rigidbody2D _rigidbody2D;
    protected BoxCollider2D _boxCollider2D;
    protected Vector2 theorethicalVelocity;
    protected bool isAnySkillActive = false;
    protected BossSkill activeSkill = null;

    public float distanceToPlayer;
    /* #endregion */

    public virtual Vector2 GetTeorethicalDirection() => theorethicalVelocity;

    public virtual BoxCollider2D GetBoxCollider2D() {
        return _boxCollider2D;
    }
    public virtual Rigidbody2D GetRigidbody2D() {
        return _rigidbody2D;
    }

    public virtual float GetDictanceToPlayer(){
        if (_player)
            return Vector3.Distance(_player.transform.position, transform.position);
        return -1f;
    } 
    protected virtual void Awake() {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        scale = transform.localScale;
    }
    protected abstract void Start();
    protected abstract void Update();
    protected virtual void Death()
    {
        isDead = true;
        scale = transform.localScale;
        transform.localScale = new Vector3(scale.x, -scale.y, scale.z);
    }
    public virtual void TakeDamage(float damageValue) => health -= damageValue;
    protected virtual void LocalScaleRotate() {
        if (_player.transform.position.x > transform.position.x) 
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        else 
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }
}
