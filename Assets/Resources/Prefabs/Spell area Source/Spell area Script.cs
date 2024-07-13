using UnityEngine;

public class SpellArea : MonoBehaviour
{
    Vector2 position;
    void Awake(){
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = position;
    }

    void Update()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = position;
    }
}
