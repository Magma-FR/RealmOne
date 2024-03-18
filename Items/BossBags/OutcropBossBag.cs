/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Accessories;
using RealmOne.Items.Others;
using RealmOne.Items.Potions;
using RealmOne.Items.Weapons.PreHM.BossDrops.OutcastDrops;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.BossBags
{
    public class OutcropBossBag : ModItem
    {
        // public override int BossBagNPC => ModContent.NPCType<MegaSquirmHead>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag (The Outcrop Outcast)");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}"
                + "\n'Untie a bag of pure evolution and plantation'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;

            ItemID.Sets.BossBag[Type] = true; // This set is one that every boss bag should have, it, for example, lets our boss bag drop dev armor..
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true; // ..But this set ensures that dev armor will only be dropped on special world seeds, since that's the behavior of pre-hardmode boss bags.
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.rare = 11;
            Item.consumable = true;
            Item.maxStack = 99;
            Item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemID.MudBlock, 1, 20, 30));
            itemLoot.Add(ItemDropRule.Common(ItemID.GrassSeeds, 1, 30, 50));
            itemLoot.Add(ItemDropRule.Common(ItemID.Wood, 1, 30, 50));
            itemLoot.Add(ItemDropRule.Common(ItemID.StaffofRegrowth, 4, 1, 1));

            //        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BotanicLogLauncher>(), 3, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FoliageFury>(), 3, 1, 1));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheOutcastsOverseer>(), 3, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthEmerald>(), 3, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BarrenBrew>(), 3, 4, 6));
        }

        public override Color? GetAlpha(Color lightColor)
        {
            // Makes sure the dropped bag is always visible
            return Color.Lerp(lightColor, Color.LimeGreen, 0.5f);
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
            Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
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
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(181, 128, 177, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(218, 122, 147, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.ForestGreen.ToVector3() * 0.4f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.Worm, velocity);
                dust.scale = 0.6f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }
    }
}

*/