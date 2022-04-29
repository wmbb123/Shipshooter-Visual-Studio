using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Blank_Game.Controls;

namespace Blank_Game
{
    public class Menu : States
    {
        private List<Component> _components;

        public Menu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)

          : base(game, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Button");
            var buttonFont = content.Load<SpriteFont>("Debug");
            var newGameButton = new ButtonMenu(buttonTexture, buttonFont)
            {
                Position = new Vector2(620, 500),
                Text = "1 Player",
            };
            newGameButton.Click += NewGameButton_Click;
            var loadGameButton = new ButtonMenu(buttonTexture, buttonFont)
            {
                Position = new Vector2(620, 550),
                Text = "2 Player",
            };
            loadGameButton.Click += LoadGameButton_Click;
            var quitGameButton = new ButtonMenu(buttonTexture, buttonFont)
            {
                Position = new Vector2(620, 600),
                Text = "Quit Game",
            };
            quitGameButton.Click += QuitGameButton_Click;
            _components = new List<Component>()
      {
        newGameButton,
        loadGameButton,
        quitGameButton,
      };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState2(new GameState(_game, _graphicsDevice, _content));
        }
        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
