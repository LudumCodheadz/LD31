﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using CodheadzLD31.Components;
using CodheadzLD31.GameStates;
using CodheadzLD31.Input;

namespace CodheadzLD31
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LDGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public LDGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 480;
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            var debug = new DebugComponent(this);
            this.Components.Add(debug);

            var gameStateManager = new GameStateManager(this);
            this.Services.AddService(gameStateManager);
            this.Components.Add(gameStateManager);

            var inputManager = new InputManager(this);
            this.Components.Add(inputManager);
            this.Services.AddService(inputManager);

            //var inputTest = new InputTestComponent(this);
            //this.Components.Add(inputTest);

            var mainMenu = new MainMenuComponent(this);
            this.Components.Add(mainMenu);

            var level = new LevelComponent(this);
            this.Components.Add(level);
            this.Services.AddService(level);

            var levelManager = new LevelManagerComponent(this);
            this.Components.Add(levelManager);
            this.Services.AddService(levelManager);

            var player = new PlayerComponent(this);
            this.Components.Add(player);

            var levelEnd = new LevelOverSummaryComponent(this);
            this.Components.Add(levelEnd);

            var env = new EnvironmentComponent(this);
            this.Components.Add(env);
            this.Services.AddService(env);

            var playerHud = new PlayerHudComponent(this);
            this.Components.Add(playerHud);
            this.Services.AddService(playerHud);

            var gameOver = new GameOverComponent(this);
            this.Components.Add(gameOver);

            base.Initialize();

            Messages.Messenger.Default.Publish(new Messages.GameStateChangeMessage(this, GameStates.GameStates.MainMenu));
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
