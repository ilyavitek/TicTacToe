using System.Collections.Generic;
using System.Linq;
using Components;

public class WinChecker
{
    private readonly int _boardSize;
    private readonly List<List<TileComponent>> _tiles;
    
    public WinChecker(BoardComponent boardComponent)
    {
        _boardSize = boardComponent.BoardSize;
        _tiles = boardComponent.Tiles;
    }
    
    public bool DidPlayerWon(int x, int y, PlayerMark playerMark)
    {
        return WonVertically(x, playerMark) || 
               WonHorizontally(y, playerMark) ||
               WonDiagonally(playerMark) ||
               WonInverseDiagonally(playerMark);
    }
    
    public bool IsDraw()
    {
        return !_tiles.SelectMany(_ => _.Where(t => t.Mark == PlayerMark.None)).Any();
    }

    #region Win Conditions

    private bool WonHorizontally(int y, PlayerMark playerMark)
    {
        for (var i = 0; i < _boardSize; i++)
        {
            if (_tiles[i][y].Mark != playerMark)
                return false;
        }
        return true;
    }

    private bool WonVertically(int x, PlayerMark playerMark)
    {
        for (var i = 0; i < _boardSize; i++)
        {
            if (_tiles[x][i].Mark != playerMark)
                return false;
        }
        return true;
    }
    
    private bool WonDiagonally(PlayerMark playerMark)
    {
        for (var i = 0; i < _boardSize; i++)
        {
            if (_tiles[i][i].Mark != playerMark)
                return false;
        }
        return true;
    }
    
    private bool WonInverseDiagonally(PlayerMark playerMark)
    {
        for (var i = 0; i < _boardSize; i++)
        {
            if (_tiles[i][_boardSize - i - 1].Mark != playerMark)
                return false;
        }
        return true;
    }

    #endregion
}