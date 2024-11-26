using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knight.cs
public class Knight : Piece
{
    public override MoveInfo[] GetMoves()
    {
        // --- TODO ---
        return new MoveInfo[]
        {
            // "L"�� �̵� - 8���� ���
            new MoveInfo(2, 1, 1),  // ������ �� ĭ, ���� �� ĭ
            new MoveInfo(2, -1, 1), // ������ �� ĭ, �Ʒ��� �� ĭ
            new MoveInfo(-2, 1, 1), // ���� �� ĭ, ���� �� ĭ
            new MoveInfo(-2, -1, 1),// ���� �� ĭ, �Ʒ��� �� ĭ
            new MoveInfo(1, 2, 1),  // ���� �� ĭ, ������ �� ĭ
            new MoveInfo(1, -2, 1), // ���� �� ĭ, ���� �� ĭ
            new MoveInfo(-1, 2, 1), // �Ʒ��� �� ĭ, ������ �� ĭ
            new MoveInfo(-1, -2, 1) // �Ʒ��� �� ĭ, ���� �� ĭ
        };
        // ------
    }
}