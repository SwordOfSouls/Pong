using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteData spriteData;

        private Sprite ball;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteData = new SpriteData(spriteBatch, this);

            ball = new Sprite(spriteData, "ball", new Vector2(75, 75), 0.4f);
            ball.SetVelocity(new Vector2(4,4));
            ball.SetPosition(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            if(ball.GetPosition().X + (ball.GetOrigin().X / 2 ) >= GraphicsDevice.Viewport.Width || ball.GetPosition().X - (ball.GetOrigin().X / 2)  <= 0)
            {
                ball.SetVelocity(new Vector2(ball.GetVelocity().X * -1, ball.GetVelocity().Y));
            }

            if (ball.GetPosition().Y + (ball.GetOrigin().Y /2)  >= GraphicsDevice.Viewport.Height || ball.GetPosition().Y - (ball.GetOrigin().Y / 2)  <= 0)
            {
                ball.SetVelocity(new Vector2(ball.GetVelocity().X, ball.GetVelocity().Y * -1));
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);

            spriteBatch.Begin();
            ball.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
