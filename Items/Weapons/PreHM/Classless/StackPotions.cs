using Microsoft.Xna.Framework;
using RealmOne.Items.Opens;
using RealmOne.Projectiles.Throwing;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Classless
{
    public class StackPotions : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stack Of Potions"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Throws a bundle of different potions"
                + "\nWhen the bundle is smashed, it has a chance of dropping a random potion on drop"
                + "\nRight Click to Drink a random potion");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Generic;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1f;
            Item.value = 20000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.shoot = ModContent.ProjectileType<StackPotionsProj>();
            Item.shootSpeed = 12f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.DrinkLiquid;
                Item.useTime = 48;
                Item.useAnimation = 48;

                Item.width = 20;
                Item.height = 20;
                Item.maxStack = 99;
                Item.shoot = ProjectileID.None;
                Item.value = 500;
                Item.rare = ItemRarityID.Green;
                Item.consumable = true;

                Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/LightbulbShine");

                if (Main.rand.NextBool(5))
                    player.AddBuff(BuffID.Ironskin, 800);

                if (Main.rand.NextBool(5))
                    player.AddBuff(BuffID.Swiftness, 800);

                if (Main.rand.NextBool(5))
                    player.AddBuff(BuffID.Regeneration, 800);

                if (Main.rand.NextBool(5))
                    player.AddBuff(BuffID.Endurance, 800);

                if (Main.rand.NextBool(5))
                    player.AddBuff(BuffID.ManaRegeneration, 800);

                if (Main.rand.NextBool(5))
                    player.AddBuff(BuffID.MagicPower, 800);

                if (Main.rand.NextBool(5))
                    player.AddBuff(BuffID.Spelunker, 800);

                if (Main.rand.NextBool(5))
                    player.AddBuff(BuffID.Shine, 800);

                if (Main.rand.NextBool(5))
                    player.AddBuff(BuffID.NightOwl, 800);
            }
            else
            {
                Item.damage = 20;
                Item.DamageType = DamageClass.Generic;
                Item.width = 24;
                Item.height = 24;
                Item.useTime = 40;
                Item.useAnimation = 40;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.knockBack = 1f;
                Item.value = 20000;
                Item.rare = ItemRarityID.Blue;
                Item.UseSound = SoundID.Item1;
                Item.autoReuse = true;
                Item.maxStack = 999;
                Item.shoot = ModContent.ProjectileType<StackPotionsProj>();
                Item.shootSpeed = 12f;
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.consumable = true;
            }

            return base.CanUseItem(player);
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Pink.ToVector3() * 1f);
            Lighting.AddLight(Item.Right, Color.LightGreen.ToVector3() * 1f);
            Lighting.AddLight(Item.Left, Color.Yellow.ToVector3() * 1f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.PinkTorch, velocity);
                dust.scale = 0.5f;
                dust.fadeIn = 0.4f;
                dust.noGravity = true;
                dust.noLight = false;
                dust.alpha = 0;

                var dustright = Dust.NewDustPerfect(Item.Right + direction * distance, DustID.GreenTorch, velocity);
                dustright.scale = 0.5f;
                dustright.fadeIn = 0.4f;
                dustright.noGravity = true;
                dustright.noLight = false;
                dustright.alpha = 0;

                var dustleft = Dust.NewDustPerfect(Item.Right + direction * distance, DustID.YellowTorch, velocity);
                dustleft.scale = 0.5f;
                dustleft.fadeIn = 0.4f;
                dustleft.noGravity = true;
                dustleft.noLight = false;
                dustleft.alpha = 0;
            }
        }
    }

    internal class StackLoot : ModSystem
    {
        public override void PostWorldGen()
        {
            // Place some additional items in Frozen Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInWoodChests1 = { ItemType<StackPotions>(), ItemType<StackPotions>(), ItemType<StackPotions>() };
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
                if (chestTile1.TileType == TileID.Containers && chestTile1.TileFrameX == 1 * 36)
                {
                    // We have found a Frozen Chest
                    // If we don't want to add one of the items to every Frozen Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(2))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex1 = 0; inventoryIndex1 < Chest.maxItems; inventoryIndex1++)
                    {
                        if (chest1.item[inventoryIndex1].type == ItemID.None)
                        {
                            // Place the item
                            chest1.item[inventoryIndex1].SetDefaults(itemsToPlaceInWoodChests1[itemsToPlaceInWoodChestsChoice1]);
                            // Decide on the next item that will be placed.
                            chest1.item[inventoryIndex1].stack = WorldGen.genRand.Next(10, 18);

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