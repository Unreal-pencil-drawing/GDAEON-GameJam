using UnityEngine;

public class MagicShield : ActiveSkill
{
    private Player player;

    public override void Init() {
        player = GameManager.gameManager.player.GetComponent<Player>();
        cooldown = 10;    
    }

    public override void Cast()
    {
        (player.baseSkills[1] as TakeDamage).AfterHitResistenceTimer = 3;
        cooldownTimer = cooldown;

        if(Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a")) player.currentAction = Player.Action.Running;
        else player.currentAction = Player.Action.Idle;
    }

    public override GameObject createUI()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MagicShieldUI"), Vector3.zero, Quaternion.identity, GameObject.Find("Skills Panel").transform) as GameObject;
        obj.name = "MagicShield";
        return obj;
    }

    public override bool Detect()
    {
        if(Input.GetKey(key) && cooldownTimer <= 0){
            player.currentAction = Player.Action.Casting;
            return true;
        }
        return false;
    }

    public override void UpdateTimer()
    {
        if(cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
    }
}
