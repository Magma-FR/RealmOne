using Microsoft.Xna.Framework;
using RealmOne.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class BrassHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Helmet");
            Tooltip.SetDefault("6% increased melee damage ");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3; //
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 0.6f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BrassBody>() && legs.type == ModContent.ItemType<BrassLegs>();
        }

        private int Watertimer = 0;

        public override void UpdateArmorSet(Player player)
        {
            //      string tapDir = Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN");
            player.setBonus = "Double tap UP to gain Brass Might which increases the players defense by 10+ but 14% decreased movement & running speed\n10 second cooldown";
            Watertimer++;
            player.GetModPlayer<BrassSetBonus>().SpecialSetBonus = true;

            if (Watertimer == 9)
            {
                int d = Dust.NewDust(player.position, player.width, player.height, DustID.CopperCoin);
                Main.dust[d].scale = 1f;
                Main.dust[d].velocity *= 0.5f;
                Main.dust[d].noLight = false;

                Watertimer = 0;
            }

            player.statDefense += 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(Mod, "BrassIngot", 4)
            .AddTile(TileID.Furnaces)
            .Register();
        }
    }

    public class BrassSetBonus : ModPlayer
    {
        public const int PressUp = 1;

        public const int Cooldown = 1400;
        public const int Duration = 30;

        public const float Thing = 10f;

        public int Dir = -1;

        public bool SpecialSetBonus;
        public int Delay = 0;
        public int Timer = 0;

        public override void ResetEffects()
        {
            SpecialSetBonus = false;

            if (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[PressUp] < 15)
            {
                Dir = PressUp;
            }
            else
            {
                Dir = -1;
            }
        }

        public override void PreUpdateMovement()//this is were the code gets very sketchy, this is the only way i could get it to work cause me dumb, please fix if you know how to
        {//the code may be sketch, however i game it works
            if (CanUseDash() && Dir != -1 && Delay == 0)//this is stupic im sorry
            {
                switch (Dir)
                {
                    case PressUp when Player.velocity.Y > -Thing:
                        {
                            Player.AddBuff(ModContent.BuffType<BrassMight>(), 750);
                            SoundEngine.PlaySound(SoundID.MaxMana, Player.position);

                            for (int i = 0; i < 80; i++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(3f, 3f);
                                var d = Dust.NewDustPerfect(Player.Center, DustID.OrangeTorch, speed * 5, Scale: 3f);
                                ;
                                d.noGravity = true;
                            }
                            break;
                        }

                    default:
                        return;
                }

                Delay = Cooldown;
                Timer = Duration;
            }

            if (Delay > 0)
                Delay--;

            if (Timer > 0)
            {
                Timer--;
            }
        }

        private bool CanUseDash()
        {
            return SpecialSetBonus

                && !Player.mount.Active; //because i dont like mounts, plus because if the sketchy ass code i used, you need this or els it breaks, but lets go woth the first anser
        }
    }
}