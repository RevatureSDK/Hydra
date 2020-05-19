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
        public Fireball fireball;
        public Cerberus cerberus;
        int startingX = 100;
        int startingY = 250;

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
            }
            else if (lvlState == 2)
            {
                LoadLevel2();
            }
            else if (lvlState == 3)
            {
                LoadLevel3();
            }
        }

        public void LoadPlayer()
        {
            Texture2D playerLI = Content.Load<Texture2D>("player/HydraLeftIdle_v1.2");
            Texture2D playerRI = Content.Load<Texture2D>("player/HydraRightIdle_v1.2");
            Texture2D playerLW = Content.Load<Texture2D>("player/HydraLeftWalking_v1");
            Texture2D playerRW = Content.Load<Texture2D>("player/HydraRightWalking_v1");
            player = new Player(startingX, startingY, playerLI, playerRI, playerLW, playerRW, 2, 1);
        }

        public void LoadFireball(int x, int y)
        {
            Texture2D fireballTexture = Content.Load<Texture2D>("enemy/fireball");
            fireballTexture.Name = "Damage";
            fireball = new Fireball(fireballTexture, x, y, 1, 1);
            objects.Add(fireball);
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
            LoadFireball(410, 480);
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
            LoadPlayer();
        }

        public void LoadLevel2()
        {
            LoadBackground();
            LoadTiles();
            LoadPlayer();
            player.Speed *= -1;
        }

        public void LoadLevel3()
        {
            LoadBackground();
            string levelPath = string.Format("Content/levels/3.txt");
            Stream fileStream = TitleContainer.OpenStream(levelPath);
            LoadTiles(fileStream);
            LoadPlayer();
        }


        public Tile SmallPlatform(int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/small_p1");
            return new Tile(textureTile, x, y, 0, 5, textureTile.Width, textureTile.Height - 20);
        }


        public Tile MediumPlatform(int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/medium_p1_tree");
            return new Tile(textureTile, x, y, 30, 90, textureTile.Width - 30, textureTile.Height - 95);
        }

        public Tile LongPlatform(int x, int y)
        {
            Texture2D textureFloor = Content.Load<Texture2D>("tile/longfloor");
            return new Tile(textureFloor, x, y);
        }

        public Tile LongLeftPlatform(int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/longfloorleft");
            return new Tile(textureTile, x, y);
        }

        public Tile LongMiddlePlatform(int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/longfloormid");
            return new Tile(textureTile, x, y);
        }

        public Tile LongRightPlatform(int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/longfloorright");
            return new Tile(textureTile, x, y);
        }

        public Tile CliffTopLeft(int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/clifftop");
            return new Tile(textureTile, x, y, 0, 15, textureTile.Width, textureTile.Height - 15);
        }

        public Tile CliffTopRight (int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/clifftopr");
            return new Tile(textureTile, x, y, 0, 15, textureTile.Width, textureTile.Height - 15);
        }

        public Tile CliffBottomLeft(int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/cliffmid");
            return new Tile(textureTile, x, y);
        }

        public Tile CliffBottomRight(int x, int y)
        {
            Texture2D textureTile = Content.Load<Texture2D>("tile/cliffmidr");
            return new Tile(textureTile, x, y);
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
            if (fireball != null)
            {
                fireball.Update(width, height);

                if (fireball.Position.X <= -50)
                {
                    LoadFireball(410, 480);
                }
            }
            player.Update(width, height, objects);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            mBackground.Draw(spriteBatch);

            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
            if (fireball != null)
            {
                fireball.Draw(spriteBatch);
            }

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
            var gridTiles = new Tile[width, lines.Count];
            var Width = lines[0].Length;
            var Height = lines.Count;

            objects = new List<Object2D>();
            // Loop over every tile position,
            for (int y = 0; y < Height; y++)
            {
                for (int x = Width-1; x >= 0; x--)
                {
                    char tileType = lines[y][x];
                    var tile = LoadTile(tileType, x, y);
                    if (tile != null)
                    {
                        tiles.Add(tile);
                    }                    
                }
            }

            foreach (var tile in tiles)
            {
                objects.Add(tile);
            }
        }

        private Tile LoadTile(char tileType, int x, int y)
        {
            x *= 50;
            y *= 50;
            switch (tileType)
            {
                case ' ':
                    return null;

                case '.':
                    return null;

                case 'X':
                    return Exit(x, y);

                case '@':
                    return SmallPlatform(x, y);

                case '_':
                    return MediumPlatform(x, y);

                case '=':
                    return LongPlatform(x, y);

                case '|':
                    return LongMiddlePlatform(x, y);

                case '<':
                    return LongLeftPlatform(x, y);

                case '>':
                    return LongRightPlatform(x, y);

                case '{':
                    return CliffTopLeft(x, y);

                case '}':
                    return CliffTopRight(x, y);

                case '(':
                    return CliffBottomLeft(x, y);

                case ')':
                    return CliffBottomRight(x, y);

                case 'C':
                    return Cerberus(x, y);

                case 'H':
                    startingX = x;
                    startingY = y;
                    return null;

                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }

    }
}
