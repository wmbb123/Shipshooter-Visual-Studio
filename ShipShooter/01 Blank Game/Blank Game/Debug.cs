using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Blank_Game
{
    class Debug
    {
        private SpriteFont font;
        List<AsteroidClass> asteroidList;
        Player player;

        public Debug(List<AsteroidClass> asteroidList, Player player)
        {
            this.asteroidList = asteroidList;
            this.player = player;
        }

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Debug");
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
