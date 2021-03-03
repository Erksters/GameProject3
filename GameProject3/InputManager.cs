using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Collisions.Collisions;

namespace GameProject3
{
    public class InputManager
    {
        /// <summary>
        /// Determines if the user presses ESC
        /// Much cleaner than it's counterparts
        /// </summary>
        public bool Exit {get; private set;}

        private KeyboardState currentKeyboardState;
        private KeyboardState priorKeyboardState;

        /// <summary>
        /// Vector2 Direction for sprite velocity
        /// </summary>
        public Movement Direction = Movement.Idle;

        /// <summary>
        /// Which way is the player facing
        /// When holding the Left Key, it is true.
        /// When holding the Right Key, it is false.
        /// 
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
        /// Public Constructor
        /// </summary>
        public InputManager()
        {

        }

        /// <summary>
        /// Updates the keyboard state of the game
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //base state of Input
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            Direction = Movement.Idle;
            Moving = false;
            Attacking = false;
            Shift = false;

            //Check inputs
            if (PressingLeft())
            {
                Flipped = true;
                Moving = true;
                Direction = Movement.Left;
            }

            if (PressingRight())
            {
                Flipped = false;
                Moving = true;
                Direction = Movement.Right;
            }

            if (PressingUp())
            {
                Direction = Movement.Up;
                Jumping = true;
            }

            if (StoppedUp())
            {
                Jumping = false;
            }

            if(PressingDown())
            {
                Direction = Movement.Down;
            }

            //Attacking
            if (PressingSpace())
            {
                Attacking = true;
            }

            //Holding Shift
            if (PressingShift())
            {
                Shift = true;
            }

            //Exit game
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit = true;
            }
        }

        /// <summary>
        /// Helper function to clean up code in update()
        /// </summary>
        /// <returns></returns>
        private bool PressingLeft()
        {
            if (currentKeyboardState.IsKeyDown(Keys.Left) 
                   || currentKeyboardState.IsKeyDown(Keys.A) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Helper function to clean up code in update()
        /// </summary>
        /// <returns></returns>
        private bool PressingRight()
        {
            if (currentKeyboardState.IsKeyDown(Keys.Right) 
                   || currentKeyboardState.IsKeyDown(Keys.D))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Helper function to clean up code in update()
        /// </summary>
        /// <returns></returns>
        private bool PressingUp() 
        {
            if (currentKeyboardState.IsKeyDown(Keys.Up) && priorKeyboardState.IsKeyUp(Keys.Up)
                   || currentKeyboardState.IsKeyDown(Keys.W) && priorKeyboardState.IsKeyUp(Keys.W))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private bool StoppedUp()
        {
            if (priorKeyboardState.IsKeyDown(Keys.Up) && currentKeyboardState.IsKeyUp(Keys.Up)
                  || priorKeyboardState.IsKeyDown(Keys.W) && currentKeyboardState.IsKeyUp(Keys.W))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// Helper function to clean up code in update()
        /// </summary>
        /// <returns></returns>
        private bool PressingDown()
        {
            if (currentKeyboardState.IsKeyDown(Keys.Down) && priorKeyboardState.IsKeyUp(Keys.Down)
                   || currentKeyboardState.IsKeyDown(Keys.S) && priorKeyboardState.IsKeyUp(Keys.S))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Helper function to clean up code in update()
        /// </summary>
        /// <returns></returns>
        private bool PressingSpace()
        {
            if (currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space))
            {
                return true; 
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Helper function to clean up code in update()
        /// </summary>
        /// <returns></returns>
        private bool PressingShift()
        {
            if (currentKeyboardState.IsKeyDown(Keys.LeftShift) && priorKeyboardState.IsKeyUp(Keys.LeftShift))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
