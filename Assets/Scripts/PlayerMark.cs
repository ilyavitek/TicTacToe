using System;
using UnityEngine;

public enum PlayerMark
{
    None,
    X,
    O
}

public static class PlayerMarkExtensions
{
    public static string GetMarkByType(this PlayerMark mark)
    {
        switch (mark)
        {
            case PlayerMark.X:
                return "X";
            case PlayerMark.O:
                return "O";
            case PlayerMark.None:
                return "";
            default:
                Debug.LogError($"Not supported PlayerMark {mark}");
                throw new IndexOutOfRangeException();
        }
    }

    public static PlayerMark SwitchTurn(this PlayerMark mark)
    {
        switch (mark)
        {
            case PlayerMark.X:
                return PlayerMark.O;
            case PlayerMark.O:
                return PlayerMark.X;
            default:
                Debug.LogError($"Not supported PlayerMark {mark}");
                throw new IndexOutOfRangeException();
        }
    }
}