using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra
{
    class Level : IDisposable
    {
        // Physical structure of the level.
        private List<Tile> tiles = new List<Tile>();
        private List<Object2D> objects;
        private Background mBackground = new Background();
        private Player player;

        // Key locations in the level.        
        private Vector2 start;
        public bool reachedExit = false;

        // Level content.        
        public ContentManager Content;

        public Level(IServiceProvider serviceProvider, int lvlState)
        {
            // Create a new content manager to load content used just by this level.
            Content = new ContentManager(serviceProvider, "Content");
            LoadBackground();
            LoadPlayer();

            if (lvlState == 1)
            {
                LoadLevel1();
            } else if (lvlState == 2)
            {
                LoadLevel2();
            }
        }

        public void LoadPlayer()
        {
            Texture2D playerLI = Content.Load<Texture2D>("player/HydraLeftIdle_v1.2");
            Texture2D playerRI = Content.Load<Texture2D>("player/HydraRightIdle_v1.2");
            Texture2D playerLW = Content.Load<Texture2D>("player/HydraLeftWalking_v1");
            Texture2D playerRW = Content.Load<Texture2D>("player/HydraRightWalking_v1");
            player = new Player(playerLI, playerRI, playerLW, playerRW, 2, 1);
        }

        public void LoadBackground()
        {
            mBackground = new Background();
            mBackground.Scale = 2.0f;
            mBackground.LoadContent(this.Content, "background/gamebackgroundfull");
            mBackground.Position = new Vector2(0, 0);
        }

        public void LoadLevel1()
        {
            Texture2D textureTile1 = Content.Load<Texture2D>("tile/small_p1");
            Texture2D textureTile2 = Content.Load<Texture2D>("tile/medium_p1_tree");
            Texture2D textureFloor = Content.Load<Texture2D>("tile/floor");
            Texture2D textureFlag = Content.Load<Texture2D>("object/flag");
            textureFlag.Name = "Flag";

            tiles.Add(new Tile(textureTile1, 300, 400, 0, 5, textureTile1.Width, textureTile1.Height - 20));
            tiles.Add(new Tile(textureTile2, 500, 270, 30, 90, textureTile2.Width - 30, textureTile2.Height - 95));
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

        public void Dispose()
        {
            Content.Unload();
        }

        public void Update(int width, int height, GameTime gameTime)
        {
            player.Update(width, height, objects);
            if (player.reachedExit)
            {
                this.reachedExit = true;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            mBackground.Draw(spriteBatch);

            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
        }

    }
}
