using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RealmOne.Common.Core
{
    public class BloodMoonSky : Mod
    {
        public override void Load()
        {

            if (!Main.dedServ && Main.bloodMoon)
            {
                Filters.Scene["YourMod:BloodMoonSky"] = new Filter(new ScreenShaderData("FilterBloodMoonSky"), EffectPriority.VeryLow);

                // Make the filter active only during blood moons
                Filters.Scene["YourMod:BloodMoonSky"].IsActive();
            }
        }
    
    
    public class FilterBloodMoonSky : ScreenShaderData
    {
        private float strength;

        public FilterBloodMoonSky(string passName) : base(passName) { }

        public override void Update(GameTime gameTime)
        {
            // Adjust the strength of the sky filter based on your custom needs
            strength = 3;
            base.Update(gameTime);
        }

        public override void Apply()
        {
            // Apply your custom gradient sky effect here
            // You can use spriteBatch to draw your gradient or apply a custom shader
            // Example code using spriteBatch:

            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            // Calculate the gradient colors
            Color startColor = Color.DarkRed;
            Color endColor = Color.Red;

            int screenHeight = Main.screenHeight;
            Rectangle gradientRectangle = new Rectangle(0, 0, Main.screenWidth, screenHeight);

            for (int y = 0; y < screenHeight; y++)
            {
                float lerpAmount = (float)y / (float)screenHeight;
                Color currentColor = Color.Lerp(startColor, endColor, lerpAmount);

      //IDFK I HATE DRAW CODE          SpriteBatch.Draw(Main.MagicPixel, new Rectangle(0, y, Main.screenWidth, 1), currentColor);
            }

            Main.spriteBatch.End();

            base.Apply();
        }
    }
}
