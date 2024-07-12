using System;
using System.Collections;
using UnityEngine;

public class Boss2 : Boss
{

    /* #region Variables */

    private Vector2 direction;
    public float betweenSkillCooldown = 2f;
    private float betweenSkillTimer = 0;
    private GameObject _arena;
    private EdgeCollider2D _arenaCollider;
    private Boss2Teleport _teleport;
    public GameObject _squarePrefab;

    public bool isColliderOn;

    public float distanceToArena;
    private float elapsedTime = 1;
    public bool isStoping = false;
    public bool isStop = true;
    protected Vector2[] arenaCorners;
    protected bool once = true;
    private Vector2 closestPoint;
    private float distance;
    private float temp;
    private Vector2 defaultVector = new Vector2(-1, 0);
    public float angle;

    /* #endregion */

    public Vector2 GetClosestPointToArena() => _arenaCollider.ClosestPoint(transform.position);
    public float GetDistanceToClosestArenaPoint() => Vector2.Distance(_arenaCollider.ClosestPoint(transform.position), transform.position);


    public void GetInfoOfClosestArenaPoint(){
        closestPoint = _arenaCollider.ClosestPoint(transform.position);
        distance = Vector2.Distance(closestPoint, transform.position);
    } 


    protected void AddBossSkills() {
        _teleport = gameObject.AddComponent<Boss2Teleport>();
        _bossSkills.Add(_teleport);
        _bossSkills[0].Init(this, 0);
        _bossSkills.Add(gameObject.AddComponent<Boss2SummonSquareAttack>());
        _bossSkills[1].Init(this, 3);
    }
    
    protected override void Awake() {
        base.Awake();
        AddBossSkills();
        _arena = GameObject.Find("Arena");
        _arenaCollider = _arena.GetComponent<EdgeCollider2D>();
        arenaCorners = _arenaCollider.points;
    }

    protected void GetInfoOfClosestCorner() {
        distance = Vector2.Distance(arenaCorners[0], transform.position);
        closestPoint = arenaCorners[0];
        for (int i = 1; i <= 3; i++) {
            temp = Vector2.Distance(arenaCorners[i], transform.position);
            if (temp < distance) {
                distance = temp;
                closestPoint = arenaCorners[i];
            }
        }
    }

    protected override void Move()
    {
        if (!isAnySkillActive) {
            direction = (Vector2) (transform.position - _player.transform.position);

            if (GetDistanceToPlayer() >= 5f) {
                isStoping = true;
            } else if (isStoping || isStop) {
                elapsedTime = 1;
                isStoping = false;
                isStop = false;
            }

            velocity = speed * direction.normalized;
            if (isStoping && !isStop) {
                elapsedTime += Time.deltaTime;
                velocity /= elapsedTime;
                if (elapsedTime > 1.5f) {
                    _rigidbody2D.velocity = new Vector2(0, 0);
                    isStoping = false;
                    isStop = true;
                }
            }
            
            if (!isStop) {
                _rigidbody2D.velocity = velocity;
            }
        }
    }

    protected override void Start()
    {
    }

    protected override void TakeDamage() {
        base.TakeDamage();
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        // if (_teleport.IsTriggerCondition()) {
        //     _teleport.Cast();
        // }
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

    protected override void Update() 
    {   
        isColliderOn = _boxCollider2D.enabled;
        if (health <= 0 && !isDead)
            Death();
        if (!isDead) {
            if (isAnySkillActive) {
                if (activeSkill.isSkillEnd) {
                    activeSkill = null; 
                    isAnySkillActive = false; 
                    isStop = true;
                    betweenSkillTimer = betweenSkillCooldown; 
                } else {
                    activeSkill.UpdateChanges();
                }
                    
            } else {
                Move();
            }
            angle = Vector2.Angle(transform.position - _player.transform.position, defaultVector);
            if (transform.position.y < _player.transform.position.y) {
                angle *= -1;
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
