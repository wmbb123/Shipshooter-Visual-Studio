using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blank_Game
{
    public class Bounds
    {
        public float radius;
        public float x;
        public float y;

        public Bounds(Vector2 pos, Texture2D texture, float scale)
        {
            radius = (texture.Width / 5) * scale;
            x = pos.X + texture.Width / 2;
            y = pos.Y + texture.Height / 2;
        }

        public bool Intersects(Bounds bounds)
        {
            double distance = Math.Sqrt((Math.Pow(x - bounds.x, 2) + (Math.Pow(y - bounds.y, 2))));
            if (distance <= radius + bounds.radius)
            {
                return true;
            }
            else
                return false;
        }
    }
}
