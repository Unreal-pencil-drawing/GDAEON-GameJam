using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill")]
public class SkillInfo : ScriptableObject
{
    public string title;
    public string description;
    public bool active;

    public string skillType;
    public Sprite icon;
}
