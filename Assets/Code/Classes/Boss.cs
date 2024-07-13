using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    /* #region Variables */
    public float health;
    public float speed;

    public static event Action Dead;
    // Внутренние переменные
    protected bool isDead;
    public Player _player;
    protected Vector3 scale;
    public Rigidbody2D _rigidbody2D { get; protected set; }
    public BoxCollider2D _boxCollider2D { get; protected set; }
    public SpriteRenderer _spriteRenderer { get; protected set; }
    protected Vector2 velocity;
    protected bool isAnySkillActive = false;
    protected BossSkill activeSkill = null;
    protected List<BossSkill> _bossSkills = new List<BossSkill>{};
    /* #endregion */

    public virtual Vector3 GetPlayerPosition() {
        return _player.transform.position;
    }

    public virtual float GetDistanceToPlayer(){
        if (_player)
            return Vector3.Distance(_player.transform.position, transform.position);
        return -1f;
    } 
    protected virtual void Awake() {
        _player = GameManager.gameManager.player.GetComponent<Player>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        scale = transform.localScale;
    }
    protected abstract void Start();
    protected abstract void Update();

    protected virtual void CastSkill(BossSkill skill) {
        Debug.Log(this + " cast: " + skill.GetType());
        skill.Cast();
        activeSkill = skill;
        isAnySkillActive = true;
    }

    protected virtual void Death()
    {
        _rigidbody2D.velocity = new Vector2(0, 0);
        isDead = true;
        scale = transform.localScale;
        transform.localScale = new Vector3(scale.x, -scale.y, scale.z);
        Dead.Invoke();
    }

    protected virtual void Move()
    {
        _rigidbody2D.velocity = new Vector2(0, 0);
    }

    protected virtual void TakeDamage() {
        Debug.Log(this + "get hit");
        health -= _player.damage;
        OnTakeDamage();
    }

    protected IEnumerator DamageAnimation() {
        _spriteRenderer.color = new Color(1f, 0, 0, 1f);
        yield return new WaitForSeconds(0.2f);  
        _spriteRenderer.color = Color.white;
    }

    protected virtual void OnTakeDamage() {
        StartCoroutine(DamageAnimation());
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);

    protected virtual void LocalScaleRotate() {
        if (_player.transform.position.x > transform.position.x) 
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        else 
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }
}
