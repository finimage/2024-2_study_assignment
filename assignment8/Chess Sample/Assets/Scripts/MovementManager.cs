using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject effectPrefab;
    private Transform effectParent;
    private List<GameObject> currentEffects = new List<GameObject>();

    public void Initialize(GameManager gameManager, GameObject effectPrefab, Transform effectParent)
    {
        this.gameManager = gameManager;
        this.effectPrefab = effectPrefab;
        this.effectParent = effectParent;
    }

    private bool TryMove(Piece piece, (int, int) targetPos, MoveInfo moveInfo)
    {
        // moveInfo의 distance만큼 direction을 이동시키며 이동이 가능한지를 체크
        // 보드에 있는지, 다른 piece에 의해 막히는지 등을 체크
        // 폰에 대한 예외 처리를 적용
        // --- TODO ---
        (int, int) direction = (moveInfo.dirX, moveInfo.dirY);
        int distance = moveInfo.distance;
        (int, int) currentPos = piece.MyPos;
        for (int i = 1; i <= distance; i++)
        {
            (int, int) nextPos = (currentPos.Item1 + direction.Item1 * i, currentPos.Item2 + direction.Item2 * i);
            if (!Utils.IsInBoard(nextPos)) return false;
            var blockingPiece = gameManager.Pieces[nextPos.Item1, nextPos.Item2];

            if (blockingPiece != null)
            {
                if (blockingPiece.PlayerDirection == piece.PlayerDirection) return false;
                if (piece.GetType().Name == "Pawn")
                {
                    if (direction.Item1 != 1 && direction.Item1 != -1) return false;
                }
            }
            else if (piece.GetType().Name == "Pawn" && (direction.Item1 == 1 || direction.Item1 == -1)) return false;
            if (nextPos == targetPos) return true;
        }
        return false;
        // ------
    }

    private bool IsValidMoveWithoutCheck(Piece piece, (int, int) targetPos)
    {
        if (!Utils.IsInBoard(targetPos) || targetPos == piece.MyPos) return false;

        foreach (var moveInfo in piece.GetMoves())
        {
            if (TryMove(piece, targetPos, moveInfo))
                return true;
        }
        
        return false;
    }

    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMoveWithoutCheck(piece, targetPos)) return false;

        // 체크 상태 검증을 위한 임시 이동
        var originalPiece = gameManager.Pieces[targetPos.Item1, targetPos.Item2];
        var originalPos = piece.MyPos;

        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = piece;
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = null;
        piece.MyPos = targetPos;

        bool isValid = !IsInCheck(piece.PlayerDirection);

        // 원상 복구
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = piece;
        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = originalPiece;
        piece.MyPos = originalPos;

        return isValid;
    }

    private bool IsInCheck(int playerDirection)
    {
        (int, int) kingPos = (-1, -1); // 왕의 위치
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var piece = gameManager.Pieces[x, y];
                if (piece is King && piece.PlayerDirection == playerDirection)
                {
                    kingPos = (x, y);
                    break;
                }
            }
            if (kingPos.Item1 != -1 && kingPos.Item2 != -1) break;
        }

        // 왕이 지금 체크 상태인지를 리턴
        // --- TODO ---
        Debug.Log($"{kingPos.Item1}, {kingPos.Item2}");
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var piece = gameManager.Pieces[x, y];
                if (piece != null && piece.PlayerDirection != playerDirection)
                {
                    foreach (var moveInfo in piece.GetMoves())
                    {
                        if (TryMove(piece, kingPos, moveInfo))
                        {
                            Debug.Log($"{piece}, {kingPos.Item1}, {kingPos.Item2} {"->"} {x},{y}, {moveInfo.dirX}, {moveInfo.dirY}, {moveInfo.distance}");
                            return true;
                        }
                    }
                }
            }
        }
        return false;
        // ------
    }

    public void ShowPossibleMoves(Piece piece)
    {
        ClearEffects();

        // 가능한 움직임을 표시
        // --- TODO ---
        foreach (var moveInfo in piece.GetMoves())
        {
            for (int i = 1; i <= moveInfo.distance; i++)
            {
                (int, int) nextPos = (piece.MyPos.Item1 + moveInfo.dirX * i,  piece.MyPos.Item2 + moveInfo.dirY * i);

                if (!Utils.IsInBoard(nextPos)) break;

                if (TryMove(piece, nextPos, moveInfo))
                {
                    GameObject effect = Instantiate(effectPrefab, effectParent);
                    Vector2 tmp = Utils.ToRealPos(nextPos);
                    effect.transform.position = new Vector3(tmp.x, tmp.y, 0);
                    currentEffects.Add(effect);
                    var blockingPiece = gameManager.Pieces[nextPos.Item1, nextPos.Item2];
                    if (blockingPiece != null) break;
                }
                else
                {
                    break;
                }
            }
        }
        // ------
    }

    public void ClearEffects()
    {
        foreach (var effect in currentEffects)
        {
            if (effect != null) Destroy(effect);
        }
        currentEffects.Clear();
    }
}