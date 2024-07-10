using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Boss1 : Boss
{

    /* #region Variables */
    private Vector2 direction;
    private List<BossSkill> _bossSkills = new List<BossSkill>{};

    private float betweenSkillCooldown = 10f;
    public float betweenSkillTimer = 0;

    /* #endregion */
    protected override void Awake()
    {
        base.Awake();
        _bossSkills.Add(gameObject.AddComponent<Boss1Dash>());
        _bossSkills.Add(gameObject.AddComponent<Boss1Rush>());
        _bossSkills[0].Init(this, 3);
        _bossSkills[1].Init(this, 7);
    }

    public Vector3 GetPlayerPosition() {
        return _player.transform.position;
    }

    private void Move()
    {
        if (!isAnySkillActive) {
            direction = (Vector2) (_player.transform.position - transform.position);
            theorethicalVelocity = direction.normalized * speed;
            _rigidbody2D.velocity = theorethicalVelocity;
        }
    }

    protected override void Start()
    {
    }

    public override void TakeDamage(float damageValue)
    {
        base.TakeDamage(damageValue);
        if (!isResistance)
        {
            isResistance = true;
            resistanceTimer = resistanceTime;
            Debug.Log("Get Hit");
        }
    }

    private void UpdateTimers()
    {
        if (resistanceTimer > 0)
            resistanceTimer -= Time.deltaTime;
        else
            isResistance = false;
        
        if (betweenSkillTimer > 0) {
            betweenSkillTimer -= Time.deltaTime;
        }
    }

    private void CastSkill(BossSkill skill) {
        Debug.Log(skill.GetType());
        skill.Cast();
        activeSkill = skill;
        isAnySkillActive = true;
    }

    protected override void Update()
    {   
        if (health <= 0 && !isDead)
            Death();
        if (!isDead)
        {
            if (isAnySkillActive) {
                activeSkill.UpdateChanges();
                if (activeSkill.isSkillEnd) {
                    activeSkill = null;
                    isAnySkillActive = false;
                    betweenSkillTimer = betweenSkillCooldown;
                }
            } else {
                Move();
            }
            foreach (BossSkill skill in _bossSkills) {
                if (betweenSkillTimer <= 0)
                    if (!isAnySkillActive && skill.IsTriggerCondition()) 
                        CastSkill(skill);
                skill.UpdateTimer();
            }
            UpdateTimers();
            distanceToPlayer = GetDictanceToPlayer();
            LocalScaleRotate();
        }
    }
}
