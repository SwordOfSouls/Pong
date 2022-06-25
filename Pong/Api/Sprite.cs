using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong;

namespace SwordOfSouls.Games.Api
{
    public class AnimatedSprite : GameSprite
    {
        protected List<Sprite> frames = new List<Sprite>();
        protected TimeSpan elapsedTime;
        public TimeSpan frameTime = TimeSpan.FromMilliseconds(100);
        public int currentFrame;
        public bool play;


        public AnimatedSprite(SpriteData spriteInfo, string texture, Rectangle? textureLocation, Vector2 origin, Vector2 velocity, float scale)
            : base(spriteInfo, texture, textureLocation, origin, velocity, scale)
        {
            this.velocity = velocity;

            this.frames.Add(this);
        }

        public void AddFrame(Frame frame)
        {
            frames.Add(frame);
        }

        public override void Draw()
        {
            Sprite frame = frames[currentFrame];

            data.batch.Draw(frame.texture, frame.position, frame.textureLocation, frame.tint,
                frame._rotation,
                new Vector2((frame.origin.X - frame.textureLocation.Value.Left),
                (frame.origin.Y - frame.textureLocation.Value.Top)),
                frame.scale,
                frame.effects,
                0f);

            frame.hitbox.DrawHitbox();
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime >= frameTime)
            {
                if (currentFrame >= frames.Count - 1)
                {
                    currentFrame = 0;
                }
                else
                {
                    currentFrame++;
                    Sprite frame = frames[currentFrame];
                    frame.position = this.position;
                    frame.rotation = this.rotation;
                    Frame(gameTime);
                }
                elapsedTime = TimeSpan.Zero;
            }
        }

        public virtual void Frame(GameTime gameTime)
        {
            //this.rotation+=5;
        }

    }

    public class GameSprite : Sprite
    {
        public Vector2 velocity = new Vector2(0, 0);

        public GameSprite(SpriteData spriteData, string texture, Rectangle? textureLocation, Vector2 origin, Vector2 velocity, float scale)
            : base(spriteData,texture, textureLocation, origin, scale)
        {
            this.velocity = velocity;
        }

        public void UpdatePosition()
        {
            this.position = position + velocity;
        }

    }

    public class Frame : Sprite
    {
        public Frame(Sprite sprite, Vector2 origin,  string texture, Rectangle? textureLocation)
            : base(sprite.data,texture, textureLocation, origin, sprite.scale)
        {
            this.scale = sprite.scale;
            this.tint = sprite.tint;
            this.effects = sprite.effects;
            this.debug = sprite.debug;
            this.position = sprite.position;
            this.rotation = sprite.rotation;
        }

        public Frame(Sprite sprite, Vector2 origin, Rectangle? textureLocation)
            : base(sprite.data, sprite.texture.Name, textureLocation, origin, sprite.scale)
        {
            this.scale = sprite.scale;
            this.tint = sprite.tint;
            this.effects = sprite.effects;
            this.debug = sprite.debug;
            this.position = sprite.position;
            this.rotation = sprite.rotation;
        }
    }

    public class Sprite
    {
        public readonly SpriteData data;

        public readonly Texture2D texture;
        public readonly Rectangle? textureLocation;
        public readonly Vector2 origin;
        public Hitbox hitbox;

        public Vector2 position = new Vector2(0, 0);
        public Color tint = Color.White;
        public SpriteEffects effects = SpriteEffects.None;

        public bool debug = false;
        public float scale;
        public float _rotation = 0;
        public float rotation
        {
            set
            {
                _rotation = (float) (value * Math.PI / 180.0);
            }
            get 
            {
                return (float)(_rotation * 180 / Math.PI);
            }
        }

        public Sprite(SpriteData spriteData, string texture, Rectangle? textureLocation, Vector2 origin, float scale)
        {

            this.data = spriteData;
            this.texture = spriteData.game.Content.Load<Texture2D>(texture);
            if(textureLocation.HasValue)
            {
                Rectangle textureLocT = (Rectangle) textureLocation;
                this.textureLocation = new Rectangle(textureLocT.X, textureLocT.Height, textureLocT.Width + 1 - textureLocT.X, textureLocT.Y-textureLocT.Height);
                Debug.WriteLine(textureLocation);
            } else
            {
                this.textureLocation = textureLocation;
            }
            this.origin = origin;
            this.scale = scale;

            this.hitbox = new Hitbox(this);
            Game1.sprites.Add(this);
        }

        public virtual void Draw()
        {
            if(textureLocation.HasValue)
            {
                data.batch.Draw(texture, position, textureLocation, tint, _rotation, new Vector2(origin.X - textureLocation.Value.Left, origin.Y - textureLocation.Value.Top), scale, effects, 1);
            } else
            {
                data.batch.Draw(texture, position, textureLocation, tint, _rotation, origin, scale, effects, 1);
            }


            if (debug)
            {
                hitbox.DrawHitbox();
            }

        }

        public virtual void Update(GameTime gameTime) { }

    }

    public class SpriteData
    {
        public readonly SpriteBatch batch;
        public readonly Game game;

        public SpriteData(SpriteBatch batch, Game game)
        {
            this.batch = batch;
            this.game = game;
        }
    }

}
