using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blank_Game
{
    public class Laser
    {
        Texture2D texture;
        Vector2 pos;
        Vector2 startingPos;
        Vector2 origin;
        Vector2 direction;
        Rectangle sourceRectangle;
        float angle;
        float speed;
        float distance;
        public bool isVisible;
        public Bounds bounds;


        public Laser(Vector2 pos, float angle, Texture2D texture)
        {
            this.pos = pos;
            startingPos = pos;
            this.angle = angle;
            this.texture = texture;
            speed = 20f;
            isVisible = false;
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            bounds = new Bounds(pos, texture, 1f);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }

        public void Update(GameTime gameTime)
        {
            direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            direction.Normalize();

            distance = (float)Math.Sqrt(Math.Pow(pos.X - startingPos.X, 2) + Math.Pow(pos.Y - startingPos.Y, 2));

            pos += direction * speed;

            bounds.x = pos.X;
            bounds.y = pos.Y;
        }

        public bool checkRange(float range)
        {
            if (distance >= range)
                return true;
            else
                return false;
        }
    }
}
