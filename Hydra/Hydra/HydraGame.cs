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

        private Player player;
        private List<Tile> tiles = new List<Tile>();
        private List<Object2D> objects = new List<Object2D>();
        private Background mBackground = new Background();
        private int lvlState = 1;

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

            //mBackgroundTwo = new Background();
            //mBackgroundTwo.Scale = 1.0f;

            // TODO: Add your initialization logic here
            //inputHelper = new InputHelper();
            //player = new Player(inputHelper);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Texture2D playerLI = Content.Load<Texture2D>("player/HydraLeftIdle_v1.2");
            Texture2D playerRI = Content.Load<Texture2D>("player/HydraRightIdle_v1.2");
            Texture2D playerLW = Content.Load<Texture2D>("player/HydraLeftWalking_v1");
            Texture2D playerRW = Content.Load<Texture2D>("player/HydraRightWalking_v1");
            player = new Player(playerLI, playerRI, playerLW, playerRW, 2, 1);
            LoadBackground();
            LoadContent(lvlState);
        }

        protected void LoadContent(int state)
        {
            if (state == 1)
            {
                LoadLevel1();
            }
            else if (state == 2)
            {
                LoadLevel2();
            }           

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            int previous = player.currentLvl;
            player.Update(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, objects);
            lvlState = player.currentLvl;
            if (player.currentLvl != previous)
            {
                UnloadContent();
                LoadContent();
                //LoadContent(player.currentLvl);
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            mBackground.Draw(this.spriteBatch);
            //mBackgroundTwo.Draw(this.spriteBatch);
            spriteBatch.End();

            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);

            base.Draw(gameTime);
        }


        public void LoadLevel1()
        {
            Texture2D textureTile1 = Content.Load<Texture2D>("tile/small_p1");
            Texture2D textureTile2 = Content.Load<Texture2D>("tile/medium_p1_tree");
            Texture2D textureFloor = Content.Load<Texture2D>("tile/floor");
            Texture2D textureFlag= Content.Load<Texture2D>("object/flag");
            textureFlag.Name = "Flag";

            tiles.Add(new Tile(textureTile1, 300, 400, 0, 5, textureTile1.Width, textureTile1.Height - 20));
            tiles.Add(new Tile(textureTile2, 500, 270, 30, 90, textureTile2.Width -30, textureTile2.Height - 95));
            tiles.Add(new Tile(textureFlag, 700, 300));
            tiles.Add(new Tile(textureFloor, 0, 540));

            objects = new List<Object2D>();
            foreach (var tile in tiles)
            {
                objects.Add(tile);
            }
        }

        public void LoadLevel2()
        {
            Texture2D textureTile1 = Content.Load<Texture2D>("tile/small_p1");
            Texture2D textureTile2 = Content.Load<Texture2D>("tile/medium_p1_tree");
            Texture2D textureFloor = Content.Load<Texture2D>("tile/floor");

            tiles.Add(new Tile(textureTile1, 500, 400, 0, 5, textureTile1.Width, textureTile1.Height - 20));
            tiles.Add(new Tile(textureTile2, 300, 270, 30, 90, textureTile2.Width - 30, textureTile2.Height - 95));
            tiles.Add(new Tile(textureFloor, 0, 540));

            objects = new List<Object2D>();
            foreach (var tile in tiles)
            {
                objects.Add(tile);
            }
        }

        public void LoadBackground()
        {
            mBackground = new Background();
            mBackground.Scale = 2.0f;
            mBackground.LoadContent(this.Content, "background/gamebackgroundfull");
            mBackground.Position = new Vector2(0, 0);
        }
    }
}