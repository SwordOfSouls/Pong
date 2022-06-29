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

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                ball.Reset();

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
