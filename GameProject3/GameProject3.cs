using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject3
{
    public class GameProject3 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Protagonist protagonist;
        private InputManager IO;

        public GameProject3()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            protagonist = new Protagonist(this, new Vector2(40, 400));
            IO = new InputManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            protagonist.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (IO.Exit) { Exit(); }

            // TODO: Add your update logic here
            IO.Update(gameTime);

            //Gametime, Direction, Moving, Flipped, Jumping, Attacking, Shift, 
            protagonist.Update(gameTime,IO.Direction,IO.Moving, IO.Flipped, IO.Jumping, IO.Attacking, IO.Shift, 
                //on platform, stop left movement, stop right movement, stop jump, stop fall, stop all moving
                  true,           false,                 false,          false,     false,     false);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            protagonist.Draw(gameTime, _spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
