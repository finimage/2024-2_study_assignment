using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private GameManager gameManager;
    private Piece selectedPiece = null;
    private Vector3 dragOffset;
    private Vector3 originalPosition;
    private bool isDragging = false;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private (int, int) GetBoardPosition(Vector3 worldPosition)
    {
        float x = worldPosition.x + (Utils.TileSize * Utils.FieldWidth) / 2f;
        float y = worldPosition.y + (Utils.TileSize * Utils.FieldHeight) / 2f;
        
        int boardX = Mathf.FloorToInt(x / Utils.TileSize);
        int boardY = Mathf.FloorToInt(y / Utils.TileSize);
        
        return (boardX, boardY);
    }

    void HandleMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var boardPos = GetBoardPosition(mousePosition);

        if (!Utils.IsInBoard(boardPos)) return;
        // 클릭된 piece을 검증하고, 가능한 이동 경로를 표시
        // --- TODO ---
        Piece piece = gameManager.Pieces[boardPos.Item1, boardPos.Item2];
        if (piece != null && piece.PlayerDirection == gameManager.CurrentTurn)
        {
            selectedPiece = piece;
            dragOffset = selectedPiece.transform.position - mousePosition;
            originalPosition = selectedPiece.transform.position;
            isDragging = true;
            gameManager.ShowPossibleMoves(selectedPiece);
        }
        // ------
    }

    void HandleDrag()
    {
        if (selectedPiece != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            selectedPiece.transform.position = mousePosition + dragOffset;
        }
    }

    void HandleMouseUp()
    {
        if (selectedPiece != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var boardPos = GetBoardPosition(mousePosition);

            // piece의 이동을 검증하고, 이동시킴
            // effect를 초기화
            // --- TODO ---
            Debug.Log($"{boardPos.Item1},{boardPos.Item2}");
            if (gameManager.IsValidMove(selectedPiece, boardPos))
            {
                gameManager.Move(selectedPiece, boardPos);
            }
            else
            {
                selectedPiece.transform.position = originalPosition;
            }
            gameManager.ClearEffects();
            selectedPiece = null;
            isDragging = false;
            // ------
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            HandleDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }
    }
}