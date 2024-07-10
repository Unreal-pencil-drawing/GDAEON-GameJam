using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill")]
public class SkillInfo : ScriptableObject
{
    public string title;
    public string description;
    public bool active;

    public MonoScript skillScript;
    public Sprite icon;
}
