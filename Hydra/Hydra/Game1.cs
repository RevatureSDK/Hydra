using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Hydra
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D background;
        private Player player;
        private Tile tile;
        private List<Tile> tiles = new List<Tile>();
        private Tile floor;
        private List<Object2D> objects = new List<Object2D>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
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

            //background = Content.Load<Texture2D>("images/bulkhead-wallsx3"); // change these names to the names of your images
            background = Content.Load<Texture2D>("images/stars"); // change these names to the names of your images
            Texture2D texturePlayerLeft = Content.Load<Texture2D>("images/HydraLeft_v1.2");
            Texture2D texturePlayerRight = Content.Load<Texture2D>("images/HydraRight_v1.2");
            Texture2D textureTile = Content.Load<Texture2D>("images/blockA1");
            Texture2D textureFloor = Content.Load<Texture2D>("images/sprite_floor0.11");
            player = new Player(texturePlayerLeft, texturePlayerRight, 2, 1);
            tiles.Add(new Tile(textureTile, 600, 400));
            tiles.Add(new Tile(textureTile, 600, 200));
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
            // TODO: Add your update logic here
            //inputHelper.Update();
            player.Update(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, objects);

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

            spriteBatch.Draw(background, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White);
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