using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace RealmOne
{
    public class RealmModMenu : ModMenu
    {
        private const string menuAssetPath = "RealmOne/Assets/Textures/Menu"; // Creates a constant variable representing the texture path, so we don't have to write it out multiple times

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("RealmOne/Assets/Textures/Menu/logoFUNNY", (AssetRequestMode)2);

        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/WormSun");

        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/WormMoon");

        public override int Music => MusicLoader.GetMusicSlot(this.Mod, "Assets/Music/Rlyeh");
        private float floatX;

        private float floatY;

        public override string DisplayName => "FARMER EMOJI YES YES";

        public override void OnSelected()
        {
            SoundEngine.PlaySound(rorAudio.ModMenuClick);
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            logoScale = 0.6f;
            Texture2D MenuBG = (Texture2D)ModContent.Request<Texture2D>("RealmOne/Assets/Textures/Menu/logoFUNNY", (AssetRequestMode)2);
            Vector2 zero = Vector2.Zero;
            float width = Main.screenWidth / (float)MenuBG.Width;
            _ = Main.screenHeight / (float)MenuBG.Height;
            spriteBatch.Draw(MenuBG, new Vector2(zero.X + MathHelper.Lerp(-98f, -82f, floatX), zero.Y + MathHelper.Lerp(-50f, -47f, floatY)), null, Color.White, 0f, Vector2.Zero, width * 1f, 0, 0f);
            return true;
        }
    }

    public class OfficeMenu : ModMenu
    {
        private const string menuAssetPath = "RealmOne/Assets/Textures/Menu"; // Creates a constant variable representing the texture path, so we don't have to write it out multiple times

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("RealmOne/Assets/Textures/Menu/officeLogo", (AssetRequestMode)2);

        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/WormSun");

        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/WormMoon");

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Rlyeh");
        private float floatX;

        private float floatY;

        public override string DisplayName => "The Beginning of Creation";

        public override void OnSelected()
        {
            SoundEngine.PlaySound(rorAudio.ModMenuClick);
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            logoScale = 0.8f;
            drawColor = Color.White;
            Texture2D MenuBG = (Texture2D)ModContent.Request<Texture2D>("RealmOne/Assets/Textures/Menu/office", (AssetRequestMode)2);
            Vector2 zero = Vector2.Zero;
            float width = Main.screenWidth / (float)MenuBG.Width;
            _ = Main.screenHeight / (float)MenuBG.Height;
            spriteBatch.Draw(MenuBG, new Vector2(zero.X + MathHelper.Lerp(-0f, -0f, floatX), zero.Y + MathHelper.Lerp(-50f, -47f, floatY)), null, Color.White, 0f, Vector2.Zero, width * 1f, 0, 0f);
            return true;
        }
    }

    public class BattleMenu : ModMenu
    {
        private const string menuAssetPath = "RealmOne/Assets/Textures/Menu"; // Creates a constant variable representing the texture path, so we don't have to write it out multiple times

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("RealmOne/Assets/Textures/Menu/SuperLogo", (AssetRequestMode)2);

        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/WormSun");

        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/WormMoon");

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Rlyeh");
        private readonly float floatX;
        private readonly float floatY;

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
            Texture2D MenuBG = (Texture2D)ModContent.Request<Texture2D>("RealmOne/Assets/Textures/Menu/battle", (AssetRequestMode)2);
            Vector2 zero = Vector2.Zero;
            float width = Main.screenWidth / (float)MenuBG.Width;
            _ = Main.screenHeight / (float)MenuBG.Height;
            spriteBatch.Draw(MenuBG, new Vector2(zero.X + MathHelper.Lerp(-0f, -0f, floatX), zero.Y + MathHelper.Lerp(-50f, -47f, floatY)), null, Color.White, 0f, Vector2.Zero, width * 1f, 0, 0f);
            return true;
        }
    }
}