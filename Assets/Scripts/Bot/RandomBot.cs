using System.Collections.Generic;
using System.Linq;
using Components;

namespace Bot {
    public class RandomBot : IBot
    {
        private readonly List<List<TileComponent>> _tiles;

        public void PlayTurn()
        {
            var emptyTiles = _tiles.SelectMany(_ => _.Where(t => t.Mark == PlayerMark.None));
        
            var randomIndex = new System.Random().Next(0, emptyTiles.Count());
            var randomEmptyTile = emptyTiles.ElementAt(randomIndex);
        
            randomEmptyTile.BotTurn();
        }
    }
}