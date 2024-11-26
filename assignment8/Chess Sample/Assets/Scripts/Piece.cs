using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public (int, int) MyPos;
    public int PlayerDirection = 1;
    
    public Sprite WhiteSprite;
    public Sprite BlackSprite;
    
    protected GameManager MyGameManager;
    protected SpriteRenderer MySpriteRenderer;

    void Awake()
    {
        MyGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void initialize((int, int) targetPos, int direction)
    {
        PlayerDirection = direction;
        initSprite(PlayerDirection);
        MoveTo(targetPos);
    }

    void initSprite(int direction)
    {
        // direction에 따라 sprite를 설정하고 회전함
        // --- TODO ---
        if (direction == 1)
        {
            MySpriteRenderer.sprite = WhiteSprite;
        }
        else
        {
            MySpriteRenderer.sprite = BlackSprite;
        }
        // ------
    }

    public void MoveTo((int, int) targetPos)
    {
        // 말을 이동시킴
        // --- TODO ---
        MyPos = targetPos;
        Vector2 tmp = Utils.ToRealPos(MyPos);
        transform.position = new Vector3(tmp.x, tmp.y, 0);
        // ------
    }

    public abstract MoveInfo[] GetMoves();
}
