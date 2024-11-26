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
            // "L"ÀÚ ÀÌµ¿ - 8°¡Áö °æ¿ì
            new MoveInfo(2, 1, 1),  // ¿À¸¥ÂÊ µÎ Ä­, À§·Î ÇÑ Ä­
            new MoveInfo(2, -1, 1), // ¿À¸¥ÂÊ µÎ Ä­, ¾Æ·¡·Î ÇÑ Ä­
            new MoveInfo(-2, 1, 1), // ¿ÞÂÊ µÎ Ä­, À§·Î ÇÑ Ä­
            new MoveInfo(-2, -1, 1),// ¿ÞÂÊ µÎ Ä­, ¾Æ·¡·Î ÇÑ Ä­
            new MoveInfo(1, 2, 1),  // À§·Î µÎ Ä­, ¿À¸¥ÂÊ ÇÑ Ä­
            new MoveInfo(1, -2, 1), // À§·Î µÎ Ä­, ¿ÞÂÊ ÇÑ Ä­
            new MoveInfo(-1, 2, 1), // ¾Æ·¡·Î µÎ Ä­, ¿À¸¥ÂÊ ÇÑ Ä­
            new MoveInfo(-1, -2, 1) // ¾Æ·¡·Î µÎ Ä­, ¿ÞÂÊ ÇÑ Ä­
        };
        // ------
    }
}