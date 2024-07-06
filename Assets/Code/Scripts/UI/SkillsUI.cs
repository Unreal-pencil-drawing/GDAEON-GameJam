using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    
    [SerializeField] Player player;
    private GameObject uichild;

    void Start()
    {
        Debug.Log("UI");
        foreach(Skill skill in player.skills){
            uichild = new GameObject(skill.title);
            
            uichild.AddComponent<Image>();
            uichild.GetComponent<Image>().sprite = skill.uisprite;
            uichild.GetComponent<Image>().type = Image.Type.Filled;

            uichild.transform.SetParent(transform);
            uichild.transform.localScale = new Vector2(1, 1);
        }
    }

    void Update(){
        foreach(Skill skill in player.skills){
            if(skill.cooldownTimer > -100){
                GameObject.Find(skill.title).GetComponent<Image>().fillAmount = (skill.cooldown - skill.cooldownTimer)/skill.cooldown;
            }
        }
    }
}
