using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Action{
        Running,
        Casting,
        Attacking,
        Idle,
        DEAD
    }

    public Action currentAction;

    [SerializeField] public float velocity;
    [SerializeField] public float dashCooldown;
    [SerializeField] public int maxhp;
    [SerializeField] public int damage;
    [SerializeField] public float AfterHitResistenceTime;

    public List<BaseModule> baseSkills;
    public List<PassiveSkill> passiveSkills;
    public List<ActiveSkill> activeSkills;

    public void Init(){
        baseSkills.Add(gameObject.AddComponent<Run>());
        GetComponent<Run>().Init();

        baseSkills.Add(gameObject.AddComponent<TakeDamage>());
        GetComponent<TakeDamage>().Init();

        baseSkills.Add(gameObject.AddComponent<Attack>());
        GetComponent<Attack>().Init();
    }
}
