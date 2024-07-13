using System.Collections;
using UnityEngine;

public class Boss1 : Boss
{

    /* #region Variables */
    private Vector2 direction;

    public float betweenSkillCooldown = 2f;
    private float betweenSkillTimer = 0;

    /* #endregion */

    protected void AddBossSkills() {
        _bossSkills.Add(gameObject.AddComponent<Boss1Dash>());
        _bossSkills.Add(gameObject.AddComponent<Boss1Rush>());
        _bossSkills[0].Init(this, 3);
        _bossSkills[1].Init(this, 2);
    }

    protected override void Awake()
    {
        base.Awake();
        AddBossSkills();
    }
    protected override void Move()
    {
        if (!isAnySkillActive) {
            direction = (Vector2) (_player.transform.position - transform.position);
            velocity = direction.normalized * speed;
            _rigidbody2D.velocity = velocity;
        }
    }

    protected override void Start() {
        
    }

    protected override void TakeDamage()
    {   
        base.TakeDamage();
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 7) {
            TakeDamage();
        }
    }

    private void UpdateTimers()
    {   
        if (betweenSkillTimer > 0) {
            betweenSkillTimer -= Time.deltaTime;
        }
    }

    protected override void Update() {   
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
            LocalScaleRotate();
        }
    }
}
