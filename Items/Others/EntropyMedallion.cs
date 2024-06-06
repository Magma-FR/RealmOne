/*using Microsoft.Xna.Framework.Input;
using RealmOne.Common.Systems;
using RealmOne.Items.Accessories;
using RealmOne.Projectiles.Other;
using RealmOne.RealmPlayer;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Others
{
    public class EntropyMedallion : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.useAnimation = 100;
            Item.useTime = 100;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = 90000;
            Item.rare = ItemRarityID.Master;
            Item.UseSound = rorAudio.TheIdol;
            Item.autoReuse = false;
            Item.masterOnly = true;
            Item.scale = 0.9f;
            Item.shoot = ModContent.ProjectileType<IdolProj>();
            Item.shootSpeed = 0.5f;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var tooltip = tooltips.Where((line => line.Name == "Tooltip0")).FirstOrDefault();
            tooltip.Text = Main.keyState.IsKeyDown(Keys.LeftShift) ?
                "He always knew there was someone stupid enough to use this'"
                + "\n'Only useable for the one that desires utter control of the universe'"
                + "\nOnly available in Master Mode"
                + "\n'Changes the way you play Terraria'"
                + "\nMakes enemies way more stronger (No AI Changes)"
                + "\n'Bosses are now as strong as Cthulhu originally deemed them to be'"
                + "\n Healing potions take longer to re consume them"
                + "\nBetter loot and exclusive powerful drops"
                + "\nShop prices are as valuable as Lay's Chips LUL"
                + "\nRealms are now more difficult and the enemies of each realm are harder"
                + "\nTraps and fall damage now do ridiculous amount of damage"
                + "\nLife Crystals only heal for 10 health now LMFAO HEEHEEHAW"
                + "\nLovecraftian Bosses are universally destructive" :

                "Toggles Transcendental Entropy Mode."
               + "\nPress [c/22ff22:SHIFT to reveal info]";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EmptyLocket>(), 1);
            recipe.Register();
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<Screenshake>().LongShake = true;
            player.AddBuff(BuffID.Obstructed, 600);
            player.AddBuff(BuffID.Darkness, 600);

            return true;
        }
    }
}*/