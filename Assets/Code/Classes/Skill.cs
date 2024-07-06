using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string title;
    public string description;
    public float cooldown, cooldownTimer; 
    public Sprite uisprite;

    public abstract void Init();
    public abstract void Cast();
    public abstract bool Detect();
    public abstract void UpdateTimer();
}
