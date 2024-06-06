using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Items.Weapons.PreHM.Classless;
using RealmOne.Projectiles.Other;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Others
{
    public class HeavenMagnet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heaven's Magnet"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Magnet in random loot from the heavens!!"
                + "\nHas a cooldown of 5 minutes");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        private int cooldownTime = 18000; // 30 seconds in frames (1 second = 60 frames)
        private int cooldownTimer = 0;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 60000;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.UseSound = SoundID.DD2_DarkMageHealImpact;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.shoot = ModContent.ProjectileType<LootBalloon>();
            Item.shootSpeed = 4f;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<MagnetBuff>()))
            {
                Main.NewText(Language.GetTextValue("You are already under the effects of the Heaven's Magnet"), 150, 243, 244);
            }
            else
            {
                Main.NewText(Language.GetTextValue($"\n[i:{ItemID.FallenStar}]The heaven has accepeted your wish, you have been granted an item![i:{ItemID.FallenStar}]"), 150, 243, 244);
            }
            return cooldownTimer == 0;
        }

        public override bool? UseItem(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                GenericGlowParticle particle = new(new Vector2(player.Center.X + Main.rand.Next(-30, 30), player.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), Color.NavajoWhite, 0.5f, 120);
                SparkleParticle sparkle = new(Color.LightYellow, 1, new Vector2(player.Center.X + Main.rand.Next(-30, 30), player.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);

                ParticleSystem.GenerateParticle(sparkle);
                ParticleSystem.GenerateParticle(particle);
            }
            player.AddBuff(ModContent.BuffType<MagnetBuff>(), 18000);

            // Start the cooldown
            cooldownTimer = cooldownTime;

            // Perform the item's functionality
            // Add your desired code here

            return true;
        }

        public override void UpdateInventory(Player player)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer--;
            }
        }

        public int spreadMax = 22;
        public int spreadMin = -20;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position,
           Vector2 velocity, int type,
           int damage, float knockback)
        {
            int numberProjectiles = 1;
            for (int index = 0; index < numberProjectiles; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)(player.position.X + player.width * 0.5 +
                             (Main.rand.Next(201) * -player.direction) + (Main.mouseX +
                                 Main.screenPosition.X - player.position.X)),
                    (float)(player.position.Y + player.height * 0.5 -
                             600.0));
                vector2_1.X = (float)((vector2_1.X + player.Center.X) / 2.0) +
                              Main.rand.Next(-200, 201);
                vector2_1.Y -= 100 * index;
                float num12 = Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if (num13 < 0.0) num13 *= -1f;
                if (num13 < 20.0) num13 = 20f;
                float num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
                float num15 = Item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + Main.rand.Next(spreadMin, spreadMax) * 0.02f; //Projectile Spread
                float SpeedY = num17 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(Terraria.Entity.GetSource_None(), vector2_1.X, vector2_1.Y, SpeedX, SpeedY, type, damage,
                                 knockback, Main.myPlayer, 0.0f, Main.rand.Next(1));
            }

            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch sB, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            for (int i = 0; i < 1; i++)
            {
                int num7 = 16;
                float num8 = (float)(Math.Cos(Main.GlobalTimeWrappedHourly % 2.4 / 2.4 * MathHelper.TwoPi) / 5 + 0.4);
                SpriteEffects spriteEffects = SpriteEffects.None;
                Texture2D texture = TextureAssets.Item[Item.type].Value;
                var vector2_3 = new Vector2(TextureAssets.Item[Item.type].Value.Width / 2, TextureAssets.Item[Item.type].Value.Height / 1 / 2);
                var color2 = new Color(249, 254, 159, 140);
                Rectangle r = TextureAssets.Item[Item.type].Value.Frame(1, 1, 0, 0);
                for (int index2 = 0; index2 < num7; ++index2)
                {
                    Color color3 = Item.GetAlpha(color2) * (0.65f - num8);
                    Main.spriteBatch.Draw(texture, position + new Vector2(3, 1), new Microsoft.Xna.Framework.Rectangle?(r), color3, 0f, vector2_3, Item.scale * .30f + num8, spriteEffects, 0.0f);
                }
            }

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "HeavenMagnet", "'Some would even say this is a gift from God!'")
            {
                OverrideColor = new Color(220, 230, 149)
            };
            tooltips.Add(line);
        }
    }

    //	}

    public class MagnetBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heaven's Magnet Buff");
            Description.SetDefault("Gift from God!");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] == 2 && player.whoAmI == Main.myPlayer)
            {
                if (Main.netMode == NetmodeID.Server)
                {
                    Main.NewText(Language.GetTextValue($"\n[i:{ItemID.FallenStar}]The heaven has accepeted your wish, you have been granted an item![i:{ItemID.FallenStar}]"), 150, 243, 244);
                }
            }
        }
    }

    internal class MagnetLoot : ModSystem
    {
        public override void PostWorldGen()
        {
            // Place some additional items in Frozen Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInWoodChests1 = { ItemType<HeavenMagnet>(), ItemType<HeavenMagnet>(), ItemType<HeavenMagnet>() };
            // This variable will help cycle through the items so that different Frozen Chests get different items
            int itemsToPlaceInWoodChestsChoice1 = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each).
            int itemsPlaced1 = 0;
            int maxItems1 = 40;
            // Loop over all the chests
            for (int chestIndex1 = 0; chestIndex1 < Main.maxChests; chestIndex1++)
            {
                Chest chest1 = Main.chest[chestIndex1];
                if (chest1 == null)
                {
                    continue;
                }
                Tile chestTile1 = Main.tile[chest1.x, chest1.y];
                // We need to check if the current chest is the Frozen Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Frozen Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Frozen Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Frozen_Chest
                if (chestTile1.TileType == TileID.Containers && chestTile1.TileFrameX == 13 * 36)
                {
                    // We have found a Frozen Chest
                    // If we don't want to add one of the items to every Frozen Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(3))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex1 = 0; inventoryIndex1 < Chest.maxItems; inventoryIndex1++)
                    {
                        if (chest1.item[inventoryIndex1].type == ItemID.None)
                        {
                            // Place the item
                            chest1.item[inventoryIndex1].SetDefaults(itemsToPlaceInWoodChests1[itemsToPlaceInWoodChestsChoice1]);
                            // Decide on the next item that will be placed.

                            itemsToPlaceInWoodChestsChoice1 = (itemsToPlaceInWoodChestsChoice1 + 1) % itemsToPlaceInWoodChests1.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInFrozenChests));
                            itemsPlaced1++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced1 >= maxItems1)
                {
                    break;
                }
            }
        }
    }
}