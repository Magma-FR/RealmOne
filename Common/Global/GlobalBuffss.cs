using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Common.Global
{
    public class GlobalBuffss : GlobalBuff
    {
        public class ThornsPotionChange : GlobalBuff
        {
            public override void Update(int type, Player player, ref int buffIndex)
            {
                if (type == BuffID.Thorns && player.HasBuff(BuffID.Thorns))
                    player.GetModPlayer<ThornsPotionChangePlayer>().thornsPoisoned = true;
            }
        }

        public class ThornsPotionChangePlayer : ModPlayer
        {
            public bool thornsPoisoned;

            public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
            {
                if (thornsPoisoned && !target.boss && target.lifeMax > 5)
                    target.AddBuff(BuffID.Poisoned, 500);
            }

            public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
            {
                if (thornsPoisoned && !target.boss && target.lifeMax > 5)
                    target.AddBuff(BuffID.Poisoned, 500);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, int type, int buffIndex, ref BuffDrawParams drawParams)
        {
            if (type == ModContent.BuffType<HazardBuff>())
            {
                drawParams.DrawColor = Main.DiscoColor * Main.buffAlpha[buffIndex];

                Vector2 shake = new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3));

                drawParams.Position += shake;
                drawParams.TextPosition += shake;
            }
            return true;
        }
    }
}