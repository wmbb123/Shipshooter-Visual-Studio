using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Boss Level

namespace Blank_Game
{
    class BigBadBoss
    {
        Random rand;
        Texture2D texture;
        public Vector2 origin, direction;
        Rectangle sourceRectangle;
        double rotationDirection;
        public float angle, speed, scale;
        public bool isVisible;
        public Bounds bounds;
        public Vector2 pos;

        public BigBadBoss(Texture2D texture, float scale, Vector2 pos)
        {
            rand = new Random();
            this.texture = texture;
            this.scale = scale;
            this.pos = pos;
            angle = degreesToRadian(rand.Next(0, 360));
            direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            isVisible = true;
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            bounds = new Bounds(pos, texture, scale);
            speed = (float)rand.NextDouble() * (3 - 1) + 1;

            setupRotation();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, sourceRectangle, Color.White, angle, origin, scale, SpriteEffects.None, 1f);
        }

        public void Update(GameTime gameTime)
        {
            bounds.x = pos.X;
            bounds.y = pos.Y;

            pos += direction * speed;

            if (pos.X > 2120)
                pos.X = -200;
            else if (pos.X < -200)
                pos.X = 2120;
            if (pos.Y > 1280)
                pos.Y = -200;
            else if (pos.Y < -200)
                pos.Y = 1280;

            angle += (float)rotationDirection;

        }

        public float degreesToRadian(int degrees)
        {
            float radian = (float)(Math.PI / 180) * degrees;
            return radian;
        }

        public void setupRotation()
        {
            rotationDirection = rand.NextDouble() * (0.01f - 0.005f) + 0.005f;

            int x = rand.Next(2);
            if (x == 1)
                rotationDirection = -rotationDirection;
        }

    }
}