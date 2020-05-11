using Microsoft.Xna.Framework;
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

        private Texture2D background;
        private Player player;
        private List<Tile> tiles = new List<Tile>();
        private Tile floor;
        private List<Object2D> objects = new List<Object2D>();

        private Background mBackgroundOne;

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
            mBackgroundOne = new Background();
            mBackgroundOne.Scale = 2.0f;

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
            mBackgroundOne.LoadContent(this.Content, "background/gamebackgroundfull");
            mBackgroundOne.Position = new Vector2(0, 0);
            //mBackgroundTwo.LoadContent(this.Content, "background/gamebackgroundright");
            //mBackgroundTwo.Position = new Vector2(mBackgroundOne.Position.X + mBackgroundOne.Size.Width, 0);

            //background = Content.Load<Texture2D>("images/bulkhead-wallsx3"); // change these names to the names of your images
            background = Content.Load<Texture2D>("background/stars"); // change these names to the names of your images

            Texture2D playerLI = Content.Load<Texture2D>("player/HydraLeftIdle_v1.2");
            Texture2D playerRI = Content.Load<Texture2D>("player/HydraRightIdle_v1.2");
            Texture2D playerLW = Content.Load<Texture2D>("player/HydraLeftWalking_v1");
            Texture2D playerRW = Content.Load<Texture2D>("player/HydraRightWalking_v1");

            Texture2D textureTile = Content.Load<Texture2D>("tile/blockA1");
            Texture2D textureFloor = Content.Load<Texture2D>("tile/sprite_floor0.11");
            player = new Player(playerLI, playerRI, playerLW, playerRW, 2, 1);
            tiles.Add(new Tile(textureTile, 600, 400));
            tiles.Add(new Tile(textureTile, 500, 250));
            floor = new Tile(textureFloor, 0, 540);
            foreach(var tile in tiles)
            {
                objects.Add(tile);
            }
            //objects.Add(tile);
            objects.Add(floor);
            //shuttle = Content.Load<Texture2D>("images/shuttle");  // if you are using your own images.
            //earth = Content.Load<Texture2D>("images/earth");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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

            Console.WriteLine(mBackgroundOne.Position.X);
            player.Update(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, objects);

            //if (player.Position.X == graphics.PreferredBackBufferWidth / 2)
            //{
            //    mBackgroundOne.Position.X += speed;
            //}

            //currentFrame++;
            //if (currentFrame >= fps) 
            //{
            //    currentFrame = 0;
            //}                                    
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
            mBackgroundOne.Draw(this.spriteBatch);
            //mBackgroundTwo.Draw(this.spriteBatch);
            spriteBatch.End();
            
            foreach(var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
            //tile.Draw(spriteBatch);
            floor.Draw(spriteBatch);
            player.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}