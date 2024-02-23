using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.Common.Systems;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace RealmOne
{
    public class RealmModMenu : ModMenu
    {
        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("RealmOne/Assets/Textures/Menu/SuperLogo", (AssetRequestMode)2);

        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>("RealmOne/Assets/Textures/Empty");

        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>("RealmOne/Assets/Textures/Empty");

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Rlyeh");

        public override string DisplayName => "Otherworldly Rampage";

        public override void OnSelected()
        {
            SoundEngine.PlaySound(rorAudio.ModMenuClick);
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            Texture2D tex2 = ModContent.Request<Texture2D>("RealmOne/Assets/Effects/Idol").Value;
            Color color = Color.White;
            color.A = 0;
            spriteBatch.Draw(tex2, logoDrawCenter, null, color, logoRotation, tex2.Size() / 2f, logoScale, 0, 0);
            logoRotation *= 3f;

            logoScale = 0.8f;
            drawColor = Color.White;

            Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/MainMenu/MenuBackground").Value;

            Vector2 drawOffset = Vector2.Zero;
            float xScale = (float)Main.screenWidth / texture.Width;
            float yScale = (float)Main.screenHeight / texture.Height;
            float scale = xScale;

            if (xScale != yScale)
            {
                if (yScale > xScale)
                {
                    scale = yScale;
                    drawOffset.X -= (texture.Width * scale - Main.screenWidth) * 0.5f;
                }
                else
                    drawOffset.Y -= (texture.Height * scale - Main.screenHeight) * 0.5f;
            }

            spriteBatch.Draw(texture, drawOffset, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            return true;
        }
    }
}