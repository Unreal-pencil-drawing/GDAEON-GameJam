using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoulSplitter : ActiveSkill
{
    private GameObject damageCollider;
    private bool isColliderCreated;

    public override void Init()
    {
        cooldown = 3;
    }

    public override bool Detect()
    {
        if(Input.GetKey(key) && cooldownTimer <= 0 && !isColliderCreated){
            damageCollider = Instantiate(Resources.Load("Prefabs/Spell area"), Vector3.zero, Quaternion.identity) as GameObject;
            isColliderCreated = true;
            return true;
        }

        if(Input.GetKey(KeyCode.Mouse0) && isColliderCreated){
            gameObject.GetComponent<Player>().currentAction = Player.Action.Casting;
            return true;
        }
        return false;

    }

    public override void Cast()
    {
        StartCoroutine(_Cast());
    }

    private IEnumerator _Cast()
    {
        if (damageCollider) damageCollider.GetComponent<CapsuleCollider2D>().enabled = true;
        cooldownTimer = cooldown;

        yield return new WaitForSeconds(0.05f);

        isColliderCreated = false;
        Destroy(damageCollider);

        yield return new WaitForSeconds(0.2f);

        if(Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a")) gameObject.GetComponent<Player>().currentAction = Player.Action.Running;
        else gameObject.GetComponent<Player>().currentAction = Player.Action.Idle;

        
    }

    public override GameObject createUI()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/SoulSplitterUI"), Vector3.zero, Quaternion.identity, GameObject.Find("Skills Panel").transform) as GameObject;
        obj.name = "SoulSplitter";
        return obj;
    }

    public override void UpdateTimer()
    {
        if(cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
    }
}
