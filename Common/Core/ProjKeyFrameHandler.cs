using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RealmOne.Common.Core
{
    public class ProjKeyFrameHandler
    {
        public delegate Vector2 CustomFunction(Vector2 start, Vector2 end, float t);

        CustomFunction Customfunc;

        KeyFrameInterpolationCurve keyFrameInterpolationCurve;
        static string texturePath;
        int pointCount;
        Texture2D tex;

        /// <param name="keyFrameInterpolationCurve">The method of interpolating new points between the provided points.</param>
        /// <param name="texPath">The path to the texture with the keypoints on it.</param>
        /// <param name="totalPointCount">The total amount of points to be returned.</param>
        /// <param name="CustomInterpolationFunc">Only set this to something if you have chosen KeyFrameInterpolationCurve.Custom.</param>
        public ProjKeyFrameHandler(KeyFrameInterpolationCurve keyFrameInterpolationCurve, string texPath, int totalPointCount = 30, CustomFunction CustomInterpolationFunc = null)
        {
            this.keyFrameInterpolationCurve = keyFrameInterpolationCurve;
            texturePath = texPath;
            pointCount = totalPointCount;
            tex = (Texture2D)ModContent.Request<Texture2D>(texturePath, ReLogic.Content.AssetRequestMode.ImmediateLoad);
            Customfunc = CustomInterpolationFunc;
        }

        /// <param name="radius">The radius of the interpolated circle. Only set this if youre using KeyFrameInterpolationCurve.Slerp.</param>
        /// <returns>The interpolated points.</returns>
        public List<Vector2> GetPoints(float radius = default)
        {
            List<Vector2> returnPoints = new();
            int height = tex.Height;
            int width = tex.Width;
            
            Color[] colorData = new Color[tex.Width * tex.Height];
            tex.GetData(colorData);
            List<Vector2> coordsForColorData = new();

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    Color currPixel = colorData[row * width + column];

                    if (currPixel.A != 0)
                    {
                        coordsForColorData.Add(new Vector2(column, row));
                    }
                }
            }

            switch (keyFrameInterpolationCurve)
            {
                //might implement lagrange polynomial later but nrn
                case KeyFrameInterpolationCurve.Bezier:
                    if (coordsForColorData.Count != 1)
                    {
                        for (float k = 0; k <= pointCount; k++)
                        {
                            returnPoints.Add(EasingFunctions.Bezier(coordsForColorData.ToArray(), k / pointCount));
                        }
                        return returnPoints;
                    }
                    return null;
                case KeyFrameInterpolationCurve.Lerp:
                    if (coordsForColorData.Count != 1)
                    {
                        for (int i = 0; i < coordsForColorData.Count - 1; i++)
                        {
                            for (float k = 0; k <= pointCount / coordsForColorData.Count; k++)
                            {
                                returnPoints.Add(Vector2.Lerp(coordsForColorData[i], coordsForColorData[i + 1], k / (pointCount / coordsForColorData.Count)));
                            }
                        }
                        return returnPoints;
                    }
                    return null;
                case KeyFrameInterpolationCurve.Slerp:
                    if (coordsForColorData.Count != 1)
                    {
                        for (int i = 0; i < coordsForColorData.Count - 1; i++)
                        {
                            for (float k = 0; k <= pointCount / coordsForColorData.Count; k++)
                            {
                                //NO ACTUAL WAY I SPENT 2 DAYS DEBUGGING AND GOING INSANE OVER ME SWAPPING TWO NUMBERS
                                //anyway heres the normal code that shouldve worked all along

                                Vector2 center = new((coordsForColorData[coordsForColorData.Count - 1].X + coordsForColorData[0].X) / 2, (coordsForColorData[coordsForColorData.Count - 1].Y + coordsForColorData[0].Y) / 2); //average of the two points 
                                float maxRotation = center.AngleTo(coordsForColorData[i + 1]) - center.AngleTo(coordsForColorData[i]);
                                returnPoints.Add(EasingFunctions.Slerp(coordsForColorData[i], coordsForColorData[i + 1], k / (pointCount / coordsForColorData.Count) * maxRotation, center ,radius));
                            }
                        }
                        return returnPoints;
                    }
                    return null;
                case KeyFrameInterpolationCurve.Custom:
                    if (coordsForColorData.Count != 1)
                    {
                        for (int i = 0; i < coordsForColorData.Count - 1; i++)
                        {
                            for (float k = 0; k < pointCount / coordsForColorData.Count; k++)
                            {
                                returnPoints.Add(Customfunc(coordsForColorData[i], coordsForColorData[i + 1], k / pointCount / coordsForColorData.Count));
                            }
                        }
                        return returnPoints;
                    }
                    return null;
                case KeyFrameInterpolationCurve.CompleteCustom:
                    if (coordsForColorData.Count != 1)
                        Customfunc(default, default, default);
                    return null;
                default:
                    return null;
            }
        }
        public delegate Vector2 DesiredChange(Vector2 point, int i);
        /// <summary>
        /// Change all points in a certain way, implemented by desiredchange.
        /// </summary>
        /// <param name="points">The returned keypoints from GetPoints().</param>
        /// <param name="desiredChange">How you would like to change the points.</param>
        public void ChangePoints(ref List<Vector2> points, DesiredChange desiredChange)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = desiredChange(points[i], i);
            }
        }
        /// <summary>
        /// Calculates the correct position of the projectile based on the parameters. To use, set the projectile's center to what this function returns.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        /// <param name="mouse">Make sure to only capture Main.Mouseworld as a variable when the projectile spawns in OnSpawn, dont use Main.Mouseworld in the AI hook.</param>
        /// <param name="owner">The projectile's owner.</param>
        /// <param name="points">The returned keypoints from GetPoints().</param>
        /// <param name="i">A timer. Set this to zero if the player is facing right, keypoints.Count - 1 if facing left. Set this in OnSpawn.</param>
        /// <param name="projOffset">A vector2 offset for the position of the projectile.</param>
        /// <param name="upswing">Use in conjunction with a modplayer variable. Whether the projectile swings up or not.</param>
        /// <param name="rotOffset">A float offset for the rotation of the projectile, measured in radians</param>
        /// <returns>The projectile's center with the right movement applied from the points.</returns>
        public Vector2 CalculateSwordSwingPointsAndApplyRotation(Projectile projectile, Vector2 mouse, Player owner, List<Vector2> points, ref int i, Vector2 projOffset = default, bool upswing = false, float rotOffset = 0)
        {
            if (projOffset == default)
            {
                projOffset = Vector2.Zero;
            }
            if (upswing)
            {
                rotOffset -= MathHelper.PiOver2 * owner.direction;
            }

            if ((owner.direction == 1 && !upswing) || owner.direction == -1 && upswing)
            {
                if (i < points.Count - 1)
                {
                    i++;
                    projectile.rotation = owner.Center.DirectionTo(owner.Center + points[i]).ToRotation() + MathHelper.PiOver4 + owner.Center.DirectionTo(mouse).ToRotation() + rotOffset;
                }
                else
                {
                    projectile.Kill();
                }
            }
            else
            {
                if (i > 0)
                {
                    i--;
                    projectile.rotation = owner.Center.DirectionTo(owner.Center + points[i]).ToRotation() - MathHelper.PiOver4 + MathHelper.Pi + owner.Center.DirectionTo(mouse).ToRotation() + rotOffset;
                }
                else
                {
                    projectile.Kill();
                }
            }

            return owner.Center + points[i].RotatedBy(owner.Center.DirectionTo(mouse).ToRotation()) + projOffset;
        }
        /// <summary>
        /// Sets common variables that most held projectiles have. Call this in AI.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        /// <param name="owner">The projectile's owner.</param>
        /// <param name="mouse">Make sure to only capture Main.Mouseworld as a variable when the projectile spawns in OnSpawn, dont use Main.Mouseworld in the AI hook.</param>
        public void SetAiDefaults(Projectile projectile, Player owner, Vector2 mouse)
        {
            //cool that i dont have to make these ref 
            projectile.direction = owner.direction;
            projectile.spriteDirection = projectile.direction;
            owner.direction = Math.Sign(owner.DirectionTo(mouse).X);
            owner.heldProj = projectile.whoAmI;
            owner.itemTime = 2;
            owner.itemAnimation = 2;
        }
    }
    //long ahh name
    public enum KeyFrameInterpolationCurve
    {
        Lerp,
        Slerp,
        Bezier,
        Custom,
        CompleteCustom
    }
}
