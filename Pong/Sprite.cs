using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Sprite
    {
        readonly SpriteBatch batch;

        readonly Texture2D texture;
        readonly Vector2 origin;

        Vector2 position = new Vector2(0,0);
        Color tint = Color.White;
        SpriteEffects effects = SpriteEffects.None;

        int rotation = 0;
        float scale;

        public Sprite(Game game,SpriteBatch batch, string texture, Vector2 origin, float scale)
        {
            this.batch = batch;
            this.texture = game.Content.Load<Texture2D>(texture);
            this.origin = origin;
            this.scale = scale;
        }

        public void Draw()
        {
            batch.Draw(texture, position, null, tint, rotation, origin,scale, effects, 0);
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public void SetTint(Color tint)
        {
            this.tint = tint;
        }

        public void setEffects(SpriteEffects effects)
        {
            this.effects = effects;
        }

        public void setRotation(int rotation)
        {
            this.rotation = rotation;
        }

        public void setScale(float scale)
        {
            this.scale = scale;
        }
    }
}
