using UnityEngine;

public class KillingSharpness : PassiveSkill
{
    private Player player;

    public override void Init(){ 
        player = gameObject.GetComponent<Player>();
        player.damage += 10;
    } 

    public override GameObject createUI()
    {
        throw new System.NotImplementedException();
    }
}
