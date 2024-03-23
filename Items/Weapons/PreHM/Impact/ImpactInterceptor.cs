using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs;
using RealmOne.Buffs.Debuffs;
using RealmOne.Buffs.Debuffs.ShockStacks;
using RealmOne.Common.Core;
using RealmOne.Common.Systems;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Impact
{
    public class ImpactInterceptor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impact Interceptor");
            Tooltip.SetDefault("Calls down randomly positioned pulse lasers that spark into electricity when homed onto an enemy"
                + "\nThe lasers depend on the mouse position when firing"
                + "\n'No WIFI password required'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemGlowy.AddItemGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Impact/ImpactInterceptor_Glow");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.mana = 8;
            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 2f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Blue;
            Item.shootSpeed = 4f;
            Item.useAnimation = 120;
            Item.useTime = 120;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 90);
            Item.shoot = ProjectileType<ImpactSonarShot>();
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Sonar");
            Item.scale = 1f;
            Item.ArmorPenetration = 999999999;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(1, 0);
            return offset;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/Impact/ImpactInterceptor_Glow", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),

                Color.LightCyan,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(rorAudio.PulsaPickup);

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(Mod, "ImpactTech", 12)

            .AddTile(TileID.Anvils)
            .Register();
        }
    }

    public class ImpactSonarShot : ModProjectile
    {
        public override string Texture => Helper.Empty;

        int size;
        bool fadeIN = false;

        public override void SetDefaults()
        {
            Projectile.height = 1;
            Projectile.width = 1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -2;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 80;
            Projectile.alpha = 255;
        }

        public override void OnSpawn(IEntitySource source)
        {
            size = 0;
            Projectile.ai[1] = 1f;
            Projectile.ai[2] = 1f;
        }

        public override void AI()
        {
            Projectile.ai[0] += 0.02f;
            size += 5;

            if (size == 100)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].Center, new Vector2(0, 0), ModContent.ProjectileType<ImpactLesserSonarShot>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            }
            if (size == 200)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].Center, new Vector2(0, 0), ModContent.ProjectileType<ImpactLesserSonarShot>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            }


            /*if (fadeIN == false)
            {
                if (Projectile.ai[1] < 0.6f)
                {
                    Projectile.ai[1] += 0.02f;
                }
                else if (Projectile.ai[1] == 0.6f)
                {
                    fadeIN = true;
                }

            }
            else
            {
                
            }*/

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Vector2.Distance(Projectile.Center, Main.npc[i].Center) < size && Vector2.Distance(Projectile.Center, Main.npc[i].Center) > size - 5 && Projectile.timeLeft > 20)
                {
                    int hitDir = 0;
                    bool crit = false;
                    if (Main.npc[i].Center.X < Projectile.Center.X)
                    {
                        hitDir = 1; // right
                    }
                    if (Main.npc[i].Center.X > Projectile.Center.X)
                    {
                        hitDir = -1; // left
                    }
                    if (Main.rand.Next(100) < Projectile.CritChance)
                    {
                        crit = true;
                    }
                    if (Main.npc[i].friendly == false)
                    {
                        Main.npc[i].SimpleStrikeNPC(Projectile.damage, hitDir, crit, Projectile.knockBack, DamageClass.Magic);
                        if (Main.npc[i].HasBuff<Copperized>())
                        {
                            Main.npc[i].AddBuff(BuffType<Cancelled>(), 300);
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.npc[i].Center, new Vector2(0, 0), ModContent.ProjectileType<ImpactEvenLesserSonarShot>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
                        }
                        else if (!Main.npc[i].HasBuff<Copperized>())
                        {
                            Main.npc[i].AddBuff(BuffType<NoConnect>(), 300);
                        }
                    }
                }
            }

            if (Projectile.timeLeft < 50)
            {
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 0.02f;
                    Projectile.ai[2] -= 0.02f;
                }

            }



        }

        public override void PostAI()
        {
            /*if (Projectile.ai[0] == 1)
                Projectile.damage = 0;*/
        }

        public override bool PreDraw(ref Color lightColor)
        {

            Texture2D tex = Request<Texture2D>("RealmOne/Assets/Effects/Pulsee").Value;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); float alpha = MathHelper.Lerp(4, 0, Projectile.alpha);
            for (int i = 0; i < 1; i++)
                Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, new Color(0, Projectile.ai[1], Projectile.ai[1], Projectile.ai[2]), Projectile.rotation, tex.Size() / 2, Projectile.ai[0], SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }

    public class ImpactLesserSonarShot : ModProjectile
    {
        public override string Texture => Helper.Empty;

        int size;
        bool fadeIN = false;

        public override void SetDefaults()
        {
            Projectile.height = 1;
            Projectile.width = 1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -2;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 80;
            Projectile.alpha = 255;
        }

        public override void OnSpawn(IEntitySource source)
        {
            size = 1;
            Projectile.ai[1] = 1f;
            Projectile.ai[2] = 1f;
        }

        public override void AI()
        {
            Projectile.ai[0] += 0.02f;
            size += 5;

            /*if (fadeIN == false)
            {
                if (Projectile.ai[1] < 0.6f)
                {
                    Projectile.ai[1] += 0.02f;
                }
                else if (Projectile.ai[1] == 0.6f)
                {
                    fadeIN = true;
                }

            }
            else
            {
                
            }*/

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Vector2.Distance(Projectile.Center, Main.npc[i].Center) < size && Vector2.Distance(Projectile.Center, Main.npc[i].Center) > size - 5 && Projectile.timeLeft > 20)
                {
                    int hitDir = 0;
                    bool crit = false;
                    if (Main.npc[i].Center.X < Projectile.Center.X)
                    {
                        hitDir = 1; // right
                    }
                    if (Main.npc[i].Center.X > Projectile.Center.X)
                    {
                        hitDir = -1; // left
                    }
                    if (Main.rand.Next(100) < Projectile.CritChance)
                    {
                        crit = true;
                    }
                    if (Main.npc[i].friendly == false)
                    {
                        Main.npc[i].SimpleStrikeNPC(Projectile.damage, hitDir, crit, Projectile.knockBack, DamageClass.Magic);
                        if (Main.npc[i].HasBuff<Copperized>())
                        {
                            Main.npc[i].AddBuff(BuffType<Cancelled>(), 300);
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.npc[i].Center, new Vector2(0, 0), ModContent.ProjectileType<ImpactEvenLesserSonarShot>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
                        }
                        else if (!Main.npc[i].HasBuff<Copperized>())
                        {
                            Main.npc[i].AddBuff(BuffType<NoConnect>(), 300);
                        }
                    }
                }
            }

            if (Projectile.timeLeft < 50)
            {
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 0.02f;
                    Projectile.ai[2] -= 0.02f;
                }

            }



        }

        public override void PostAI()
        {
            /*if (Projectile.ai[0] == 1)
                Projectile.damage = 0;*/
        }

        public override bool PreDraw(ref Color lightColor)
        {

            Texture2D tex = Request<Texture2D>("RealmOne/Assets/Effects/Pulsee").Value;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); float alpha = MathHelper.Lerp(4, 0, Projectile.alpha);
            for (int i = 0; i < 1; i++)
                Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, new Color(0, Projectile.ai[1], Projectile.ai[1], Projectile.ai[2]), Projectile.rotation, tex.Size() / 2, Projectile.ai[0], SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); return false;
        }


        public override bool ShouldUpdatePosition()
        {
            return false;
        }


    }

    public class ImpactEvenLesserSonarShot : ModProjectile
    {
        public override string Texture => Helper.Empty;

        int size;
        float g;
        bool fadeIN = false;

        public override void SetDefaults()
        {
            Projectile.height = 1;
            Projectile.width = 1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -2;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 80;
            Projectile.alpha = 255;
        }

        public override void OnSpawn(IEntitySource source)
        {
            size = 1;
            Projectile.ai[1] = 1f;
            g = 0.5f;
            Projectile.ai[2] = 1f;
        }

        public override void AI()
        {
            Projectile.ai[0] += 0.01f;
            size += 3;

            /*if (fadeIN == false)
            {
                if (Projectile.ai[1] < 0.6f)
                {
                    Projectile.ai[1] += 0.02f;
                }
                else if (Projectile.ai[1] == 0.6f)
                {
                    fadeIN = true;
                }

            }
            else
            {

            }*/

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Vector2.Distance(Projectile.Center, Main.npc[i].Center) < size && Vector2.Distance(Projectile.Center, Main.npc[i].Center) > size - 5 && Projectile.timeLeft > 20)
                {
                    int hitDir = 0;
                    bool crit = false;
                    if (Main.npc[i].Center.X < Projectile.Center.X)
                    {
                        hitDir = 1; // right
                    }
                    if (Main.npc[i].Center.X > Projectile.Center.X)
                    {
                        hitDir = -1; // left
                    }
                    if (Main.rand.Next(100) < Projectile.CritChance)
                    {
                        crit = true;
                    }
                    if (Main.npc[i].friendly == false)
                    {
                        Main.npc[i].SimpleStrikeNPC(Projectile.damage, hitDir, crit, Projectile.knockBack, DamageClass.Magic);
                        Main.npc[i].AddBuff(BuffType<NoConnect>(), 300);
                    }
                }
            }

            if (Projectile.timeLeft < 50)
            {
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 0.02f;
                    Projectile.ai[2] -= 0.02f;
                    g -= 0.01f;
                }

            }



        }

        public override void PostAI()
        {
            /*if (Projectile.ai[0] == 1)
                Projectile.damage = 0;*/
        }

        public override bool PreDraw(ref Color lightColor)
        {

            Texture2D tex = Request<Texture2D>("RealmOne/Assets/Effects/Pulsee").Value;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); float alpha = MathHelper.Lerp(4, 0, Projectile.alpha);
            for (int i = 0; i < 1; i++)
                Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, new Color(Projectile.ai[1], g, 0, Projectile.ai[2]), Projectile.rotation, tex.Size() / 2, Projectile.ai[0], SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hitinfo, int damage)
        {
            target.AddBuff(BuffType<NoConnect>(), 300);
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}