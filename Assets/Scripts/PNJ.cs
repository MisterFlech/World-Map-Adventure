using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : MonoBehaviour
{
    private SpriteAnimation _spriteAnimation;
    public SpriteRenderer spriteRenderer;
    public int numDialog = 0;

    // Start is called before the first frame update
    void Start()
    {
        _spriteAnimation = this.gameObject.GetComponent<SpriteAnimation>();
        GridManager.addElementInGrid(this.gameObject, TileType.PNJ, transform.position);
    }

    public void changeDirection(int directionHero)
    {
        if (directionHero == (int) Direction.Right)
        {
            spriteRenderer.sprite = _spriteAnimation.spritesLeft[0];
        }
        else if (directionHero == (int)Direction.Left)
        {
            spriteRenderer.sprite = _spriteAnimation.spritesRight[0];
        }
        else if (directionHero == (int)Direction.Down)
        {
            spriteRenderer.sprite = _spriteAnimation.spritesUp[0];
        }
        else if (directionHero == (int)Direction.Up)
        {
            spriteRenderer.sprite = _spriteAnimation.spritesDown[0];
        }
    }
}
