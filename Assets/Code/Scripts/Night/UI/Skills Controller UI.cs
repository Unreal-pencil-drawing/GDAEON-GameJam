using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    private Player player;
    public List<GameObject> skillsUI;

    private GameObject _skill;
    void Awake(){
        player = GameManager.gameManager.player.GetComponent<Player>();
        foreach(ActiveSkill skill in player.activeSkills){
            _skill = skill.createUI();
            skillsUI.Add(_skill);
            _skill.transform.Find("KeyText").GetComponent<Text>().text = skill.key.ToString().Replace("Alpha", ""); 
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
