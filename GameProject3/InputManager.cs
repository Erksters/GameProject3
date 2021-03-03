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
        /// Enum to handle what the protagonist should do
        /// </summary>
        public ProtagonistState Status = ProtagonistState.Idle;

        /// <summary>
        /// Which way is the player facing
        /// When holding the Left Key, it is true.
        /// When holding the Right Key, it is false.
        /// 
        /// </summary>
        public bool Flipped;

        /// <summary>
        /// Determines if we're jumping
        /// </summary>
        public JumpState JumpStatus;

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
            Status = ProtagonistState.Idle;
            Attacking = false;
            Shift = false;

            //Check inputs
            if (PressingLeft() && !PressingRight())
            {
                Flipped = true;
                Status = ProtagonistState.Walking;
            }

            if (PressingRight() && !PressingLeft())
            {
                Flipped = false;
                Status = ProtagonistState.Walking;
            }

            if (PressingUp())
            {
                JumpStatus = JumpState.Jumping;
            }

            if(PressingDown())
            {
                JumpStatus = JumpState.Falling;
            }

            //Attacking
            if (PressingSpace())
            {
                Status = ProtagonistState.Attacking;
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
            if (currentKeyboardState.IsKeyDown(Keys.Space))
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
