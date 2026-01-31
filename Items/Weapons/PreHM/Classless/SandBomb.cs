/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Core;

namespace RealmOne.Items.Weapons.PreHM.Classless
{
    public class SandBomb : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.damage = 12;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<SandBombProj>();
            Item.shootSpeed = 7f;
            Item.DamageType = DamageClass.Generic;
        }
    }

    public class SandBombProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = ProjAIStyleID.GroundProjectile;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 140;
            Projectile.width = 26;
            Projectile.height = 34;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SandBombNado>(), Projectile.damage, 0, Main.myPlayer);
            proj.friendly = true;
            proj.hostile = false;
            proj.timeLeft = 180;

            SoundEngine.PlaySound(SoundID.DD2_BookStaffTwisterLoop, Projectile.position);
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.position);

            for (int i = 0; i < 5; i++)
            {
                GenericGlowParticle particle = new(new Vector2(proj.Center.X + Main.rand.Next(-30, 30), proj.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), Color.Yellow, 0.5f, 120);
                SparkleParticle sparkle = new(Color.SandyBrown, 1, new Vector2(proj.Center.X + Main.rand.Next(-30, 30), proj.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);

                ParticleSystem.GenerateParticle(sparkle);
                ParticleSystem.GenerateParticle(particle);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => Projectile.Kill();
    }

    public class SandBombNado : ModProjectile
    {
        public override string Texture => Helper.Empty;

        private static Asset<Texture2D> SandTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.penetrate = -2;
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 280;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.rotation += 0.18f;
            Projectile.velocity.X *= 0.0f;
            Projectile.velocity.Y *= 0.0f;
            Lighting.AddLight(Projectile.position, 1.5f, 0.7f, 2.5f);
            Lighting.Brightness(2, 2);

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && !npc.friendly && !npc.boss && npc.type != NPCID.TargetDummy)
                {
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);
                    if (distance <= 200)
                    {
                        Vector2 direction = npc.Center - Projectile.Center;
                        direction.Normalize();
                        npc.velocity -= direction * 0.5f;
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.ownerHitCheck = true;

            int radius = 250;

            // Damage enemies within the splash radius
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC target = Main.npc[i];
                if (target.active && !target.friendly && Vector2.Distance(Projectile.Center, target.Center) < radius)
                {
                    int damage = Projectile.damage * 2;
                    target.SimpleStrikeNPC(damage: 12, 0);
                }
            }
        }

        public override void Load()
        { // This is called once on mod (re)load when this piece of content is being loaded.
          // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            SandTexture = ModContent.Request<Texture2D>("RealmOne/Assets/Effects/vortex2");
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            SandTexture = null;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color drawColor = Lighting.GetColor((int)Projectile.Center.X / 16, (int)(Projectile.Center.Y / 16));

            Main.EntitySpriteDraw(SandTexture.Value, Projectile.Center - Main.screenPosition,
                          SandTexture.Value.Bounds, Color.SandyBrown, Projectile.rotation,
                          SandTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            return true;
        }
    }
}*/