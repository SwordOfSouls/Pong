using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using SwordOfSouls.Games.Api;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteData spriteData;

        private Ball ball;
        
        //private AnimatedSprite cat;

        private Rectangle gameBox;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //IsFixedTimeStep = false;
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

            ball = new Ball(spriteData, new Paddle(spriteData, Paddle.createPaddle(spriteData), 0), new Paddle(spriteData, Paddle.createPaddle(spriteData), GraphicsDevice.Viewport.Width));

            //ball.debug = true;

            //ball = new GameSprite(spriteData, "ball", null, new Vector2(75, 75), new Vector2(0, 0), .4f);
            //ball.position = (new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 1.5f));
            //ball.rotation = 45;
            //ball.debug = true;


            //cat = new AnimatedSprite(spriteData, "cat", new Rectangle(7, 41, 36, 20),new Vector2(22,31),new Vector2(0,0),3);
            //cat.position = (new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
            //cat.rotation = 45;

            //cat.AddFrame(new Frame(cat, new Vector2(72, 31), new Rectangle(56, 41, 88, 22)));
            //cat.AddFrame(new Frame(cat, new Vector2(122, 31), new Rectangle(107, 41, 136, 20)));
            //cat.AddFrame(new Frame(cat, new Vector2(172, 31), new Rectangle(156, 41, 196, 6)));

            //cat.frameTime = TimeSpan.FromMilliseconds(300);

            //cat.debug = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Sprite sprite in spriteData.sprites)
            {
                if(sprite is GameSprite)
                {
                    if (!(sprite is Frame))
                        ((GameSprite)sprite).UpdatePosition();
                }
                sprite.Update(gameTime);

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);

            spriteBatch.Begin();
            foreach (Sprite sprite in spriteData.sprites)
            {
                if (!(sprite is Frame))
                    sprite.Draw();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
