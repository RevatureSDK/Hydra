using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Hydra
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HydraGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Level level;
        private int lvlState = 0;

        public HydraGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadNextLevel();
        }

        private void LoadNextLevel()
        {
            lvlState++;
            // Unloads the content for the current level before loading the next one.
            if (level != null)
                level.Dispose();

            // Load the level.
            level = new Level(Services, lvlState);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            level.Update(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, gameTime);
            if (level.reachedExit)
            {
                LoadNextLevel();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            level.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}