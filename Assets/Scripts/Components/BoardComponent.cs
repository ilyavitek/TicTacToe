using System.Collections.Generic;
using UnityEngine;

namespace Components {
    public class BoardComponent : MonoBehaviour
    {
        [SerializeField] private int BOARD_SIZE = 3;
    
        [SerializeField] private Transform TileParent;
        [SerializeField] private GameObject TilePrefab;

        public int BoardSize
        {
            get { return BOARD_SIZE; }
        }

        public List<List<TileComponent>> Tiles { get; private set; }

        private void Awake()
        {
            BuildBoard();
        }

        public void MarkTile(int x, int y, PlayerMark playerMark)
        { 
            Tiles[x][y].SetMark(playerMark);
        }

        #region Board Building

        private void BuildBoard()
        {
            Tiles = new List<List<TileComponent>>();
        
            for (var x = 0; x < BoardSize; x++)
            {
                Tiles.Add(new List<TileComponent>());
            
                for (var y = 0; y < BoardSize; y++)
                {
                    var tile = BuildTile(x, y);
                    Tiles[x].Add(tile);
                }
            }
        }

        private TileComponent BuildTile(int x, int y)
        {
            var tile = Instantiate(TilePrefab, TileParent).GetComponent<TileComponent>();
            tile.Init(x,y);

            return tile;
        }

        #endregion
    }
}