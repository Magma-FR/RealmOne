using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs;
using RealmOne.RealmPlayer;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class OrchidRingRing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("OrchidRing");
        }


        public override void SetDefaults()
        {
            Projectile.width = 600;
            Projectile.height = 600;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 10;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.alpha = 200;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Projectile.Center = Main.player[Projectile.owner].Center;
            Projectile.rotation += 0.005f;

            if (Main.player[Projectile.owner].HasBuff<OrchidBonus>())
            {
                Projectile.timeLeft = 2;
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }

    }
}