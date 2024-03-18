using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.NPCs.TownNPC;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc
{
    public class SuperWheat : ModItem
    {
        public override string Texture => "RealmOne/Items/Misc/Wheat";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wheat"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Hand picked from the fields, ready to be baked'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 50;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Expert;
            Item.consumable = true;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override bool? UseItem(Player player)
        {
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<FarmerSlime>());
            return true;
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

            line = new TooltipLine(Mod, "SuperWheat", "Smells like beer!")
            {
                OverrideColor = new Color(220, 230, 149)
            };
            tooltips.Add(line);
        }
    }
}