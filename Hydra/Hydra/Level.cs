using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        public Player player;
        public Enemy enemy;

        // Key locations in the level.        
        private Vector2 start;

        // Level content.        
        public ContentManager Content;

        public Level(IServiceProvider serviceProvider, int lvlState)
        {
            // Create a new content manager to load content used just by this level.
            Content = new ContentManager(serviceProvider, "Content");

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
            player = new Player(50, 300, playerLI, playerRI, playerLW, playerRW, 2, 1);
        }

        public void LoadEnemy()
        {

            Texture2D textureFireball = Content.Load<Texture2D>("enemy/fireball");
            textureFireball.Name = "Damage";
            enemy = new Enemy(textureFireball, 450, 0, 1, 1);
            objects.Add(enemy);
        }

        public void LoadBackground()
        {
            mBackground = new Background();
            mBackground.Scale = 2.0f;
            mBackground.LoadContent(this.Content, "background/gamebackgroundfull");
            mBackground.Position = new Vector2(0, 0);
        }

        public void LoadTiles()
        {
            objects = new List<Object2D>();         

            tiles.Add(SmallPlatform(300, 400));
            tiles.Add(MediumPlatform(500, 270));
            tiles.Add(Cerberus(410, 465));
            tiles.Add(Exit(700, 300));
            tiles.Add(LongPlatform(0, 540));

            foreach (var tile in tiles)
            {
                objects.Add(tile);
            }
        }

        public void LoadLevel1()
        {
            LoadBackground();
            LoadTiles();
            LoadEnemy();
            LoadPlayer();
        }

        public void LoadLevel2()
        {
            LoadBackground();
            LoadTiles();
            LoadEnemy();
            LoadPlayer();
            player.Speed *= -1;
        }


        public Tile SmallPlatform(int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/small_p1");
            return new Tile(textureTile, x, y, 0, 5, textureTile.Width, textureTile.Height - 20);
        }


        public Tile MediumPlatform(int x, int y)
        {
            Texture2D textureTile2 = Content.Load<Texture2D>("tile/medium_p1_tree");
            return new Tile(textureTile2, x, y, 30, 90, textureTile2.Width - 30, textureTile2.Height - 95);
        }

        public Tile LongPlatform(int x, int y)
        {
            Texture2D textureFloor = Content.Load<Texture2D>("tile/longfloor");
            return new Tile(textureFloor, x, y);
        }

        public Tile Exit(int x, int y)
        {
            Texture2D textureFlag = Content.Load<Texture2D>("object/flag");
            textureFlag.Name = "Flag";
            return new Tile(textureFlag, x, y);
        }

        public Tile Cerberus(int x, int y)
        {
            Texture2D textureCerberus = Content.Load<Texture2D>("enemy/cerberus");
            textureCerberus.Name = "Damage";
            return new Tile(textureCerberus, x, y);
        }

        public void Dispose()
        {
            Content.Unload();
        }

        public void Update(int width, int height, GameTime gameTime)
        {
            enemy.Update(width, height);
            player.Update(width, height, objects);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            mBackground.Draw(spriteBatch);

            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
            enemy.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }

        private void LoadTiles(Stream fileStream)
        {
            // Load the level and ensure all of the lines are the same length.
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            // Allocate the tile grid.

            // Loop over every tile position,
            for (int y = 0; y < lines.Count; ++y)
            {
                for (int x = 0; x < lines[0].Length; ++x)
                {

                }
            }

        }


    }
}
