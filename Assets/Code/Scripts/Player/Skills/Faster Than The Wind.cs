using UnityEngine;

public class FasterThanTheWind : PassiveSkill
{
    private Player player;

    public override void Init(){
        player = gameObject.GetComponent<Player>();
        player.velocity += 4;
    } 

    public override GameObject createUI()
    {
        throw new System.NotImplementedException();
    }
}
