using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Collisions.Collisions;

namespace GameProject3
{
    class Protagonist
    {
        #region Animation
        /// <summary>
        /// Helps flip the animation in the Draw()
        /// </summary>
        SpriteEffects spriteEffect;

        /// <summary>
        /// Helps animate the sprite
        /// </summary>
        private double animationTimer;

        /// <summary>
        /// Helps animate the sprite
        /// </summary>
        private short animationFrame;

        /// <summary>
        /// Helps animate the sprite
        /// will help loop the animation sprite back to frame 0
        /// </summary>
        private int totalFrames;

        /// <summary>
        /// Helps animate the sprite
        /// Determines how quickly the draw() goes through frames
        /// </summary>
        private double animationSpeed;

        /// <summary>
        /// height of the animations sprite
        /// </summary>
        private int height;

        /// <summary>
        /// width of the animations sprite
        /// </summary>
        private int width;

        /// <summary>
        /// texture object to display in the Draw()
        /// </summary>
        private Texture2D texture;
        #endregion

        /// <summary>
        /// Vector2 Direction for sprite velocity
        /// </summary>
        public Movement Direction = Movement.Idle;

        /// <summary>
        /// Which way is the player facing
        /// When holding the Left Key, it is true.
        /// When holding the Right Key, it is false.
        /// </summary>
        public bool Flipped;

        /// <summary>
        /// Determines if we're actively holding the Left 
        /// or Right Keys for moving animation purposes.
        /// </summary>
        public bool Moving;

        /// <summary>
        /// Determines if we're jumping
        /// </summary>
        public bool Jumping;

        /// <summary>
        /// Determines if our player is attacking
        /// </summary>
        public bool Attacking;

        /// <summary>
        /// Determines if our player is holding shift
        /// </summary>
        public bool Shift;

        /// <summary>
        /// Helper attribute for ResetGame()
        /// </summary>
        private Vector2 initialPosition;

        /// <summary>
        /// Velocity helper and used for speed item upgrades
        /// </summary>
        private int SpeedMultiplier = 1;

        /// <summary>
        /// Used for collision detection
        /// </summary>
        public BoundingRectangle RectangleBounds;

        /// <summary>
        /// Used for constant application of speed onto Position property
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        /// Determines where to draw the sprite
        /// </summary>
        private Vector2 Position;

        /// <summary>
        /// Will prevent the user from jumping infinitely
        /// </summary>
        private int jumpingTimer = 1;

        /// <summary>
        /// public constructor
        /// </summary>
        public Protagonist(Vector2 initialPosition)
        {
            if(initialPosition == null)
            Position = initialPosition;
            initialPosition = Position;
            RectangleBounds = new BoundingRectangle(initialPosition, width, height);
        }

        /// <summary>
        /// The Update method to move, jump, ...etc 
        /// the protagonist sprite
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="movement">enum which direction we need to move</param>
        /// <param name="moving">to help animate the character sprite</param>
        /// <param name="flipTexture">to help animate the character sprite</param>
        /// <param name="jumping">determines if we are actively holding the up key</param>
        /// <param name="attacking">determines if we are pressing the attack key</param>
        /// <param name="pressingShift">determines if we are pressing the shift key</param>
        public void Update(GameTime gameTime, Movement direction, bool moving, bool flipTexture, bool jumping, bool attacking, bool pressingShift )
        {
            //Update state of protagonist
            Direction = direction;
            Moving = moving;
            Flipped = flipTexture;
            Jumping = jumping;
            Attacking = attacking;
            Shift = pressingShift;

            //TODO: Get the protagonist to move correctly
        }

        /// <summary>
        /// The drawing method to display the protagonist
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if (animationTimer > animationSpeed)
            {
                animationTimer -= animationSpeed;
                animationFrame++;
                if (animationFrame > totalFrames)
                {
                    animationFrame = 0;
                }
            }

            if (Flipped)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            
            ///source = rectangle(Which frame to draw (x-coordinate), Which frame to draw (y-coordinate), width of frame, height of frame
            var source = new Rectangle(animationFrame * width, 0, width, height);

            spriteBatch.Draw(texture, Position, source, Color.White, 0f, new Vector2(150, 150), 1, spriteEffect, 0);
        }

        /// <summary>
        /// Resets the state of the protagonist
        /// </summary>
        public void ResetGame()
        {
            Position = initialPosition;
            RectangleBounds = new BoundingRectangle(initialPosition, width, height);
            Jumping = false;
            Moving = false;
            Shift = false;
            SpeedMultiplier = 1;
            Direction = Movement.Idle;
        }

        /// <summary>
        /// Load the texture sprite for the animation
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("TODO");
        }
    }
}
