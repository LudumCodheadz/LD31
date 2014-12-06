using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CodheadzLD31.Utils
{
    public static class Extensions
    {
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, Texture2D pixel )
        {
            DrawLine(spriteBatch, start, end, color, pixel, 2.0f);
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, Texture2D pixel, float thickness)
        {
            Vector2 delta = end - start;
            spriteBatch.Draw(pixel, start, null, color, delta.ToAngle(), new Vector2(0, 0.5f), new Vector2(delta.Length(), thickness), SpriteEffects.None, 0f);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle drawRectangle, Color color, Texture2D pixel)
        {
            DrawRectangle(spriteBatch,drawRectangle,color,pixel,2.0f);
        }
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle drawRectangle, Color color, Texture2D pixel, float thickness)
        {
            spriteBatch.DrawLine(new Vector2(drawRectangle.Left, drawRectangle.Top), new Vector2(drawRectangle.Right, drawRectangle.Top),color,pixel, thickness);
            spriteBatch.DrawLine(new Vector2(drawRectangle.Left, drawRectangle.Bottom), new Vector2(drawRectangle.Right, drawRectangle.Bottom), color, pixel, thickness);
            spriteBatch.DrawLine(new Vector2(drawRectangle.Left, drawRectangle.Top), new Vector2(drawRectangle.Left, drawRectangle.Bottom), color, pixel, thickness);
            spriteBatch.DrawLine(new Vector2(drawRectangle.Right, drawRectangle.Top), new Vector2(drawRectangle.Right, drawRectangle.Bottom), color, pixel, thickness);
        }

        public static void BeginPixel(this SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        }

        public static float ToAngle(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static Vector2 ScaleTo(this Vector2 vector, float length)
        {
            return vector * (length / vector.Length());
        }

        public static Point ToPoint(this Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }


        public static float NextFloat(this Random rand, float minValue, float maxValue)
        {
            return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static Vector2 NextVector2(this Random rand, float minLength, float maxLength)
        {
            double theta = rand.NextDouble() * 2 * Math.PI;
            float length = rand.NextFloat(minLength, maxLength);
            return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
        }
    }
}
