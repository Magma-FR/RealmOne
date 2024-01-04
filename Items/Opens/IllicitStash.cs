using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc.Ores;
using RealmOne.Rarities;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Opens
{
    public class IllicitStash : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Illicit Stash");
            Tooltip.SetDefault("<right> to open up a bag of risky items"
                + "\nGuaranteed chance of getting something, but something you're not looking for"
                + "\n'You get what you get and don't get upset'");

            //  + $"\n[i:{ModContent.ItemType<DualWieldCrossbows>()}][i:{ModContent.ItemType<DumLightbulb>()}]"
            //        + $"\n[i:{ModContent.ItemType<ShatteredGemBlade>()}][i:{ModContent.ItemType<EmptyLocket>()}]");

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ModContent.RarityType<ModRarities>();
            Item.consumable = true;
            Item.maxStack = 30;
            Item.value = Item.buyPrice(gold: 15);

        }
        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodenArrow, 3, 18, 30));
            itemLoot.Add(ItemDropRule.Common(ItemID.CopperCoin, 3, 10, 40));
            itemLoot.Add(ItemDropRule.Common(ItemID.SilverCoin, 5, 10, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 8, 4, 10));
            itemLoot.Add(ItemDropRule.Common(ItemID.PlatinumCoin, 16, 1, 2));
            itemLoot.Add(ItemDropRule.Common(ItemID.DirtBlock, 3, 8, 15));
            itemLoot.Add(ItemDropRule.Common(ItemID.StoneBlock, 3, 8, 15));
            itemLoot.Add(ItemDropRule.Common(ItemID.Leather, 5, 4, 6));
            itemLoot.Add(ItemDropRule.Common(ItemID.Cobweb, 5, 8, 14));
            itemLoot.Add(ItemDropRule.Common(ItemID.Mushroom, 3, 5, 7));
            itemLoot.Add(ItemDropRule.Common(ItemID.Wood, 3, 15, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.MarshmallowonaStick, 15, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.WandofSparking, 15, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodenBoomerang, 15, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.LuckyHorseshoe, 18, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Gel, 3, 10, 18));
            itemLoot.Add(ItemDropRule.Common(ItemID.HermesBoots, 18, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Torch, 3, 16, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.AntlionMandible, 5, 3, 5));
            itemLoot.Add(ItemDropRule.Common(ItemID.Apple, 6, 2, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.Peach, 6, 2, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.Banana, 6, 2, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.Bomb, 6, 5, 9));
            itemLoot.Add(ItemDropRule.Common(ItemID.Grenade, 6, 10, 15));
            itemLoot.Add(ItemDropRule.Common(ItemID.Dynamite, 7, 4, 5));
            itemLoot.Add(ItemDropRule.Common(ItemID.FrogLeg, 18, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.GoldWatch, 17, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.PlatinumWatch, 17, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.RodofDiscord, 90, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Rope, 3, 16, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.Spear, 15, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Stinger, 6, 3, 5));
            itemLoot.Add(ItemDropRule.Common(ItemID.JungleSpores, 6, 3, 5));

            itemLoot.Add(ItemDropRule.Common(ItemID.Sunflower, 6, 2, 3));
            itemLoot.Add(ItemDropRule.Common(ItemID.Sunglasses, 9, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Goggles, 9, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Shackle, 12, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Shuriken, 3, 16, 26));
            itemLoot.Add(ItemDropRule.Common(ItemID.Starfury, 19, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Chain, 4, 6, 8));
            itemLoot.Add(ItemDropRule.Common(ItemID.IronBar, 6, 6, 10));
            itemLoot.Add(ItemDropRule.Common(ItemID.LeadBar, 6, 6, 10));
            itemLoot.Add(ItemDropRule.Common(ItemID.GoldBar, 6, 6, 10));
            itemLoot.Add(ItemDropRule.Common(ItemID.PlatinumBar, 6, 6, 10));
            itemLoot.Add(ItemDropRule.Common(ItemID.Campfire, 10, 1, 1));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrassNuggets>(), 6, 8, 10));
          

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
            Lighting.AddLight(Item.Center, Color.Gold.ToVector3() * 0.4f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.GoldCoin, velocity);
                dust.scale = 0.9f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
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
