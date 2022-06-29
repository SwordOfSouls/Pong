using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using SwordOfSouls.Games.Api;

namespace Pong
{
    class Paddle : GameSprite
    {
        public Paddle(SpriteData spriteData, Texture2D rect, float x)
            : base(spriteData, rect, null, new Vector2(30,40), new Vector2(0,0), 1f) 
        {
            position = new Vector2(x, data.game.GraphicsDevice.Viewport.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hitbox.boundingBox.Top >= 0)
            {
                position += new Vector2(0, -10f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && hitbox.boundingBox.Bottom <= data.game.GraphicsDevice.Viewport.Height)
            {
                position += new Vector2(0, 10f);
            }


            base.Update(gameTime);
        }

        public static Texture2D createPaddle(SpriteData sData)
        {
            int width = 60;
            int height = 80;
            Texture2D texture = new Texture2D(sData.game.GraphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < width * height; i++)
            {
                data[i] = Color.White;
            }
            texture.SetData(data);

            return texture;
        }

    }
}
