/*using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Summoner
{
    public class TwinklingTwig : ModItem
    {
        private int shotCount;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Twinkling Twig"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Summons an Enchanted Nightcrawler to wriggle and starstruck your enemies!!");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "TwinklingTwig", "'Sent from the birth of a nebula'")
            {
                OverrideColor = new Color(250, 100, 168)
            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "TwinklingTwig", "'Right click to shoot a Hallow Star from the wand, consuming double the mana '")
            {
                OverrideColor = new Color(118, 215, 196)
            };
            tooltips.Add(line);
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Summon;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 16;
            Item.mana = 5;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.DD2_DarkMageCastHeal;
            Item.autoReuse = true;

            Item.shoot = ProjectileID.StarCannonStar;
            Item.shootSpeed = 44f;
            Item.noMelee = true;
        }

        public override bool OnPickup(Player player)
        {
            bool pickupText = false;
            for (int i = 0; i < 50; i++)
                if (player.inventory[i].type == ItemID.None && !pickupText)
                {
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(210, 120, 150, 255), "...Up above a world so high...", false, false);
                    pickupText = true;
                }

            SoundEngine.PlaySound(rorAudio.TwinkleBell);
            return true;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(234, 129, 178, 105), "Twinkle", false, false);
        }

        /*  public override void AddRecipes()
          {
              CreateRecipe()
              .AddIngredient(ItemID.FallenStar, 15)

              .AddIngredient(ItemID.EnchantedNightcrawler, 10)
              .AddTile(TileID.Anvils)
              .Register();
          }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(6, 0);
            return offset;
        }
    }
}
*/