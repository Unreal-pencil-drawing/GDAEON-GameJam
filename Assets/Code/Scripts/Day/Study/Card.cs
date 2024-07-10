using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public SkillInfo skill;
    
    public bool choosen;

    public Text title;
    public Text description;
    public GameObject icon;

    public void Start(){
        title.text = skill.title;
        description.text = skill.description;
        if(skill.active) icon.GetComponent<Image>().sprite = skill.icon;
        else icon.SetActive(false);
    }

    public void OnClick(){
        Component newSkill = GameManager.gameManager.player.AddComponent(skill.skillScript.GetClass());

        (newSkill as Skill).Init();
        if(skill.active) 
            GameManager.gameManager.player.GetComponent<Player>().activeSkills.Add(newSkill as ActiveSkill);
        else GameManager.gameManager.player.GetComponent<Player>().passiveSkills.Add(newSkill as PassiveSkill);

        choosen = true;
    }
}
