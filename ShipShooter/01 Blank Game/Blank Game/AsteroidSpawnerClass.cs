using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Blank_Game
{
   class AsteroidSpawnerClass
   {
        Texture2D texture;
        List<AsteroidClass> asteroidList;
        Random rand;

    public AsteroidSpawnerClass(List<AsteroidClass> asteroidList)
    {
        rand = new Random();
        this.asteroidList = asteroidList;
    }

    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>("boulder1");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
            foreach (AsteroidClass asteroid in asteroidList)
            asteroid.Draw(spriteBatch);
        }

    public void Update(GameTime gameTime)
    {
        UpdateAsteroids(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            SpawnAsteroid(SetRandomSpawn(), 1f, 1);
    }

    public void SpawnAsteroid(Vector2 pos, float scale, int amount)
    {
        for (int i = 1; i <= amount; i++)
        {
            AsteroidClass newAsteroid = new AsteroidClass(texture, scale, pos);
            asteroidList.Add(newAsteroid);
        }
    }

    public void UpdateAsteroids(GameTime gameTime)
    {
        foreach (AsteroidClass asteroid in asteroidList)
        {
            asteroid.Update(gameTime);
        }

        for (int i = 0; i < asteroidList.Count; i++)
        {
            if (!asteroidList[i].isVisible)
            {
                asteroidList.RemoveAt(i);
                i--;
            }
        }
    }

    public Vector2 SetRandomSpawn()
    {
        int side = rand.Next(4);

        switch (side)
        {
            case 0:
                return new Vector2(2120, rand.Next(0, 1080));

            case 1:
                return new Vector2(rand.Next(0, 1920), 1280);

            case 2:
                return new Vector2(-200, rand.Next(0, 1080));

            case 3:
                return new Vector2(rand.Next(0, 1920), -200);

            default:
                throw new ArgumentException("");
        }
    }
}
}

