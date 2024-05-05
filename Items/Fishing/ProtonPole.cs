using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.RealmPlayer;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Fishing
{
    public class ProtonPole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Proton Fishing Pole");

            Tooltip.SetDefault("Throws an electric bobber.\n" +
                "On right click you can explode the bobber into slow moving electric sparks"
                + "\n'Somewhat vulnerable to water!'");
            ItemID.Sets.CanFishInLava[Item.type] = false;
            ItemGlowy.AddGlowMask(Item.type, "RealmOne/Items/Fishing/ProtonPole_Glow");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ImpactTech>(10)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Blue;
            Item.CloneDefaults(ItemID.ReinforcedFishingPole);
            Item.damage = 10;
            Item.fishingPole = 18; // Sets the poles fishing power
            Item.shootSpeed = 15f; // Sets the speed in which the bobbers are launched. Wooden Fishing Pole is 9f and Golden Fishing Rod is 17f.
            Item.shoot = ModContent.ProjectileType<ProtonBobber>(); // The Bobber projectile.
        }

        // NOTE: Only triggers through the hotbar, not if you hold the item by hand outside of the inventory.
        public override void HoldItem(Player player)
        {
            player.accFishingLine = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int bobberAmount = Main.rand.Next(1, 1);
            float spreadAmount = 25f; // how much the different bobbers are spread out.

            for (int index = 0; index < bobberAmount; ++index)
            {
                Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f, Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f);

                // Generate new proton bobber
                Projectile.NewProjectile(source, position, bobberSpeed, type, 10, 0f, player.whoAmI);
            }

            return false;
        }
    }

    internal class ProtonBobber : ModProjectile
    {
        public static readonly Color[] PossibleLineColors = new Color[2] {
            new Color(110, 200, 252), // A white color
			new Color(40, 40, 218) // A blue color
		};

        // This holds the index of the fishing line color in the PossibleLineColors array.
        private int fishingLineColorIndex;

        private Color FishingLineColor => PossibleLineColors[fishingLineColorIndex];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Proton Bobber");
        }

        public override void SetDefaults()
        {
            // Projectile.netImportant = true;
            Projectile.CloneDefaults(ProjectileID.BobberReinforced);
            Projectile.damage = 10;
            DrawOriginOffsetY = -8; // Adjusts the draw position
        }

        public override void OnSpawn(IEntitySource source)
        {
            // Decide color of the pole by getting the index of a random entry from the PossibleLineColors array.
            fishingLineColorIndex = (byte)Main.rand.Next(PossibleLineColors.Length);
        }

        // What if we want to randomize the line color
        public override void AI()
        {
            Projectile.damage = 10;
            if (Main.mouseRight && Main.myPlayer == Projectile.owner)

                Projectile.Kill();
            if (!Main.dedServ)
            {
                // Create some light based on the color of the line.
                Lighting.AddLight(Projectile.Center, FishingLineColor.ToVector3());
                Lighting.AddLight(Projectile.Center, Color.Cyan.ToVector3() * 1.5f);

                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 1f);

                //   Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.7f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("ProtonBobberGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("ProtonBobberGore2").Type, 1f);
            SoundEngine.PlaySound(rorAudio.Proton);
            for (float i = 0; i <= 3f; i += Main.rand.NextFloat(0.5f, 3))
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, i.ToRotationVector2() * Main.rand.NextFloat(), ProjectileID.ThunderStaffShot,

                    (int)(Projectile.damage * 1.5f), Projectile.knockBack, Projectile.owner);
            Projectile.velocity *= 5;
            Projectile.damage = 10;
        }

        public override void ModifyFishingLine(ref Vector2 lineOriginOffset, ref Color lineColor)/* tModPorter Note: Removed. Use ModItem.ModifyFishingLine */
        {
            lineOriginOffset = new Vector2(26, -20);
            // Sets the fishing line's color. Note that this will be overridden by the colored string accessories.
            lineColor = FishingLineColor;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)fishingLineColorIndex);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            fishingLineColorIndex = reader.ReadByte();
        }
    }
}