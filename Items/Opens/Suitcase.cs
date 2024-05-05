using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Accessories;
using RealmOne.Items.Ammo;
using RealmOne.Items.Weapons.Melee;
using RealmOne.Items.Weapons.PreHM.Crossbows;
using RealmOne.RealmPlayer;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Opens
{
    public class Suitcase : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lovecraft's Rift Essentials");
            Tooltip.SetDefault("<right> to crack open H.P Lovecraft's Rift Essentials"

                + "\nBelow are the Weapons you get from it!(You also get other necessities!)"

             + $"\n[i:{ModContent.ItemType<DualWieldCrossbows>()}]"
                          + $"\n[i:{ModContent.ItemType<GildedGladius>()}][i:{ModContent.ItemType<EmptyLocket>()}]");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Expert;
            Item.consumable = true;
            Item.maxStack = 1;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int CarrotGore1 = Mod.Find<ModGore>("Sgore1").Type;
                int CarrotGore2 = Mod.Find<ModGore>("Sgore2").Type;
                int CarrotGore3 = Mod.Find<ModGore>("Sgore3").Type;

                player.GetModPlayer<Screenshake>().WormScreenshake = true;

                if (Main.netMode != NetmodeID.Server)
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(255, 198, 125, 255), "Your Journey Begins!", false, false);
                IEntitySource entitySource = Item.GetSource_Death();

                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(entitySource, player.position, new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), CarrotGore1);
                    Gore.NewGore(entitySource, player.position, new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), CarrotGore2);
                    Gore.NewGore(entitySource, player.position, new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), CarrotGore3);
                }
            }
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodenArrow, 1, 50, 50));
            itemLoot.Add(ItemDropRule.Common(ItemID.RopeCoil, 1, 15, 15));
            itemLoot.Add(ItemDropRule.Common(ItemID.Torch, 1, 50, 50));
            itemLoot.Add(ItemDropRule.Common(ItemID.MiningPotion, 1, 5, 5));
            itemLoot.Add(ItemDropRule.Common(ItemID.Bomb, 1, 15, 15));
            itemLoot.Add(ItemDropRule.Common(ItemID.SpelunkerPotion, 1, 5, 5));
            itemLoot.Add(ItemDropRule.Common(ItemID.ManaCrystal, 1, 2, 2));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DualWieldCrossbows>(), 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GildedGladius>(), 1, 1, 1));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmptyLocket>(), 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Food.ForestTea>(), 1, 1, 1));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RustedBullets>(), 1, 50, 50));
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            else
                frame = texture.Frame();

            Vector2 frameOrigin = frame.Size() / 2f;
            var offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
                time = 2f - time;

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(118, 240, 209, 70), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(196, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Teal.ToVector3() * 0.4f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.RainbowMk2, velocity);
                dust.scale = 0.9f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "Suitcase", "You gotta frickin smash the thing for it to open!!!")
            {
                OverrideColor = new Color(107, 234, 186)
            };
            tooltips.Add(line);
        }
    }
}

//   IItemDropRule[] oreTypes = new IItemDropRule[] {
//      ItemDropRule.Common(ItemID.CopperOre, 1, 15, 20),
//   ItemDropRule.Common(ItemID.Bomb, 1, 8, 10),
//   ItemDropRule.Common(ItemID.IronOre, 1, 15, 20),
//   ItemDropRule.Common(ItemID.TinOre, 1, 15, 20)
//   ItemDropRule.Common(ItemID.LeadOre, 1, 15, 20),
//   ItemDropRule.Common(ItemID.GoldOre, 1, 15, 20),
//   ItemDropRule.Common(ItemID.PlatinumOre, 1, 15, 20),
//    ItemDropRule.Common(ItemID.Torch, 1, 20, 26),
//   ItemDropRule.Common(ItemID.MiningPotion, 1, 2, 3),
//  ItemDropRule.Common(ItemID.Dynamite, 1, 8, 10),
// ItemDropRule.Common(ItemID.SpelunkerPotion, 1, 2, 3),

//  ItemDropRule.Common(ModContent.ItemType<Items.GizmoScrap>(), 1, 3, 4),
// ItemDropRule.Common(ModContent.ItemType<Items.ScavengerSteel>(), 1, 3, 4),