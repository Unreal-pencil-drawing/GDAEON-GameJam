using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Action{
        Running,
        Casting,
        Attacking,
        Idle,
    }

    public Action currentAction;

    [SerializeField] public float velocity;
    [SerializeField] public float dashCooldown;
    [SerializeField] public float maxhp;
    [SerializeField] public int damage;
    [SerializeField] public float AfterHitResistenceTime;

    public List<BaseSkill> baseSkills;
    public List<PassiveSkill> passiveSkills;
    public List<ActiveSkill> activeSkills;

    void Awake(){
        baseSkills.Add(gameObject.AddComponent<Run>());
        GetComponent<Run>().Init();
    }
}
