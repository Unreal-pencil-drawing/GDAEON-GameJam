using UnityEngine;

public class Skills : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Player>().baseSkills.Add(gameObject.AddComponent<Run>());
        GetComponent<Run>().Init();

        gameObject.GetComponent<Player>().activeSkills.Add(gameObject.AddComponent<Dash>());
        GetComponent<Dash>().Init();
    }
}
