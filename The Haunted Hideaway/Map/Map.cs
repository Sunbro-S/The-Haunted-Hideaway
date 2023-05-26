using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Haunted_Hideaway.Map;

public class Map
{
    public List<CollisionTiles> CollisionTilesList { get; } = new();
    public int Width { get; set; }
    public int Height { get; set; }

    public Map() { }

    public void Generate(int[,] map, int size)
    {
        for(var x=0; x<map.GetLength(1);x++)
        for (var y = 0; y < map.GetLength(0); y++)
        {
            var number = map[y, x];
            if (number > 0)
                CollisionTilesList.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
            Width = (x + 1) * size;
            Height = (y + 1) * size;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var tile in CollisionTilesList)
            tile.Draw(spriteBatch);
    }
}