/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;

namespace RealmOne.Common
{
    public class BloodSky: CustomSky
    {
        public bool isActive;
        public float Intensity;
        public override void Activate(Vector2 position, params object[] args)
        {
            Main.bloodMoon = true;
            isActive = true;
        }
        public override void Deactivate(params object[] args)
        {
            Main.bloodMoon = false;

            isActive = false;
        }
        public override void Reset()
        {
            isActive = false;
        }
        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                Intensity = Math.Min(1f, 0.01f + Intensity);
            }
            else
            {
                Intensity = Math.Max(0f, Intensity - 0.01f);
            }

        }
        public override bool IsActive()
        {
            return Main.bloodMoon && Intensity > 0;
        }
        float glow;
        //float intensity;
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                Texture2D Tex = ModContent.Request<Texture2D>("RealmOne/Assets/Effects/BloodSky").Value;
                
                Vector2 Pos = new(Main.screenWidth / 2, Main.screenHeight / 2);
              
                if (Main.screenWidth > Tex.Width || Main.screenHeight > Tex.Height)
                    spriteBatch.Draw(Tex, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), null, Color.White * Intensity * 0.5f, 0, Vector2.Zero, SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(Tex, Pos, null, Color.White * Intensity * 0.5f, 0f, new Vector2(Tex.Width >> 1, Tex.Height >> 1), 1f, SpriteEffects.None, 1f);
                glow += Main.rand.NextFloat(-.1f, .1f);
                glow = MathHelper.Clamp(glow, 0, 1);
               

            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
        }
        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.2f));
        }
    }
}
*/