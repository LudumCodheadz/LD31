using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Input
{
    public class InputManager:Components.ComponentBase
    {

        private bool somethingPressed = false;
        private bool somethingClicked = false;

        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;
        private MouseState previousMouseState;
        private MouseState currentMouseState;

        public InputManager(LDGame game):base(game)
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();
            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            //UpdateMouse();
            UpdateKeyboard();
        }

        private void UpdateKeyboard()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if(currentKeyboardState.GetPressedKeys().Count()> 0 && !somethingPressed)
            {
                Messages.Messenger.Default.Publish(new Messages.InputChangeStateMessage(this, new InputState() { PressedKeys = currentKeyboardState.GetPressedKeys() }));
                somethingPressed = true;
                System.Diagnostics.Debug.WriteLine("Somethking pressed");
            }
            else if (somethingPressed && currentKeyboardState.GetPressedKeys().Count() == 0)
            {
                Messages.Messenger.Default.Publish(new Messages.InputChangeStateMessage(this, new InputState() { PressedKeys = currentKeyboardState.GetPressedKeys() }));
                somethingPressed = false;
            }
        }

        private void UpdateMouse()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if(currentMouseState.Position.X < 0 
                || currentMouseState.Position.Y < 0 
                || currentMouseState.Position.X > this.GraphicsDevice.PresentationParameters.BackBufferWidth 
                || currentMouseState.Position.Y > this.GraphicsDevice.PresentationParameters.BackBufferHeight) 
            {
                return;
            }

            if (previousMouseState.LeftButton == ButtonState.Released &&
                currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Messages.Messenger.Default.Publish(new Messages.InputChangeStateMessage(this, new InputState() { LeftMouseClicked = true, Position = currentMouseState.Position }));
            }
            
            if (previousMouseState.RightButton == ButtonState.Released &&
                currentMouseState.RightButton == ButtonState.Pressed)
            {
                Messages.Messenger.Default.Publish(new Messages.InputChangeStateMessage(this, new InputState() { RightMouseClicked = true, Position = currentMouseState.Position }));
            }

           if ((previousMouseState.LeftButton == ButtonState.Pressed &&
                currentMouseState.LeftButton == ButtonState.Released) &&
               (previousMouseState.RightButton == ButtonState.Pressed &&
                currentMouseState.RightButton == ButtonState.Released))
            {
                Messages.Messenger.Default.Publish(new Messages.InputChangeStateMessage(this, new InputState() { RightMouseClicked = false, Position = currentMouseState.Position }));
            }
        }

        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }
    }
}
