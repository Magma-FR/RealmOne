using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria.ID;
using Terraria.Audio;
using RealmOne.Items.Weapons.PreHM.Impact;

namespace RealmOne.Buffs.Debuffs.ShockStacks
{
    public class Shocked7 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Electrified");
            Description.SetDefault("Shocked!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.player[Main.myPlayer].ownedProjectileCounts[ModContent.ProjectileType<LightningBolt>()] <= 0)
            {
                npc.DelBuff(buffIndex);
                SoundEngine.PlaySound(SoundID.Thunder, npc.Center);
                Main.player[Main.myPlayer].GetModPlayer<Screenshake>().BigShake = 20;
                if (Main.IsItStorming == true) // More dmg during storms :)
                {
                    Projectile.NewProjectile(Main.player[Main.myPlayer].GetSource_FromThis(), new Vector2(npc.Center.X, npc.Center.Y - 1500), Main.player[Main.myPlayer].velocity, ModContent.ProjectileType<LightningBolt>(), Main.rand.Next(80, 90), 20f, Main.myPlayer);
                }
                else
                {
                    Projectile.NewProjectile(Main.player[Main.myPlayer].GetSource_FromThis(), new Vector2(npc.Center.X, npc.Center.Y - 1500), Main.player[Main.myPlayer].velocity, ModContent.ProjectileType<LightningBolt>(), Main.rand.Next(50, 60), 20f, Main.myPlayer);
                }
                
            }
            
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return true;
        }
    }


}