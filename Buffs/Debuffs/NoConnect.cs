using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria.ID;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Dusts;
using Terraria.Audio;
using RealmOne.Common.Systems;
using Microsoft.Xna.Framework.Graphics;

namespace RealmOne.Buffs.Debuffs
{
    public class NoConnect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("No Connection");
            Description.SetDefault("");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NoConnectionGlobal>().NoConnect = true;
            if (npc.buffTime[buffIndex] < 1)
            {
                npc.GetGlobalNPC<NoConnectionGlobal>().stuck = 0;
                npc.GetGlobalNPC<NoConnectionGlobal>().dir = 0;
            }
        }
    }

    public class NoConnectionGlobal : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool NoConnect;
        public int dir = 0;
        public int stuck = 0;

        public override void ResetEffects(NPC npc)
        {
            NoConnect = false;
        }

        public override bool PreAI(NPC npc)
        {
            if (NoConnect)
                if (dir == 0)
                    dir = npc.direction;

            return base.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {

            if (NoConnect)
            {
                if (Main.rand.Next(200) < 3)
                {
                    if (stuck == 0 && npc.type != NPCID.TargetDummy)
                    {
                        stuck = 60;
                        //SoundEngine.PlaySound(rorAudio.SFX_Sonar, npc.Center);
                    }

                }
                if (stuck > 0)
                {
                    stuck--;

                    npc.direction = dir;
                    npc.velocity /= 20f;
                }

            }
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("RealmOne/Assets/Textures/NoConnection").Value;

            if (stuck > 0)
            {
                Main.spriteBatch.Draw(tex, new Vector2(npc.Center.X - 32, npc.Center.Y - 32) - Main.screenPosition, Color.White);
            }

        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (NoConnect)
                drawColor = new Color(1f, 0f, 0f);

        }
    }
}