using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    public Sprite dmgSprite;
    public int hp = 4;

    private SpriteRenderer spriteRenderer;
    

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int lose)
    {
        spriteRenderer.sprite = dmgSprite;
        hp -= lose;
        if (hp <= 0)
            gameObject.SetActive(false);
    }

}
