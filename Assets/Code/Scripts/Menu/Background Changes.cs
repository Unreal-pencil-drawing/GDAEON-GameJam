using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanges : MonoBehaviour
{
    public Image backgroundImage;
    public Vector4 backgroundColor;
    private int d;

    void Start()
    {
        backgroundImage = GetComponent<Image>();
        backgroundColor = backgroundImage.color;
        d = 1;
    }

    void FixedUpdate()
    {
        if(backgroundImage.color.a <= 0 || backgroundImage.color.a >= 1 ) d *= -1;
        backgroundImage.color = new Vector4(1, 1, 1, backgroundImage.color.a + d*Time.deltaTime*0.1f);
    }
}
