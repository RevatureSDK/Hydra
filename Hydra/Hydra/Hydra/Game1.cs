using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Hydra;

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
        //private Texture2D shuttle;
        //private Texture2D earth;
        private AnimatedSprite animatedSprite;
        private InputHelper inputHelper;
        private Vector2 playerPosition;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 800;
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            inputHelper = new InputHelper();

            playerPosition = new Vector2(400, 200);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("images/stars"); // change these names to the names of your images
            Texture2D texture = Content.Load<Texture2D>("images/sprite_hydra0");
            animatedSprite = new AnimatedSprite(texture, 1, 1);
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
            inputHelper.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.IsKeyDown(Keys.D))
            {
                if(playerPosition.X <= this.Window.ClientBounds.Width)
                {
                    playerPosition.X += 2;
                }
                
            }

            if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.IsKeyDown(Keys.A))
            {
                if(playerPosition.X >= 0)
                {
                    playerPosition.X -= 2;
                }
                
            }

            if (inputHelper.IsKeyDown(Keys.Down) || inputHelper.IsKeyDown(Keys.S))
            {
                if (playerPosition.Y <= this.Window.ClientBounds.Height)
                {
                    playerPosition.Y += 2;
                }
            }

            if (inputHelper.IsKeyDown(Keys.Up) || inputHelper.IsKeyDown(Keys.W))
            {
                if(playerPosition.Y >= 0)
                {
                    playerPosition.Y -= 2;
                }
            }


            // TODO: Add your update logic here
            animatedSprite.Update();

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
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White);
            //spriteBatch.Draw(earth, new Vector2(400, 240), Color.White);
            //spriteBatch.Draw(shuttle, new Vector2(450, 240), Color.White);
            spriteBatch.End();

            animatedSprite.Draw(spriteBatch, playerPosition);
            
            base.Draw(gameTime);
        }
    }
}