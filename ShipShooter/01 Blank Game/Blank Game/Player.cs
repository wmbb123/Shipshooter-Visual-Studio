using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Blank_Game
{
    public class Player
    {
        Texture2D texture, texture2, laserTexture;
        Vector2 pos, origin, direction;
        Rectangle sourceRectangle;
        public Bounds bounds;
        public float angle, acceleration, rateOfFire, rateOfFireCounter, turnSpeed, maxSpeed, speedUpRate, slowDownRate, range;
        bool isDriving;
        List<Laser> laserList;
        Song S3;

        public Player(List<Laser> laserList)
        {
            pos = new Vector2(640, 500);
            angle = 0f;
            rateOfFire = 20;        
            turnSpeed = 0.08f;      
            maxSpeed = 5;           
            speedUpRate = 0.15f;
            slowDownRate = 0.05f;    
            range = 1000;
            rateOfFireCounter = rateOfFire;
            this.laserList = laserList;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("MF");
            texture2 = content.Load<Texture2D>("xWing");
            laserTexture = content.Load<Texture2D>("flare2");
            this.S3 = content.Load<Song>("Laser Blasts-SoundBible.com-108608437");
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            bounds = new Bounds(pos, texture, 1f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Laser laser in laserList)
                laser.Draw(spriteBatch);

            spriteBatch.Draw(texture, pos, sourceRectangle, Color.Yellow, angle, origin, 1.0f, SpriteEffects.None, 1);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            bounds.x = pos.X;
            bounds.y = pos.Y;

            UpdateLasers(gameTime);

            if (pos.X > 2020)
                pos.X = -100;
            else if (pos.X < -100)
                pos.X = 2020;
            if (pos.Y > 1180)
                pos.Y = -100;
            else if (pos.Y < -100)
                pos.Y = 1180;

            if (keyState.IsKeyDown(Keys.A))
                angle -= turnSpeed;
            if (keyState.IsKeyDown(Keys.D))
                angle += turnSpeed;

            if (keyState.IsKeyDown(Keys.W))
            {
                if (isDriving == false)
                {
                    isDriving = true;
                    acceleration = 1;
                }
                direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                direction.Normalize();
                pos += direction * acceleration;
                acceleration += speedUpRate;
                if (acceleration > maxSpeed)
                    acceleration = maxSpeed;
            }
            else
            {
                isDriving = false;
                acceleration -= slowDownRate;
            }

            if (acceleration < 0)
                acceleration = 0;

            pos += direction * acceleration;

            if (keyState.IsKeyDown(Keys.Space))
            {
                Shoot();
                MediaPlayer.Play(S3);
                MediaPlayer.Volume = 1f;
            }

            if (rateOfFireCounter < rateOfFire)
                rateOfFireCounter++;
        }

        public void Shoot()
        {
            if (rateOfFireCounter == rateOfFire)
            {
                Laser newLaser = new Laser(pos, angle, laserTexture);
                newLaser.isVisible = true;
                laserList.Add(newLaser);
            }

            if (rateOfFireCounter == rateOfFire)
                rateOfFireCounter = 0;
        }

        public void UpdateLasers(GameTime gameTime)
        {
            foreach (Laser laser in laserList)
            {
                laser.Update(gameTime);
                if (laser.checkRange(range))
                {
                    laser.isVisible = false;
                }

            }

            for (int i = 0; i < laserList.Count; i++)
            {
                if (!laserList[i].isVisible)
                {
                    laserList.RemoveAt(i);
                    i--;
                }
            }    
        }
    }
}
