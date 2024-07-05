using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Action{
        Running,
        Attacking,
        Dashing,
        Idle
    }

    public Action currentAction;

    [SerializeField] public float velocity;
    [SerializeField] public float dashCooldown;
    [SerializeField] public float maxhp;
    [SerializeField] public int damage;
}
