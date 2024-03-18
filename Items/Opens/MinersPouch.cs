using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Opens
{
    public class MinersPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miner's Pouch");
            Tooltip.SetDefault("<right> for a little pouchful of mining necessities"
                + "\nBelow is the list of items you can get in the bag"
             + $"\n[i:{ItemID.CopperOre}][i:{ItemID.IronOre}][i:{ItemID.TinOre}][i:{ItemID.LeadOre}][i:{ItemID.GoldOre}][i:{ItemID.PlatinumOre}]"
             + $"\n[i:{ItemID.Bomb}][i:{ItemID.Dynamite}][i:{ItemID.Torch}][i:{ItemID.MiningPotion}][i:{ItemID.SpelunkerPotion}]");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.maxStack = 99;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemID.CopperOre, 4, 15, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.IronOre, 4, 15, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.SilverOre, 4, 15, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.TungstenOre, 4, 15, 20));

            itemLoot.Add(ItemDropRule.Common(ItemID.TinOre, 4, 15, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.LeadOre, 4, 15, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.GoldOre, 4, 15, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.PlatinumOre, 4, 15, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.Torch, 4, 20, 26));
            itemLoot.Add(ItemDropRule.Common(ItemID.MiningPotion, 4, 2, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.Bomb, 4, 8, 10));
            itemLoot.Add(ItemDropRule.Common(ItemID.Dynamite, 4, 4, 6));
            itemLoot.Add(ItemDropRule.Common(ItemID.SpelunkerPotion, 4, 2, 4));
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
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
                dust.scale = 0.5f;
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