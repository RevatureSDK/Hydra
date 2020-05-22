using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
// using Microsoft.Xna.Framework.Audio;
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
        private Song gameMusic;
        private Song gameOverMusic;

        private Level level;
        private int lvlState = 0;
        private int lives = 3;
        private bool gameOver = false;
        private InputHelper inputHelper;


        public Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();

        public HydraGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 900;
            inputHelper = new InputHelper();
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
            gameMusic = Content.Load<Song>("audio/breakdown3");
            gameOverMusic = Content.Load<Song>("audio/gameOver");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(gameMusic);
            SoundEffect.MasterVolume = 0.4f;
            soundEffects.Add("fireball", Content.Load<SoundEffect>("audio/fireHit"));
            soundEffects.Add("jump", Content.Load<SoundEffect>("audio/jump"));
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
            level = new Level(Services, lvlState, soundEffects);
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
            inputHelper.Update();
            if (!gameOver)
            {
                level.Update(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, gameTime);
                if (!level.player.alive)
                {
                    lives--;
                    if (lives <= 0)
                    {
                        lvlState = 0;
                        lives = 3;
                        gameOver = true;
                        MediaPlayer.Play(gameOverMusic);
                        GraphicsDevice.Clear(Color.Black);
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
            }
            else
            {
                if (inputHelper.IsNewKeyPress(Keys.Space))
                {
                    MediaPlayer.Play(gameMusic);
                    gameOver = false;
                    LoadNextLevel();
                }
            }

            base.Update(gameTime);
        }

        public void DrawGameOver()
        {
            SpriteFont hudFont = Content.Load<SpriteFont>("hud/hud");
            var center = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            DrawShadowedString(hudFont, "Game Over", center, Color.White);
            center.Y += 100;
            DrawShadowedString(hudFont, "Press Spacebar to continue", center, Color.White);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (!gameOver)
            {
                level.Draw(gameTime, spriteBatch);
                DrawHud();
            }
            else
            {
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(Color.Black);
                DrawGameOver();
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void DrawHud()
        {
            SpriteFont hudFont = Content.Load<SpriteFont>("hud/hud");
            Rectangle titleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;
            Vector2 hudLocation = new Vector2(1000, 15);

            Texture2D textureHead = Content.Load<Texture2D>("hud/HydraHead");
            spriteBatch.Draw(textureHead, new Vector2(950, 15), Color.White);
            DrawShadowedString(hudFont, "x" + lives, hudLocation, Color.Black);
            DrawShadowedString(hudFont, "Level " + lvlState, new Vector2(100, 15), Color.Black);
        }

        private void DrawShadowedString(SpriteFont font, string value, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.LightGray);
            spriteBatch.DrawString(font, value, position, color);
        }
    }
}