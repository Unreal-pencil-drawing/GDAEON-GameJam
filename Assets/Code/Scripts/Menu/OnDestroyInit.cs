using UnityEngine;

public class OnDestroyInit : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);   
    }
}
