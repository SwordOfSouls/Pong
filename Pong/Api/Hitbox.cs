using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;



namespace SwordOfSouls.Games.Api
{
    public class Hitbox
    {
        readonly Sprite sprite;
        readonly Rectangle localHitbox;
        readonly Texture2D texture;
        Vector2[] corners = new Vector2[4];


        public Hitbox(Sprite sprite)
        {
            this.sprite = sprite;
            this.localHitbox = GetLocalHitbox();
            //texture = new Texture2D(sprite.data.game.GraphicsDevice, 4, 4);
            //texture.SetData(new[] {Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White});
            texture = new Texture2D(sprite.data.game.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White } );
            //Debug.WriteLine(localHitbox);
        }

        public Rectangle GetHitbox()
        {
            Rectangle hitbox = new Rectangle(0, 0, 0, 0);
            if (sprite.textureLocation.HasValue)
            {
                hitbox = new Rectangle((int)(sprite.position.X - ((sprite.origin.X - sprite.textureLocation.Value.Left) * sprite.scale)),
                                        (int)(sprite.position.Y - ((sprite.origin.Y - sprite.textureLocation.Value.Top)) * sprite.scale),
                                        (int)(localHitbox.Width * sprite.scale),
                                        (int)(localHitbox.Height * sprite.scale));
            }
            else
            {
                hitbox = new Rectangle((int)(sprite.position.X - (sprite.origin.X * sprite.scale)),
                                (int)(sprite.position.Y - sprite.origin.Y * sprite.scale),
                                (int)(localHitbox.Width * sprite.scale),
                                (int)(localHitbox.Height * sprite.scale));
            }


            Vector2[] hitboxCorners = new Vector2[4];
            Vector2 offset = new Vector2(hitbox.X + hitbox.Width / 2, hitbox.Y + hitbox.Height / 2);
            hitboxCorners[0] = new Vector2(hitbox.Left, hitbox.Top) - offset;
            hitboxCorners[1] = new Vector2(hitbox.Right, hitbox.Top) - offset;
            hitboxCorners[2] = new Vector2(hitbox.Left, hitbox.Bottom) - offset;
            hitboxCorners[3] = new Vector2(hitbox.Right, hitbox.Bottom) - offset;

            float cos_rot = (float)Math.Cos(sprite._rotation);
            float sin_rot = (float)Math.Sin(sprite._rotation);

            for (int i = 0; i < hitboxCorners.Length; i++)
            {
                corners[i].X = hitboxCorners[i].X * cos_rot - hitboxCorners[i].Y * sin_rot;
                corners[i].Y = hitboxCorners[i].X * sin_rot + hitboxCorners[i].Y * cos_rot;
                corners[i] += offset;
            }
            return hitbox;
        }

        public void DrawHitbox()
        {
            Color red = Color.DarkRed;
            red.A = (byte).5;
            //sprite.data.batch.Draw(texture, GetHitbox(), red);
            GetHitbox();
            foreach (Vector2 cornor in corners)
            {
                sprite.data.batch.Draw(texture, cornor, Color.Yellow);
            }

            sprite.data.batch.Draw(texture, sprite.position, Color.Yellow);
        }

        private Rectangle GetLocalHitbox()
        {
            if (sprite.textureLocation.HasValue)
            {
                return (Rectangle)sprite.textureLocation;
            }
            else
            {
                Texture2D Texture = sprite.texture;
                Color[,] Colors = TextureTo2DArray(Texture);

                int x1 = 9999999, y1 = 9999999;
                int x2 = -999999, y2 = -999999;

                for (int a = 0; a < Texture.Width; a++)
                {
                    for (int b = 0; b < Texture.Height; b++)
                    {
                        if (Colors[a, b].A != 0)
                        {
                            if (x1 > a) x1 = a;
                            if (x2 < a) x2 = a;

                            if (y1 > b) y1 = b;
                            if (y2 < b) y2 = b;
                        }
                    }
                }

                return new Rectangle(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
            }
        }

        private Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }
    }
}
