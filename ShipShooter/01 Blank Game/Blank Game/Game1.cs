using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Blank_Game
{
    public class Game1 : Game
    {
        Debug debug;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Background _back;
        Player player;
        private States _currentState;
        private States _nextState;

        enum GameState
        {
            GameMenu = 0,
            GamePlay = 1,
            GamePlay2 = 2,
            GameOver = 3,
        }

        GameState currentGameState;
        GameState currentGameState2;

        public void ChangeState(States state)
        {
            currentGameState = GameState.GamePlay;
            _nextState = state;

        }

        public void ChangeState2(States state)
        {
            currentGameState2 = GameState.GamePlay2;
            _nextState = state;
        }


        Player2 player2;

        Texture2D back;
        Texture2D background, background2, background3, banner;
        AsteroidSpawnerClass asteroidSpawner;
        Song song, S2; //S4
        List<Laser> laserList;
        List<AsteroidClass> asteroidList;

        //private SpriteFont _font;

        Random rand;
        private SpriteFont font;
        int Coll;
        string Level;
        public int counter = 0;
        int Lives = 3;
        int Lives2 = 3;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 1024;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            rand = new Random();
            IsMouseVisible = true;

            laserList = new List<Laser>();
            asteroidList = new List<AsteroidClass>();

            player = new Player(laserList);

            player2 = new Player2(laserList);

            asteroidSpawner = new AsteroidSpawnerClass(asteroidList);

            debug = new Debug(asteroidList, player);
        }

        protected override void Initialize()
        {
            _back = new Background(GraphicsDevice.Viewport);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new Menu(this, graphics.GraphicsDevice, Content);

            debug.LoadContent(Content);
            banner = Content.Load<Texture2D>("SW");
            background = Content.Load<Texture2D>("space 2");
            background2 = Content.Load<Texture2D>("Space");
            background3 = Content.Load<Texture2D>("sw_deathstar_01");
            player.LoadContent(Content);
            player2.LoadContent(Content);

            asteroidSpawner.LoadContent(Content);
            this.song = Content.Load<Song>("Star Wars Theme Song By John Williams");
            this.S2 = Content.Load<Song>("Explosion+1");

            //this.S4 = Content.Load<Song>("Thank Me");

            font = Content.Load<SpriteFont>("Debug");

            //_font = Content.Load<SpriteFont>("Font");

            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.2f;
        }

        protected override void UnloadContent()
        {

        }

        public void CheckColl2()
        {
            for (int a = 0; a < laserList.Count; a++)
            {
                for (int b = 0; b < asteroidList.Count; b++)
                {
                    if (asteroidList[b].bounds.Intersects(laserList[a].bounds))
                    {
                        Coll++;
                        laserList[a].isVisible = false;
                        asteroidList[b].isVisible = false;
                        if (asteroidList[b].scale > 0.25f)
                        {
                            asteroidSpawner.SpawnAsteroid(asteroidList[b].pos, asteroidList[b].scale / 2, rand.Next(2, 4));
                            MediaPlayer.Play(S2);
                            MediaPlayer.Volume = 3f;
                        }
                    }
                }
                //if (laserList[a].bounds.Intersects(player.bounds))
                //{
                //    Lives--;
                //    if (Lives == 0)
                //    {
                //        Exit();
                //    }
                //}
            }

            for (int b = 0; b < asteroidList.Count; b++)
            {
                if (asteroidList[b].bounds.Intersects(player.bounds))
                {
                    asteroidList[b].isVisible = false;
                    Lives--;
                }
                if (Lives == 0)
                {
                    Exit();
                }
                if (asteroidList[b].bounds.Intersects(player2.bounds))
                {
                    asteroidList[b].isVisible = false;
                    Lives--;
                }
                if (Lives2 == 0)
                {
                    Exit();
                }
            }
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                counter++;
            }

            if (counter < 50)
            {
                Level = "Padawan";
                back = background;
            }
            else if (50 <= counter && counter < 100)
            {
                Level = "Jedi";
                back = background2;
            }
            else if (100 <= counter)
            {
                Level = "Grand Master";
                back = background3;
            }

            player.Update(gameTime);

            player2.Update(gameTime);
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);
            asteroidSpawner.Update(gameTime);

            CheckColl2();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            switch (currentGameState)
            {
                case GameState.GameMenu:
                    spriteBatch.Begin();//SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    Vector2 spritePos2 = new Vector2(40, 0);
                    Vector2 spritePos1 = new Vector2(0, 170);
                    spriteBatch.Draw(banner, spritePos2, Color.White);
                    spriteBatch.Draw(background3, spritePos1, Color.White);
                    spriteBatch.End();
                    _currentState.Draw(gameTime, spriteBatch);
                    break;

                case GameState.GamePlay:
                    spriteBatch.Begin();//SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    Vector2 spritePosition = new Vector2(0, 0);
                    spriteBatch.Draw(back, spritePosition, Color.White);
                    player.Draw(spriteBatch);
                    asteroidSpawner.Draw(spriteBatch);
                    debug.Draw(spriteBatch);

                    spriteBatch.DrawString(font, "Press 'Enter' to Start a wave.\nPress 'Space' to Shoot at Enemies \nHold for harder Levels.", new Vector2(1000, 950), Color.Yellow);
                    spriteBatch.DrawString(font, "Controls:\nPress 'W' Button to Move Forward.\nPress 'A' Button to Turn Left.\nPress 'D' Button to Turn Right.", new Vector2(20, 930), Color.Yellow);
                    spriteBatch.DrawString(font, "Wave Level: " + counter, new Vector2(1120, 20), Color.Yellow);
                    spriteBatch.DrawString(font, "Rank: " + Level, new Vector2(1120, 40), Color.Yellow);
                    spriteBatch.DrawString(font, "Score: " + Coll, new Vector2(1120, 60), Color.Yellow);
                    spriteBatch.DrawString(font, "Health: " + Lives, new Vector2(10, 10), Color.Yellow);
                    spriteBatch.End();
                    break;
            }
            switch (currentGameState2)
            { 
                case GameState.GamePlay2:
                    spriteBatch.Begin();
                    Vector2 spritePos3 = new Vector2(0, 0);
                    spriteBatch.Draw(back, spritePos3, Color.White);
                    player.Draw(spriteBatch);
                    player2.Draw(spriteBatch);
                    asteroidSpawner.Draw(spriteBatch);
                    debug.Draw(spriteBatch);

                    spriteBatch.DrawString(font, "Wave Level: " + counter, new Vector2(1120, 20), Color.White);
                    spriteBatch.DrawString(font, "Rank: " + Level, new Vector2(1120, 40), Color.White);
                    spriteBatch.DrawString(font, "Press 'Enter' to Start a wave. \nHold for harder Levels.", new Vector2(1000, 950), Color.White);

                    spriteBatch.DrawString(font, "Press 'Space' to Shoot at Enemies", new Vector2(1000, 890), Color.Yellow);
                    spriteBatch.DrawString(font, "Controls:\nPress 'W' Button to Move Forward.\nPress 'A' Button to Turn Left.\nPress 'D' Button to Turn Right.", new Vector2(20, 870), Color.Yellow);
                    spriteBatch.DrawString(font, "Player 1 Score: " + Coll, new Vector2(1120, 60), Color.Yellow);
                    spriteBatch.DrawString(font, "Player 1 Health: " + Lives, new Vector2(10, 10), Color.Yellow);

                    spriteBatch.DrawString(font, "Press 'Backspace' to Shoot at Enemies", new Vector2(1000, 910), Color.Red);
                    spriteBatch.DrawString(font, "Controls:\nPress 'Up' Button to Move Forward.\nPress 'Left' Button to Turn Left.\nPress 'Right' Button to Turn Right.", new Vector2(20, 940), Color.Red);
                    spriteBatch.DrawString(font, "Player 2 Score: " + Coll, new Vector2(1120, 80), Color.Red);
                    spriteBatch.DrawString(font, "Player 2 Health: " + Lives2, new Vector2(10, 30), Color.Red);
                    spriteBatch.End();
                    break;
            }
             base.Draw(gameTime);
        }
    }
}