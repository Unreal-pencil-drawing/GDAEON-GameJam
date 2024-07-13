using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    public Sprite _spriteNoDamage;
    public Sprite _spriteDamage;

    public static float attackDelay = 0.75f;
    public static float existTime = 0.75f;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(Invoke());
    }

    public IEnumerator Invoke() {
        yield return new WaitForSeconds(attackDelay);
        _spriteRenderer.sprite = _spriteDamage;
        _boxCollider2D.enabled = true;
        yield return new WaitForSeconds(existTime);
        Destroy(gameObject);
    }
}
