using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using SwordOfSouls.Games.Api;

namespace Pong
{
    class Ball : GameSprite
    {
        Paddle padd1;
        Paddle padd2;
        Random rand = new Random();

        public Ball(SpriteData spriteData, Paddle paddle1, Paddle paddle2)
            : base(spriteData, "ball", null, new Vector2(75,75), new Vector2(7,7), 0.3f) 
        {
            position = new Vector2(data.game.GraphicsDevice.Viewport.Width / 2, data.game.GraphicsDevice.Viewport.Height / 2);
            this.padd1 = paddle1;
            this.padd2 = paddle2;

            tint = Color.Cyan;
        }

        public override void Update(GameTime gameTime)
        {
            GraphicsDevice GraphicsDevice = data.game.GraphicsDevice;
            hitbox.UpdateHitbox();
            if (hitbox.IsColliding(padd1) || hitbox.IsColliding(padd2))
            {
                tint = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
                velocity = new Vector2(velocity.X * -1, velocity.Y * -1);
            } else if (hitbox.boundingBox.Right >= GraphicsDevice.Viewport.Width || hitbox.boundingBox.Left <= 0)
            {
                velocity = Vector2.Zero;
                this.draw = false;
                this.padd1.draw = false;
                this.padd2.draw = false;
            }

            if (hitbox.boundingBox.Bottom >= GraphicsDevice.Viewport.Height || hitbox.boundingBox.Top <= 0)
            {
                velocity = new Vector2(velocity.X, velocity.Y * -1);
                padd1.tint = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
                padd2.tint = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            if(!draw)
            {
                data.game.GraphicsDevice.Clear(Color.DarkRed);
            }
            base.Draw();
        }

    }
}
