using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public (int, int) MyPos;
    Color tileColor = new Color(255 / 255f, 193 / 255f, 204 / 255f);
    SpriteRenderer MySpriteRenderer;

    private void Awake()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Set((int, int) targetPos)
    {
        // targetPos로 이동시키고, 색깔을 지정
        // --- TODO ---
        MyPos = targetPos;
        Vector2 tmp = Utils.ToRealPos(MyPos);
        transform.position = new Vector3(tmp.x, tmp.y, 1);
        MySpriteRenderer.color = (targetPos.Item1 + targetPos.Item2) % 2 == 0 ? tileColor : new Color(1,1,1);
        // ------
    }
}
