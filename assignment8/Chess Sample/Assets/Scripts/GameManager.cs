using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject[] PiecePrefabs;
    public GameObject EffectPrefab;

    private Transform TileParent;
    private Transform PieceParent;
    private Transform EffectParent;
    private MovementManager movementManager;
    private UIManager uiManager;
    
    public int CurrentTurn = 1;
    public Tile[,] Tiles = new Tile[Utils.FieldWidth, Utils.FieldHeight];
    public Piece[,] Pieces = new Piece[Utils.FieldWidth, Utils.FieldHeight];

    void Awake()
    {
        TileParent = GameObject.Find("TileParent").transform;
        PieceParent = GameObject.Find("PieceParent").transform;
        EffectParent = GameObject.Find("EffectParent").transform;
        
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        movementManager = gameObject.AddComponent<MovementManager>();
        movementManager.Initialize(this, EffectPrefab, EffectParent);
        
        InitializeBoard();
    }

    void InitializeBoard()
    {
        // 8x8로 타일들을 배치
        // --- TODO ---
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                GameObject tileObj = Instantiate(TilePrefab, TileParent);
                Tile tile = tileObj.GetComponent<Tile>();
                tile.Set((x, y));
                Tiles[x, y] = tile;
            }
        }
        // ------

        PlacePieces(1);
        PlacePieces(-1);
    }

    void PlacePieces(int direction)
    {
        // 체스 말들을 배치
        // --- TODO ---
        int row = (direction == 1) ? 0 : Utils.FieldHeight - 1;

        int[] pieceOrder = { 4, 3, 2, 0, 1, 2, 3, 4 };
        for (int col = 0; col < Utils.FieldWidth; col++)
        {
            PlacePiece(pieceOrder[col], (col, row), direction);
        }
        row = (direction == 1) ? 1 : Utils.FieldHeight - 2;
        for (int col = 0; col < Utils.FieldWidth; col++)
        {
            PlacePiece(5, (col, row), direction);
        }
        // ------
    }

    Piece PlacePiece(int pieceType, (int, int) pos, int direction)
    {
        // 체스 말 하나를 배치 후 initialize
        // --- TODO ---
        GameObject pieceObj = Instantiate(PiecePrefabs[pieceType], PieceParent);
        Piece piece = pieceObj.GetComponent<Piece>();
        piece.initialize(pos, direction);
        Pieces[pos.Item1, pos.Item2] = piece;
        return piece;
        // ------
    }

    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        return movementManager.IsValidMove(piece, targetPos);
    }

    public void ShowPossibleMoves(Piece piece)
    {
        movementManager.ShowPossibleMoves(piece);
    }

    public void ClearEffects()
    {
        movementManager.ClearEffects();
    }


    public void Move(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMove(piece, targetPos)) return;

        // 체스 말을 이동하고, 만약 해당 자리에 상대 말이 있다면 삭제
        // --- TODO ---
        Pieces[piece.MyPos.Item1, piece.MyPos.Item2] = null;
        Piece targetPiece = Pieces[targetPos.Item1, targetPos.Item2];
        if (targetPiece != null && targetPiece.PlayerDirection != piece.PlayerDirection)
        {
            Destroy(targetPiece.gameObject);
        }
        piece.MoveTo(targetPos);
        Pieces[targetPos.Item1, targetPos.Item2] = piece;
        ChangeTurn();
        // ------
    }

    void ChangeTurn()
    {
        // 턴을 변경하고, UI에 표시
        // --- TODO ---
        CurrentTurn *= -1;
        uiManager.UpdateTurn(CurrentTurn);
        // ------
    }
}
