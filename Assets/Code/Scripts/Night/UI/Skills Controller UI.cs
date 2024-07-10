using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    private Player player;
    public List<GameObject> skillsUI;

    void Start(){
        player = GameManager.gameManager.player.GetComponent<Player>();
        foreach(ActiveSkill skill in player.activeSkills){
            skillsUI.Add(skill.createUI() as GameObject);
            Debug.Log(skill);
        }
    }

    void Update(){
        int i = 0;
        foreach(ActiveSkill skill in player.activeSkills){
            if(skill.cooldownTimer > -100){
                skillsUI[i].GetComponent<Image>().fillAmount = (skill.cooldown - skill.cooldownTimer)/skill.cooldown;
                i++;
            }
        }
    }
}
