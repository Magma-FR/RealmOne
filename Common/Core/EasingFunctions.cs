using Microsoft.Xna.Framework;
using Terraria;

namespace RealmOne.Common.Core
{
    internal static class EasingFunctions
    {
        public static Vector2 Slerp(Vector2 start, Vector2 end, float t, Vector2 center, float radius = default)
        {
            return radius == default ? (center).DirectionTo(start.RotatedBy(t, center)) : (center).DirectionTo(start.RotatedBy(t, center)) * radius;
            //alt implementations
            //return Utils.AngleLerp(start.ToRotation(), end.ToRotation(), t).ToRotationVector2();
            // return new((float)Math.Cos(t), (float)Math.Sin(t)); //* radius;
        }

        public static Vector2 Bezier(Vector2[] controlPoints, float t)
        {
            //my own implementation of a bezier that takes any number of points
            Vector2[] temp = controlPoints;
            while (temp.Length > 1)
            {
                temp = BezierCalculation(temp, t);
            }
            return temp[0];
        }

        private static Vector2[] BezierCalculation(Vector2[] c, float t)
        {
            Vector2[] temp = new Vector2[c.Length - 1];
            for (int i = 0; i < c.Length - 1; i++)
            {
                temp[i] = Vector2.Lerp(c[i], c[i + 1], t);
            }
            return temp;
        }
    }
}