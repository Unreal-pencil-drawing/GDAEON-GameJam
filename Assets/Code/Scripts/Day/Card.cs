using System;
using UnityEngine;
using UnityEngine.UI;

/*maybe to do */
public class Card : MonoBehaviour
{
    public SkillInfo skill;
    public int skillIndex;
    
    public bool choosen;

    public Text title;
    public Text description;
    public GameObject icon;

    public static event Action Choosen;

    public void Start(){
        title.text = skill.title;
        description.text = skill.description;
        icon.GetComponent<Image>().sprite = skill.icon;
    }

    public void OnClick(){
        Player player = GameManager.gameManager.player.GetComponent<Player>();

        if(skill.active){
            ActiveSkill newSkill = GameManager.gameManager.player.AddComponent(Type.GetType(skill.skillType)) as ActiveSkill;
            KeyCode key = calculateKeyCode(player);
            player.activeSkills.Add(newSkill);
            newSkill.Init(key);
        }
        else{
            PassiveSkill newSkill = GameManager.gameManager.player.AddComponent(Type.GetType(skill.skillType)) as PassiveSkill;
            player.passiveSkills.Add(newSkill);
            newSkill.Init();
        }
        choosen = true;
        Choosen.Invoke();
    }

    private KeyCode calculateKeyCode(Player player){
        KeyCode[] keyCodes = {
		    KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
		    KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
	    };

        KeyCode key = keyCodes[player.activeSkills.Count];
        if(GameManager.gameManager.player.GetComponent<Dash>() != null) key -= 1;
        return key;
    }
}
