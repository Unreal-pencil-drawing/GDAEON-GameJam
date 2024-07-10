using UnityEngine;

public class SteelSkin : PassiveSkill
{
    private Player player;

    public override void Init(){
        player = gameObject.GetComponent<Player>();
        player.maxhp += 2;
    } 

    public override GameObject createUI()
    {
        throw new System.NotImplementedException();
    }
}
