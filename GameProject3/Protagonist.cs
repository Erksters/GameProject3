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
        public Game game;

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
        private int totalFrames = 11;

        /// <summary>
        /// Helps animate the sprite
        /// Determines how quickly the draw() goes through frames
        /// Smaller is faster
        /// </summary>
        private double animationSpeed = 0.1;

        /// <summary>
        /// height of the animations sprite
        /// </summary>
        private int idleHeight = 32;

        /// <summary>
        /// width of the animations sprite
        /// </summary>
        private int idleWidth = 24;

        private int movingWidth = 22;

        /// <summary>
        /// texture object to display in the Draw()
        /// </summary>
        private Texture2D idleTexture;
        
        /// <summary>
        /// texture object to display in the Draw()
        /// </summary>
        private Texture2D WalkingTexture;

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
        public Vector2 HorizontalVelocity = new Vector2(3, 0);

        /// <summary>
        /// An attribute to be affected by gravitational and jumping forces
        /// </summary>
        private Vector2 Velocity = new Vector2(0, 0);

        /// <summary>
        /// The value of gravity in this environment
        /// </summary>
        public Vector2 Gravity = new Vector2(0, 50);

        /// <summary>
        /// The value of Jumping in this environment
        /// </summary>
        public Vector2 Jump = new Vector2(0, -150);

        /// <summary>
        /// Determines where to draw the sprite
        /// </summary>
        private Vector2 Position;

        /// <summary>
        /// Will prevent the user from jumping infinitely
        /// </summary>
        private double jumpingTimer = 0;

        /// <summary>
        /// Helps determine if the player is on a platform
        /// </summary>
        public bool OnPlatform;

        /// <summary>
        /// public constructor
        /// </summary>
        public Protagonist(Game game, Vector2 initialPosition)
        {
            this.game = game;

            //if(initialPosition == null) { throw new ArgumentException(); }
            Position = initialPosition;
            initialPosition = Position;
            RectangleBounds = new BoundingRectangle(initialPosition, idleWidth, idleHeight);
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
        public void Update(GameTime gameTime, Movement direction, bool moving, bool flipTexture, bool jumping, bool attacking, bool pressingShift, bool onPlatform, bool stopLeftMovement, bool stopRightMovement , bool stopJump, bool stopFall , bool stopMoving)
        {
            #region Load in Updates
            //Update state of protagonist
            Direction = direction;
            Moving = moving;
            Flipped = flipTexture;
            Jumping = jumping;
            Attacking = attacking;
            Shift = pressingShift;
            OnPlatform = onPlatform;
            #endregion

            if (stopMoving)
            {
                return;
            }

            //TODO: Get the protagonist to move correctly
            switch (Direction)
            {
                case Movement.Idle:
                    break;
                case Movement.Left:
                    Position += -HorizontalVelocity * SpeedMultiplier;
                    updateBounds();
                    break;

                case Movement.Right:
                    Position += HorizontalVelocity * SpeedMultiplier;
                    updateBounds();
                    break;

                case Movement.Up:
                    OnPlatform = false;
                    break;
                case Movement.Down:
                    break;
            }
            jumpingTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (Jumping && OnPlatform && !stopJump) 
            {
                jump(gameTime);

                
                //If we need to stop jumping
                if(jumpingTimer > 3) 
                { 
                    Jumping = false;
                    jumpingTimer = 0;
                    Jump = new Vector2(0, -150);
                }
                OnPlatform = false;
            }


            //TODO: Determine if we want to always apply gravity
            //I don't think we should
            if(OnPlatform == false)
            {
                applyGravity(gameTime);
            }

            //We are falling, We landed, so keep the horizontal 
            //Velocity, but reset gravity and jumping forces
            if (stopFall)
            {
                Velocity = new Vector2(Velocity.X, 0);
            }
        }

        /// <summary>
        /// Helper method to place the force of a jump on our protagonists position
        /// </summary>
        /// <param name="gameTime"></param>
        private void jump(GameTime gameTime)
        {
            if (Jump.Y > 0) { return; }
            Position += Jump * (float)gameTime.ElapsedGameTime.TotalSeconds;
            updateBounds();
            //Decrement the Jump power 
            Jump += new Vector2(0, 1);
            

        }

        /// <summary>
        /// Helper method to update the bounds after updating the Position
        /// </summary>
        private void updateBounds()
        {
            RectangleBounds.X += Position.X;
            RectangleBounds.Y += Position.Y;
        }

        /// <summary>
        /// The drawing method to display the protagonist
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            checkTextures();

            //Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if (animationTimer > animationSpeed)
            {
                animationTimer -= animationSpeed;
                animationFrame++;
                if (animationFrame > 12)
                {
                    animationFrame = 0;
                }
                if (!Moving && animationFrame > 10)
                {
                    animationFrame = 0;
                }
            }

            handleSpriteEffect(Flipped);

            //source = rectangle(Which frame to draw (x-coordinate), Which frame to draw (y-coordinate), width of frame, height of frame
            var source = new Rectangle(animationFrame * ((Moving) ? movingWidth : idleWidth), 0, ((Moving) ? movingWidth : idleWidth), idleHeight);

            spriteBatch.Draw((Moving ? WalkingTexture : idleTexture), Position, source, Color.White, 0f, new Vector2(idleWidth, idleHeight), 1, spriteEffect, 0);
        }

        /// <summary>
        /// Resets the state of the protagonist
        /// </summary>
        public void ResetGame()
        {
            Position = initialPosition;
            RectangleBounds = new BoundingRectangle(initialPosition, idleWidth, idleHeight);
            Jumping = false;
            Moving = false;
            Shift = false;
            SpeedMultiplier = 1;
            Direction = Movement.Idle;
        }

        /// <summary>
        /// Helper function for applying gravity onto our position attribute
        /// Used in Update()
        /// </summary>
        /// <param name="gameTime"></param>
        private void applyGravity(GameTime gameTime)
        {
            Velocity += Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            updateBounds();
        }

        /// <summary>
        /// Load the texture sprite for the animation
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            idleTexture = content.Load<Texture2D>("SkeletonIdle");
            WalkingTexture = content.Load<Texture2D>("Skeleton Walk");
        }

        /// <summary>
        /// Helper function to determine if we need to flip the direction of the animation
        /// Used in Draw()
        /// </summary>
        /// <param name="flipped"></param>
        private void handleSpriteEffect(bool flipped)
        {
            if (Flipped)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffect = SpriteEffects.None;
            }
        }

        /// <summary>
        /// Helper function to determine if the texture objects loaded correctly
        /// Used in LoadContent()
        /// </summary>
        private void checkTextures()
        {
            if (idleTexture is null) throw new InvalidOperationException("Texture must be loaded to render");
            if (WalkingTexture is null) throw new InvalidOperationException("Texture must be loaded to render");
        }
    }
}
