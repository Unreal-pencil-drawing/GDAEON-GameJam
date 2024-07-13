using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<SkillInfo> pool;
    public List<SkillInfo> currentPool = new List<SkillInfo>();
    public Card card1, card2, card3;

    void Awake()
    {   
        pool = GameManager.gameManager.SkillPool;

        int i;
        while(currentPool.Count < 3){
            i = Random.Range(0, pool.Count);
            if(!currentPool.Contains(pool[i])) currentPool.Add(pool[i]);
        }
        card1.skill = currentPool[0];
        card2.skill = currentPool[1];
        card3.skill = currentPool[2];

        Card.Choosen += deleteSkillFromPool;
    }

    void deleteSkillFromPool(){
        if(card1.choosen) GameManager.gameManager.SkillPool.Remove(card1.skill);
        if(card2.choosen) GameManager.gameManager.SkillPool.Remove(card2.skill);
        if(card3.choosen) GameManager.gameManager.SkillPool.Remove(card3.skill);
        Card.Choosen -= deleteSkillFromPool;
        GameManager.gameManager.changeScene();
    }
}
