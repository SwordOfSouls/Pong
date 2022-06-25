using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using SwordOfSouls.Games.Api;

namespace Pong
{
    public class Game1 : Game
    {
        public static List<Sprite> sprites = new List<Sprite>();
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteData spriteData;

        private GameSprite ball;
        private AnimatedSprite cat;

        private Rectangle gameBox;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameBox = new Rectangle(0, GraphicsDevice.Viewport.Height, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteData = new SpriteData(spriteBatch, this);

            //ball = new GameSprite(spriteData, "ball", null, new Vector2(75, 75), new Vector2(0, 0), .4f);
            //ball.position = (new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 1.5f));
            //ball.rotation = 45;
            //ball.debug = true;


            cat = new AnimatedSprite(spriteData, "cat", new Rectangle(7, 41, 36, 20),new Vector2(22,31),new Vector2(0,0),3);
            cat.position = (new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
            cat.rotation = 45;

            cat.AddFrame(new Frame(cat, new Vector2(72, 31), new Rectangle(56, 41, 88, 22)));
            cat.AddFrame(new Frame(cat, new Vector2(122, 31), new Rectangle(107, 41, 136, 20)));
            cat.AddFrame(new Frame(cat, new Vector2(172, 31), new Rectangle(156, 41, 196, 6)));

            cat.frameTime = TimeSpan.FromMilliseconds(3000);

            cat.debug = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Sprite sprite in sprites)
            {
                sprite.Update(gameTime);

                if(sprite is GameSprite)
                {
                    if (!(sprite is Frame))
                        ((GameSprite)sprite).UpdatePosition();
                }
            }




            if (cat.position.X + (cat.origin.X / 2) >= GraphicsDevice.Viewport.Width || cat.position.X - (cat.origin.X / 2) <= 0)
            {
                cat.velocity = (new Vector2(cat.velocity.X * -1, cat.velocity.Y));
            }

            if (cat.position.Y + (cat.origin.Y / 2) >= GraphicsDevice.Viewport.Height || cat.position.Y - (cat.origin.Y / 2) <= 0)
            {
                cat.velocity = (new Vector2(cat.velocity.X, cat.velocity.Y * -1));
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);

            spriteBatch.Begin();
            foreach (Sprite sprite in sprites)
            {
                if (!(sprite is Frame))
                    sprite.Draw();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
