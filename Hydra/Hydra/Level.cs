using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        Vector2 playerPos;
        Vector2 cerberusPos;
        Vector2 fireballPos;
        public bool jumpPower = false;

        // Key locations in the level.    
        private Vector2 start;

        // Level content.        
        public ContentManager Content;
        public Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();

        public Level(IServiceProvider serviceProvider, int lvlState, Dictionary<string, SoundEffect> se)
        {
            // Create a new content manager to load content used just by this level.
            Content = new ContentManager(serviceProvider, "Content");
            this.soundEffects = se;

            if (lvlState >= 4)
            {
                jumpPower = true;
            }

            LoadLevel(lvlState);

            if (lvlState == 2)
            {
                player.Speed *= -1;
            }
        }

        public void LoadLevel(int lvl)
        {
            LoadBackground();
            string levelPath = string.Format("Content/levels/{0}.txt", lvl);
            Stream fileStream = TitleContainer.OpenStream(levelPath);
            LoadTiles(fileStream);
            LoadPlayer();
            LoadCerberus();
            LoadFireball();
        }

        public void LoadBackground()
        {
            mBackground = new Background();
            mBackground.Scale = 2.0f;
            mBackground.LoadContent(this.Content, "background/gamebackgroundfull");
            mBackground.Position = new Vector2(0, 0);
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

            var Width = lines[0].Length;
            var Height = lines.Count;

            objects = new List<Object2D>();
            for (int y = 0; y < Height; y++)
            {
                for (int x = Width - 1; x >= 0; x--)
                {
                    char tileType = lines[y][x];
                    var tile = LoadTile(tileType, x, y);
                    if (tile != null)
                    {
                        objects.Add(tile);
                    }
                }
            }
        }

        private Tile LoadTile(char tileType, int x, int y)
        {
            x *= 50;
            y *= 50;
            Texture2D textureTile;
            switch (tileType)
            {
                case ' ':
                    return null;

                case '.':
                    return null;

                case 'X':
                    textureTile = Content.Load<Texture2D>("object/flag");
                    textureTile.Name = "Flag";
                    return new Tile(textureTile, x, y);

                case 'J':
                    textureTile = Content.Load<Texture2D>("object/star");
                    textureTile.Name = "Jump";
                    return new Tile(textureTile, x, y);

                case '@':
                    textureTile = Content.Load<Texture2D>("tile/small_p1");
                    return new Tile(textureTile, x, y, 0, 5, textureTile.Width, textureTile.Height - 20);

                case '-':
                    textureTile = Content.Load<Texture2D>("tile/tinyplatform");
                    return new Tile(textureTile, x, y, 0, 5, textureTile.Width, textureTile.Height - 20);

                case '_':
                    textureTile = Content.Load<Texture2D>("tile/medium_p1_tree");
                    return new Tile(textureTile, x, y, 30, 90, textureTile.Width - 30, textureTile.Height - 95);

                case '=':
                    textureTile = Content.Load<Texture2D>("tile/longfloor");
                    return new Tile(textureTile, x, y);

                case '|':
                    textureTile = Content.Load<Texture2D>("tile/longfloormid");
                    return new Tile(textureTile, x, y);

                case '<':
                    textureTile = Content.Load<Texture2D>("tile/longfloorleft");
                    return new Tile(textureTile, x, y);

                case '>':
                    textureTile = Content.Load<Texture2D>("tile/longfloorright");
                    return new Tile(textureTile, x, y);

                case '{':
                    textureTile = Content.Load<Texture2D>("tile/clifftop");
                    return new Tile(textureTile, x, y, 0, 15, textureTile.Width, textureTile.Height - 15);

                case '}':
                    textureTile = Content.Load<Texture2D>("tile/clifftopr");
                    return new Tile(textureTile, x, y, 0, 15, textureTile.Width, textureTile.Height - 15);

                case '(':
                    textureTile = Content.Load<Texture2D>("tile/cliffmid");
                    return new Tile(textureTile, x, y);

                case ')':
                    textureTile = Content.Load<Texture2D>("tile/cliffmidr");
                    return new Tile(textureTile, x, y);

                case 'C':
                    cerberusPos = new Vector2(x - 10, y + 10);
                    fireballPos = new Vector2(x - 12, y + 35);
                    return null;

                case 'H':
                    playerPos = new Vector2(x, y);
                    return null;

                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }

        public void LoadPlayer()
        {
            Texture2D playerLI = Content.Load<Texture2D>("player/HydraLeftIdle_v1.2");
            Texture2D playerRI = Content.Load<Texture2D>("player/HydraRightIdle_v1.2");
            Texture2D playerLW = Content.Load<Texture2D>("player/HydraLeftWalking_v1");
            Texture2D playerRW = Content.Load<Texture2D>("player/HydraRightWalking_v1");
            player = new Player(playerPos, soundEffects, playerLI, playerRI, playerLW, playerRW, 2, 1);
            player.jumpPower = this.jumpPower;
        }

        public void LoadCerberus()
        {
            Texture2D cerberusTexture = Content.Load<Texture2D>("enemy/cerberusv2.2");
            cerberusTexture.Name = "Cerberus";
            cerberus = new Cerberus(cerberusTexture, cerberusPos, 3, 1);
            objects.Add(cerberus);
        }

        public void LoadFireball()
        {
            Texture2D fireballTexture = Content.Load<Texture2D>("enemy/fireballv2");
            fireballTexture.Name = "Fireball";
            fireball = new Fireball(fireballTexture, fireballPos, 3, 1);
            objects.Add(fireball);
        }

        public void Update(int width, int height, GameTime gameTime)
        {
            if (cerberus != null)
            {
                cerberus.Update(width, height);
            }

            if (fireball != null)
            {
                fireball.Update(width, height);                

                if (fireball.Position.X <= -50)
                {
                    LoadFireball();
                }
            }
            
            if (player.jumpPower)
            {
                int length = objects.Count;
                for (int i = 0; i < length; i++)
                {
                    if (objects[i] != null && objects[i].Texture.Name == "Jump")
                    {
                        objects[i] = null;
                    }
                }
            }

            player.Update(width, height, objects);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            mBackground.Draw(spriteBatch);

            foreach (var obj in objects)
            {
                if (obj != null)
                {
                    obj.Draw(spriteBatch);
                }                    
            }

            player.Draw(spriteBatch);
        }

        public void Dispose()
        {
            Content.Unload();
        }
    }
}
