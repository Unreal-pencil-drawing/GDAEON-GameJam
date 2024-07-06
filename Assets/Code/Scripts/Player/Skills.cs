using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("MANAGER");
        gameObject.GetComponent<Player>().skills.Add(gameObject.AddComponent<Dash>());
        GetComponent<Dash>().Init();
    }
}
