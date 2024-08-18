using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTransition : SceneTransition
{
    /*
    public Sprite circleSprite;
    public Color color;

    public override IEnumerator Enter(Canvas Parent)
    {
        float time = 0;
        float size = Mathf.Sqrt( Mathf.Pow(Screen.width, 2) + Mathf.Pow(Screen.height, 2) );
        Vector2 initialSize = new Vector2(size, size);
        while(time < 1)
        {
            animatedObject.rectTransform.sizeDelta = Vector2.Lerp(initialSize, Vector2.zero.time);
            yield return null;
            time += time.deltaTime / animationTime;
        }

        Destroy(AnimatedObject.gameObject);
    }
    */
}
