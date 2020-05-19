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
        private int lives = 3;

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

            if (!level.player.alive)
            {
                lives--;
                if (lives <= 0)
                {
                    //game over
                    lives = 3;
                    lvlState = 0;
                    LoadNextLevel();
                }
                else
                {
                    lvlState--;
                    LoadNextLevel();
                }                
            }
            else if (level.player.reachedExit)
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
            DrawHud();
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void DrawHud()
        {
            SpriteFont hudFont = Content.Load<SpriteFont>("hud/hud");
            Rectangle titleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;
            Vector2 hudLocation = new Vector2(titleSafeArea.X + 1000, titleSafeArea.Y + 15);

            Texture2D textureHead = Content.Load<Texture2D>("hud/HydraHead");
            spriteBatch.Draw(textureHead, new Vector2(950, 15), Color.White);
            DrawShadowedString(hudFont, "x" + lives, hudLocation, Color.Black);
        }

        private void DrawShadowedString(SpriteFont font, string value, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.LightGray);
            spriteBatch.DrawString(font, value, position, color);
        }
    }
}