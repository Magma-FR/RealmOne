/*using RealmOne.Common.Systems;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class Peni : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Penacillin");
            Tooltip.SetDefault($"[c/00FF00:~Sub Accessory~]"
           + "\n10+HP"
           + "\nImmunity to poison, venom, bleeding" +
           "\nWhen you are hit you regen health twice as fast" +
           "---------------------"
            + "\nYou can consume the penacillin, making the buffs 2x as strong"
            + "\nTherefore: 20+HP");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.value = 16000;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.value = 500;
            Item.UseSound = SoundID.Item1;

            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.width = 24;
                Item.height = 24;
                Item.accessory = true;
                Item.value = 16000;
                Item.rare = ItemRarityID.Green;
                Item.useTime = 25;
                Item.useAnimation = 25;
                Item.value = 500;
                Item.rare = ItemRarityID.Green;
                Item.UseSound = SoundID.Item1;
                Item.useStyle = ItemUseStyleID.Swing;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.DrinkLiquid;
                Item.useTime = 25;
                Item.useAnimation = 25;
                Item.shoot = ProjectileID.None;
                Item.value = 500;
                Item.rare = ItemRarityID.Green;
                Item.UseSound = rorAudio.LightbulbShine;
                if (Main.rand.NextBool(1))
                    player.AddBuff(BuffID.Ironskin, 800);
            }
            return base.CanUseItem(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 10;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Bleeding] = true;
        }
    }
}*/