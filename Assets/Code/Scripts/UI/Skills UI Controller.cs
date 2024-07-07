using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    
    [SerializeField] Player player;

    void Update(){
        foreach(Skill skill in player.activeSkills){
            if(skill.cooldownTimer > -100){
                GameObject.Find(skill.title + "UI(Clone)").GetComponent<Image>().fillAmount = (skill.cooldown - skill.cooldownTimer)/skill.cooldown;
            }
        }
    }
}
