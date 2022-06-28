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
        public Rectangle boundingBox;
        private Color boundingColor = Color.Red;
        Vector2[] corners = new Vector2[4];


        public Hitbox(Sprite sprite)
        {
            this.sprite = sprite;
            this.localHitbox = GetLocalHitbox();
            texture = new Texture2D(sprite.data.game.GraphicsDevice, 4, 4);
            texture.SetData(new[] {Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White});
            //texture = new Texture2D(sprite.data.game.GraphicsDevice, 1, 1);
            //texture.SetData(new[] { Color.White } );
            //Debug.WriteLine(localHitbox
            boundingColor.A = (byte).5;
        }

        public void UpdateHitbox()
        {
            int xTemp = 0;
            int yTemp = 0;
            if (sprite.textureLocation.HasValue)
            {
                xTemp = sprite.textureLocation.Value.Left;
                yTemp = sprite.textureLocation.Value.Top;
            }
            Rectangle hitbox = new Rectangle((int)(sprite.position.X - (sprite.origin.X - xTemp) * sprite.scale),
                            (int)(sprite.position.Y - (sprite.origin.Y - yTemp) * sprite.scale),
                            (int)(localHitbox.Width * sprite.scale),
                            (int)(localHitbox.Height * sprite.scale));

            Vector2 offset = new Vector2(sprite.position.X, sprite.position.Y);
            Vector2[] hitboxCorners = new Vector2[]
            {
                new Vector2(hitbox.Left, hitbox.Top) - offset,
                new Vector2(hitbox.Right, hitbox.Top) - offset,
                new Vector2(hitbox.Left, hitbox.Bottom) - offset,
                new Vector2(hitbox.Right, hitbox.Bottom) - offset,
            };

            //if (Math.Abs(sprite.rotation) > 1e-10)
            {
                float cos_rot = (float)Math.Cos(sprite._rotation);
                float sin_rot = (float)Math.Sin(sprite._rotation);

                for (int i = 0; i < hitboxCorners.Length; i++)
                {
                    corners[i].X = hitboxCorners[i].X * cos_rot - hitboxCorners[i].Y * sin_rot;
                    corners[i].Y = hitboxCorners[i].X * sin_rot + hitboxCorners[i].Y * cos_rot;

                    corners[i] += offset;
                }
            }

            int bbxMin = (int) Math.Min(Math.Min(corners[0].X,corners[1].X), Math.Min(corners[2].X, corners[3].X));
            int bbyMin = (int) Math.Min(Math.Min(corners[0].Y,corners[1].Y), Math.Min(corners[2].Y, corners[3].Y));
            int bbxMax = (int) Math.Max(Math.Max(corners[0].X,corners[1].X), Math.Max(corners[2].X, corners[3].X));
            int bbyMax = (int) Math.Max(Math.Max(corners[0].Y,corners[1].Y), Math.Max(corners[2].Y, corners[3].Y));

            boundingBox = new Rectangle(bbxMin, bbyMin, bbxMax + 1 - bbxMin, bbyMax - bbyMin);
        }

        public bool IsColliding(Sprite sprite)
        {
            UpdateHitbox();
            sprite.hitbox.UpdateHitbox();

            //If bounding boxes don't overlap
            //if(xmax1 >= xmin2 and xmax2 >= xmin1)
            if (!((boundingBox.Right >= sprite.hitbox.boundingBox.Left) && (sprite.hitbox.boundingBox.Right >= boundingBox.Left) &&
                (boundingBox.Bottom >= sprite.hitbox.boundingBox.Top) && (sprite.hitbox.boundingBox.Bottom >= boundingBox.Top)))
            {
                //Bounding boxes don't collide
                return false;
            }
            return true;
        }

        public void DrawHitbox()
        {
            UpdateHitbox();
            sprite.data.batch.Draw(texture, boundingBox, boundingColor);
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
