using System.Collections.Generic;
using System.Linq;
using Components;

namespace Bot {
    public class WinMoveSeekerBot : IBot
    {
        private readonly int _boardSize;
        private readonly List<List<TileComponent>> _tiles;
        private readonly PlayerMark _playerMark;
        private readonly WinChecker _winChecker;

        public WinMoveSeekerBot(BoardComponent boardComponent, PlayerMark playerMark, WinChecker winChecker)
        {
            _boardSize = boardComponent.BoardSize;
            _tiles = boardComponent.Tiles;
            _playerMark = playerMark;
            _winChecker = winChecker;
        }

        public void PlayTurn()
        {
            for (var x = 0; x < _boardSize; x++)
            {
                for (var y = 0; y < _boardSize; y++)
                {
                    if (_tiles[x][y].Mark == PlayerMark.None && IsWinMove(x, y, _playerMark))
                    {
                        _tiles[x][y].BotTurn();
                        return;
                    }
                }
            }

            //RandomBotTurn();
            var emptyTiles = _tiles.SelectMany(_ => _.Where(t => t.Mark == PlayerMark.None));
        
            var randomIndex = new System.Random().Next(0, emptyTiles.Count());
            var randomEmptyTile = emptyTiles.ElementAt(randomIndex);
        
            randomEmptyTile.BotTurn();
        }
    
        private bool IsWinMove(int x, int y, PlayerMark mark)
        {
            _tiles[x][y].SetMark(mark);

            var isWinMove = _winChecker.DidPlayerWon(x, y, mark);
        
            _tiles[x][y].ClearTile();

            return isWinMove;
        }
    }
}