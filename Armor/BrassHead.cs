using Microsoft.Xna.Framework;
using RealmOne.Buffs;
using RealmOne.Common.Systems;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.GameContent.UI.Elements;
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

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = $"+2 Defense\nEnemies that collide with you have a chance to get [c/C87C00:Copperized]\nPressing [c/C1C1C1:{KeybindSystem.BrassActivation.GetAssignedKeys()[0]}] causes you to encase yourself in another layer of brass that gives:\n[c/9BFF01:+4 life regen]\n[c/9BFF01:+20 max health]\n[c/9BFF01:+8 defense]\n[c/F00000:-50% movement speed]\nWhen pressed, also conjure a [c/C87C00:Brass Missile] that seeks out for your cursor\nUpon reaching your cursor or running out of time, it drops down like a bomb and explodes upon any impact\nUpon exploding, it inflicts [c/C87C00:Copperized] to all hit enemies\n[c/C87C00:Copperized]:\nEnemies get turned into copper for a short while\nWhile [c/C87C00:Copperized], enemies conduct electricity better\nThis causes [c/A6FEFF:Impact Weapons] to gain a special effect against [c/C87C00:Copperized] enemies";
            // change X to whatever hotkey the player has set it to
            player.statDefense += 2;
            player.GetModPlayer<RealmModPlayer>().BrassSetBonus = true;
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