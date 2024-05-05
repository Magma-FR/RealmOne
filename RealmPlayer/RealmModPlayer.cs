using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs;
using RealmOne.Buffs.Debuffs;
using RealmOne.Common.Core;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Systems;
using RealmOne.Items.Opens;
using RealmOne.Items.PaperUI;
using RealmOne.Items.Weapons.PreHM.Impact;
using RealmOne.Projectiles.Bullet;
using RealmOne.Projectiles.Magic;
using RealmOne.Projectiles.Other;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace RealmOne.RealmPlayer
{
    public static class Zones
    {
        public static bool ZoneFarmy(this Player player) => player.InModBiome<Biomes.Farm.FarmSurface>();
    }

    public class Scrolly : ModPlayer
    {
        public bool ShowScroll = false;

        public override void PostUpdate()
        {
            if (ShowScroll == true)
            {
                Player target = Main.LocalPlayer;
            }

            base.PostUpdate();
        }
    }

    public class ScrollyWorm : ModPlayer

    {
        public bool ShowWorm1 = false;

        public override void PostUpdate()
        {
            if (ShowWorm1 == true)
            {
                Player target = Main.LocalPlayer;
            }

            base.PostUpdate();
        }
    }

    //ALL THIS CODE UP TO THE # IS SPIRIT MOD'S GITHUB CODE, ALL CREDIT GOES TO THEM.
    public class ItemGlowy : ModPlayer
    {
        internal new static void Unload()
        {
            ItemGlowMask.Clear();
        }

        public static void AddGlowMask(int itemType, string texturePath)
        {
            ItemGlowMask[itemType] = ModContent.Request<Texture2D>(texturePath, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }

        internal static readonly Dictionary<int, Texture2D> ItemGlowMask = new();
        public int TexturesDefaults = 0;
    }

    public class GlowMaskItemLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new BeforeParent(PlayerDrawLayers.ArmOverItem);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Item item = drawInfo.drawPlayer.HeldItem;

            if (item.type >= ItemID.Count && ItemGlowy.ItemGlowMask.TryGetValue(item.type, out Texture2D textureItem) && (drawInfo.drawPlayer.itemTime > 0 || item.useStyle != ItemUseStyleID.None)) //Held ItemType
                GlowmMaskSystem.DrawItemGlowMask(textureItem, drawInfo);
        }
    }

    //#

    public class Screenshake : ModPlayer
    {
        private int timer = 0;
        public bool SmallScreenshake = false;
        private bool makeTimerWork = false;

        public int ScreenShake = 0;
        public int BigShake = 0;

        private int timer1 = 0;
        public bool BombScreenshake = false;
        private bool makeTimerWork1 = false;

        private int timerworm = 0;
        public bool WormScreenshake = false;
        private bool WormTimerWork = false;

        private int LongShakeTimer = 0;
        public bool LongShake = false;
        private bool LongShakeWork = false;

        //public Vector2 beamStart;
        // public Vector2 beamEnd;

        public override void ModifyScreenPosition()
        {
            //screenshake
            if (SmallScreenshake == true)
            {
                makeTimerWork = true;
            }

            if (ScreenShake > 0)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-3, 4), Main.rand.Next(-3, 4));
                ScreenShake--;
            }

            if (BigShake > 0)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7));
                BigShake--;
            }

            if (makeTimerWork == true)
            {
                int power = 6;

                Vector2 random = new(Main.rand.Next(-power, power), Main.rand.Next(-power, power));

                timer++;
                if (timer > 0)
                {
                    Main.screenPosition += random;
                }

                if (timer >= 10)
                {
                    timer = 0;
                    makeTimerWork = false;
                }
            }

            if (BombScreenshake == true)
            {
                makeTimerWork1 = true;
            }

            if (makeTimerWork1 == true)
            {
                int power1 = 22;

                Vector2 random1 = new(Main.rand.Next(-power1, power1), Main.rand.Next(-power1, power1));

                timer1++;
                if (timer1 > 0)
                {
                    Main.screenPosition += random1;
                }

                if (timer1 >= 21)
                {
                    timer1 = 0;
                    makeTimerWork1 = false;
                }
            }

            if (WormScreenshake == true)
            {
                WormTimerWork = true;
            }

            if (WormTimerWork == true)
            {
                int powerworm = 22;

                Vector2 randomworm = new(Main.rand.Next(-powerworm, powerworm), Main.rand.Next(-powerworm, powerworm));

                timerworm++;
                if (timerworm > 0)
                {
                    Main.screenPosition += randomworm;
                }

                if (timerworm >= 21)
                {
                    timerworm = 0;
                    WormTimerWork = false;
                }
            }

            if (LongShake == true)
            {
                LongShakeWork = true;
            }

            if (LongShakeWork == true)
            {
                int longpower = 11;

                Vector2 randomlong = new(Main.rand.Next(-longpower, longpower), Main.rand.Next(-longpower, longpower));

                LongShakeTimer++;
                if (LongShakeTimer > 0)
                {
                    Main.screenPosition += randomlong;
                }

                if (LongShakeTimer >= 320)
                {
                    LongShakeTimer = 0;
                    LongShakeWork = false;
                }
            }
        }

        //screenshake

        public override void ResetEffects()
        {
            if (!makeTimerWork)
            {
                SmallScreenshake = false;
            }

            if (!makeTimerWork)
            {
                BombScreenshake = false;
            }

            if (!makeTimerWork)
            {
                WormScreenshake = false;
            }

            if (!makeTimerWork)
            {
                LongShake = false;
            }
        }
    }

    public class ThornsPlayer : ModPlayer
    {
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Player.HasBuff(BuffID.Thorns))

            {
                npc.AddBuff(BuffID.Poisoned, 60); // Apply Poisoned buff to the enemy for 1 second (60 frames)
            }
        }
    }

    public class RealmModPlayer : ModPlayer
    {
        public bool marbleJustJumped;

        public bool BrassSetBonus = false;
        public bool MaxTeeth = false;
        public int BrassCD = 0;
        public int BrassShineCD = 0;

        public bool GoreToothBonus = false;
        public int GoreToothCD = 0;
        public int ShineGoreCD = 0;

        public bool OrchidBonus = false;
        public int cdOrchid = 0;

        public bool GreenNeck = false;
        public bool Overseer = false;
        public bool Rusty = false;

        public bool FallSpeed = false;
        public bool PiggySet = false;

        public int PiggySwing = 0;
        public int PorceDMG = 0;
        public int PorceWidth = 58;
        public int DMGPor = 0;
        public int PigSwings = 0;
        public int cd;

        private int coinFall = 0;
        private int coinFallAmount = 0;
        private bool hasStriken = false;

        public bool piggy = false;

        public float marbleJump = 0f;

        public override void ResetEffects()
        {
            GoreToothBonus = false;
            BrassSetBonus = false;
            OrchidBonus = false;
            Overseer = false;
            Rusty = false;
            GreenNeck = false;
            marbleJustJumped = false;

            FallSpeed = false;
            PiggySet = false;
            hasStriken = false;
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (BrassSetBonus)
            {
                if (Main.rand.Next(101) < 20)
                {
                    npc.AddBuff(ModContent.BuffType<Copperized>(), 240);
                }
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Main.LocalPlayer.HasBuff<BrassMight>())
            {
                r = 1f;
                g = 0.5f;
                b = 0f;
                a = 1f;
            }
            if (GoreToothBonus = true && Main.LocalPlayer.statLife <= Main.LocalPlayer.statLifeMax2 / 2)
            {
                r = 1f;
                g = 0.7f;
                b = 0.7f;
                a = 1f;
            }
        }

        /*public void DoubleTapEffects(int keyDir)
		{
			if (keyDir == (Main.ReversedUpDownArmorSetBonuses ? 1 : 0))
			{
				if( brassSet && !Player.HasBuff(ModContent.BuffType<BrassMight>()))
				{
                    Player.AddBuff(ModContent.BuffType<BrassMight>(), 500);
                }
            }
		}*/

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            Player p = Main.LocalPlayer;

            if (KeybindSystem.BrassActivation.JustPressed && !p.HasBuff<BrassMightCD>() && !p.HasBuff<BrassMight>())
            {
                for (int i = 0; i < 90; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(2.5f, 2.5f);
                    var dus = Dust.NewDustPerfect(p.Center, DustID.Copper, speed * 5f, Scale: 2.5f);
                    ;
                    dus.noGravity = true;
                }
                SoundEngine.PlaySound(SoundID.NPCHit43, p.Center);
                p.AddBuff(ModContent.BuffType<BrassMight>(), 600);
                Projectile.NewProjectile(p.GetSource_FromThis(), p.Center, (p.Center - Main.MouseWorld).SafeNormalize(Vector2.Zero) * -5f, ModContent.ProjectileType<BrassMissile>(), 30, 0f, Main.myPlayer);
            }

            if (FallSpeed == true)
            {
                if (p.controlDownHold)
                {
                    p.maxFallSpeed += 4f;
                }
            }
        }

        public override void PostUpdateMiscEffects()
        {
            Player.ManageSpecialBiomeVisuals("RealmOne:BloodSky", Main.bloodMoon);
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (Rusty == true)
            {
                return Main.rand.NextFloat() >= 0.25f;
            }

            return base.CanConsumeAmmo(weapon, ammo);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (GreenNeck)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && npc.Distance(Player.Center) < 300f && npc.lifeMax > 5 && !npc.friendly && !npc.boss)
                    {
                        npc.AddBuff(BuffID.ShadowFlame, 1000);
                    }
                }
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {
            if (Overseer && Main.rand.NextBool(3) && !target.friendly && hit.Crit && target.lifeMax > 10 && target.type != NPCID.TargetDummy)
            {
                Player.AddBuff(ModContent.BuffType<OverseerBuff>(), 400);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
        {
            if (Overseer && Main.rand.NextBool(3) && hit.Crit && !target.friendly && target.lifeMax > 10 && !target.SpawnedFromStatue && target.type != NPCID.TargetDummy)
            {
                Player.AddBuff(ModContent.BuffType<OverseerBuff>(), 400);
            }
        }

        public class BrightProjectilePlayer : ModPlayer
        {
            public bool brightProjectiles = false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (PiggySet == true)
            {
                if (Main.rand.Next(101) < 80 && coinFallAmount <= 0)
                {
                    coinFallAmount = 6;
                }
            }
        }

        public override void PreUpdate()
        {
            Player player = Main.LocalPlayer;

            if (player.GetModPlayer<RealmModPlayer>().cd > 1)
            {
                player.GetModPlayer<RealmModPlayer>().cd--;
            }

            if (Main.GameModeInfo.IsMasterMode)
            {
                if (Player.ZoneSkyHeight)
                {
                    Player.AddBuff(BuffID.Suffocation, 20);
                }

                if (Player.HasBuff(BuffID.Suffocation))
                {
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height), new Color(80, 150, 240, 140), "You're losing air!!", false, false);
                }
            }
        }

        public override void PostUpdate()
        {
            Player p = Main.LocalPlayer;

            if (GoreToothBonus)
            {
                if (p.statLife <= p.statLifeMax2 / 2)
                {
                    if (ShineGoreCD == 0)
                    {
                        ShineGoreCD = 5;
                        int d = Dust.NewDust(p.position, p.width, p.height, DustID.RedTorch);
                        Main.dust[d].scale = 1f;
                        Main.dust[d].velocity *= 1f;
                        Main.dust[d].noLight = false;
                    }
                    if (GoreToothCD == 0 && p.ownedProjectileCounts[ModContent.ProjectileType<GoretoothTooth>()] < 24 && MaxTeeth == false) //24
                    {
                        GoreToothCD = Main.rand.Next(8, 17);
                        int dmg = Main.rand.Next(18, 25);
                        if (p.HeldItem.DamageType == DamageClass.Summon)
                        {
                            if (p.HeldItem.damage > 20)
                            {
                                dmg = Main.rand.Next(p.HeldItem.damage / 2 + 18, p.HeldItem.damage / 2 + 10 + 7);
                            }
                        }
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(p.Center.X, p.Center.Y - 35), new Vector2(0, 0), ModContent.ProjectileType<GoretoothTooth>(), dmg, 2f, Main.myPlayer);
                    }
                    if (p.ownedProjectileCounts[ModContent.ProjectileType<GoretoothTooth>()] >= 23)
                    {
                        MaxTeeth = true;
                    }

                    if (GoreToothCD == 0 && MaxTeeth == true && p.ownedProjectileCounts[ModContent.ProjectileType<GoretoothTooth>()] < 24)
                    {
                        GoreToothCD = Main.rand.Next(280, 311);
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(p.Center.X, p.Center.Y - 35), new Vector2(0, 0), ModContent.ProjectileType<GoretoothTooth>(), Main.rand.Next(18, 25), 2f, Main.myPlayer);
                    }
                }
                else if (p.statLife > p.statLifeMax2 / 2)
                {
                    MaxTeeth = false;
                    GoreToothCD = 15;
                }
            }

            if (ShineGoreCD > 0)
            {
                ShineGoreCD--;
            }

            if (GoreToothCD > 0)
            {
                GoreToothCD--;
            }

            if (BrassCD > 0)
            {
                BrassCD--;
            }

            if (BrassShineCD > 0)
            {
                BrassShineCD--;
            }

            if (BrassSetBonus && BrassShineCD == 0)
            {
                BrassShineCD = 60;
                SparkleParticle sparkle = new(Color.White, 1, new Vector2(p.Center.X + Main.rand.Next(-50, 50), p.Center.Y + Main.rand.Next(-50, 50)), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);
                ParticleSystem.GenerateParticle(sparkle);
            }

            if (BrassSetBonus && BrassCD == 0 && !p.HasBuff<BrassMight>())
            {
                BrassCD = 9;
                int d = Dust.NewDust(p.position, p.width, p.height, DustID.CopperCoin);
                Main.dust[d].scale = 1f;
                Main.dust[d].velocity *= 1f;
                Main.dust[d].noLight = false;
            }
            else if (BrassSetBonus && BrassCD == 0 && p.HasBuff<BrassMight>())
            {
                BrassCD = 2;
                int d = Dust.NewDust(p.position, p.width, p.height, DustID.CopperCoin);
                Main.dust[d].scale = 2f;
                Main.dust[d].velocity *= 1f;
                Main.dust[d].noLight = false;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Vector2.Distance(p.Center, Main.npc[i].Center) > 150)
                {
                    if (Main.npc[i].color == Color.LightGreen)
                    {
                        Main.npc[i].color = Color.White;
                    }
                }
            }

            if (OrchidBonus == true)
            {
                if (cdOrchid > 0)
                {
                    cdOrchid--;
                }

                if (Main.rand.Next(200) < 3)
                {
                    Vector2 spawnLoc = new Vector2(p.Center.X + Main.rand.Next(-260, 260), p.Center.Y + Main.rand.Next(-260, -20));
                    Projectile.NewProjectile(p.GetSource_FromThis(), p.Center, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), ModContent.ProjectileType<ForestVengeanceProjec>(), 11, 5f, Main.myPlayer);
                }

                if (cdOrchid == 0)
                {
                    cdOrchid = 20;
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 spawnLoc = new Vector2(p.Center.X + Main.rand.Next(-260, 260), p.Center.Y + Main.rand.Next(-260, -20));
                        if (Collision.CanHit(spawnLoc, 1, 1, spawnLoc, 1, 1))
                        {
                            Gore.NewGorePerfect(p.GetSource_Death(), spawnLoc, new Vector2(0, 0), GoreID.TreeLeaf_Normal, 1f);
                        }
                    }
                }

                p.AddBuff(ModContent.BuffType<OrchidBonus>(), 1);
                if (p.ownedProjectileCounts[ModContent.ProjectileType<OrchidRingRing>()] < 1)
                {
                    Projectile.NewProjectile(p.GetSource_FromThis(), p.Center, new Vector2(0, 0), ModContent.ProjectileType<OrchidRingRing>(), 0, 0f, Main.myPlayer);
                }

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Vector2.Distance(p.Center, Main.npc[i].Center) < 300)
                    {
                        if (Main.npc[i].boss == false)
                        {
                            Main.npc[i].AddBuff(ModContent.BuffType<SlowLeaf>(), 5);
                        }
                    }
                }

                for (int i = 0; i < Main.maxItems; i++)
                {
                    if (Vector2.Distance(p.Center, Main.item[i].Center) < 300 && Main.item[i].type == ItemID.Heart)
                    {
                        Main.item[i].velocity = (p.Center - Main.item[i].Center).SafeNormalize(Vector2.Zero) * 5f;
                        int d = Dust.NewDust(Main.item[i].position, Main.item[i].width, Main.item[i].height, DustID.GreenTorch, Scale: 1.25f);
                        Main.dust[d].noGravity = true;
                    }
                    if (Vector2.Distance(p.Center, Main.item[i].Center) < 150 && Main.item[i].type == ItemID.Star)
                    {
                        Main.item[i].velocity = (p.Center - Main.item[i].Center).SafeNormalize(Vector2.Zero) * 5f;
                        int d = Dust.NewDust(Main.item[i].position, Main.item[i].width, Main.item[i].height, DustID.GreenTorch, Scale: 1.25f);
                        Main.dust[d].noGravity = true;
                    }
                }
            }

            if (PiggySet == true)
            {
                if (coinFall > 0)
                {
                    coinFall--;
                }

                if (coinFall == 0 && coinFallAmount > 0)
                {
                    coinFall = 5;
                    coinFallAmount--;
                    SoundEngine.PlaySound(SoundID.Item9, p.position);
                    Vector2 SpawnLoc = new Vector2(p.position.X - 128, p.position.Y - 900);
                    int select = Main.rand.Next(1, 5);
                    if (select == 1)
                    {
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(SpawnLoc.X + Main.rand.Next(1, 257), SpawnLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<PlatinumCoinFriendly>(), Main.rand.Next(20, 31), 6f, Main.myPlayer);
                    }
                    if (select == 2)
                    {
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(SpawnLoc.X + Main.rand.Next(1, 257), SpawnLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<GoldCoinFriendly>(), Main.rand.Next(20, 31), 6f, Main.myPlayer);
                    }
                    if (select == 3)
                    {
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(SpawnLoc.X + Main.rand.Next(1, 257), SpawnLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<SilverCoinFriendly>(), Main.rand.Next(20, 31), 6f, Main.myPlayer);
                    }
                    if (select == 4)
                    {
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(SpawnLoc.X + Main.rand.Next(1, 257), SpawnLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<CopperCoinFriendly>(), Main.rand.Next(20, 31), 6f, Main.myPlayer);
                    }
                }

                if (p.statLife <= p.statLifeMax2 / 1.65)
                {
                    if (!p.HasBuff<PiggyDebuff>() && hasStriken == false)
                    {
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 900), new Vector2(0, 14f), ModContent.ProjectileType<PiggyBankFalling>(), Main.rand.Next(90, 111), 9f, Main.myPlayer);
                        p.AddBuff(ModContent.BuffType<PiggyDebuff>(), 60 * 60);
                        hasStriken = true;
                    }
                }

                if (p.statLife > p.statLifeMax2 / 1.65f)
                {
                    if (hasStriken == true)
                    {
                        hasStriken = false;
                    }
                }
            }
        }

        public override void OnEnterWorld()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue($"[i:{ItemID.FallenStar}] Go and join the discord server for the mod!! [c/0000FF:discord.gg/vsBJ8PrmCh] [i:{ItemID.FallenStar}]"), 128, 200, 55);
            }
        }

        public override void OnRespawn()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("Death is only so fragile, yet you take advantage of it."), 218, 39, 44);
            }
        }

        public override void PlayerConnect()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("'Your acquaintance wants to feel distress as well I see'"), 64, 16, 227);
            }
        }

        public override void PlayerDisconnect()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("'Never wait a second longer or shorter, it will always drive the pain towards you'"), 210, 30, 30);
            }
        }

        public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("'Regeneratating is more natural and increases your cardiovascular immunity, avoid healing, you pussy'"), 210, 100, 175);
            }
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            return (IEnumerable<Item>)(object)new Item[2]
            {
                new Item(ModContent.ItemType<Suitcase>(), 1, 0),
                new Item(ModContent.ItemType<LovecraftPaper>(), 1, 0),
            };
        }
    }
}