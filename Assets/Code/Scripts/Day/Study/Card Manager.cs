using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<SkillInfo> pool = new List<SkillInfo>();
    public List<SkillInfo> currentPool = new List<SkillInfo>();
    public Card card1, card2, card3;

    void Awake()
    {   
        int i;
        while(currentPool.Count < 3){
            i = Random.Range(0, pool.Count);
            if(!currentPool.Contains(pool[i])) currentPool.Add(pool[i]);
        }
        card1.skill = currentPool[0];
        card2.skill = currentPool[1];
        card3.skill = currentPool[2];
    }

    void Update(){
        //подписаться на событие, использовать делегат
        if(card1.choosen || card2.choosen || card3.choosen) GameManager.gameManager.changeScene();
    }
}
