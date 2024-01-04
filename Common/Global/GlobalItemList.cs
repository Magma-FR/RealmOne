using RealmOne.Items.Misc.Bars;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace RealmOne.Common.Global
{
    public class GlobalItemList : GlobalItem
    {


        //remove the tooltip
        //   tooltips.RemoveAll(x => x.Name == "Tooltip0" && x.mod == "Terraria");
        //actually, the hood's old statics is increased damage and crit chance by 10%.
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.TitanPotion)
            {
                TooltipLine line = tooltips.FirstOrDefault(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
                if (line != null)
                    line.Text = "10% increased melee weapon speed and knockback";

            }


            if (item.type == ItemID.ThornsPotion)
            {
                TooltipLine line = tooltips.FirstOrDefault(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
                if (line != null)
                    line.Text = "Enemies are poisoned when touching you" + "\nYour weapons also inflict poisoned";

            }
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (item.type == ItemID.Shackle)
                player.GetKnockback(DamageClass.Generic) += 0.10f;
        }

        public override void SetDefaults(Item item)
        {

            //Pre-Hardmode Accessories
            if (item.type == ItemID.CobaltShield) item.defense = 3;
            if (item.type == ItemID.ObsidianShield) item.defense = 5;
            if (item.type == ItemID.AnkhShield) item.defense = 8;
            if (item.type == ItemID.BandofRegeneration) item.lifeRegen += 8;
            if (item.type == ItemID.BandofStarpower) item.manaIncrease += 200;
            if (item.type == ItemID.Shackle) item.defense = 2;


            if (item.type == ItemID.NimbusRod) item.DamageType = DamageClass.Summon;
            if (item.type == ItemID.CrimsonRod) item.DamageType = DamageClass.Summon;

            //Pre-Hardmode Misc
            if (item.type == ItemID.Bomb) item.damage = 100;
            if (item.type == ItemID.Bomb) item.useTime = 25;
            if (item.type == ItemID.StickyBomb) item.damage = 100;
            if (item.type == ItemID.StickyBomb) item.useTime = 25;
            if (item.type == ItemID.Dynamite) item.damage = 250;
            if (item.type == ItemID.Dynamite) item.useTime = 40;
            if (item.type == ItemID.StickyDynamite) item.damage = 250;
            if (item.type == ItemID.StickyDynamite) item.useTime = 40;
            if (item.type == ItemID.BombFish) item.damage = 100;
            if (item.type == ItemID.BombFish) item.useTime = 25;
            if (item.type == ItemID.BouncyBomb) item.damage = 100;
            if (item.type == ItemID.BouncyBomb) item.useTime = 25;
            if (item.type == ItemID.BouncyDynamite) item.damage = 250;
            if (item.type == ItemID.BouncyDynamite) item.useTime = 40;
            if (item.type == ItemID.DirtBomb) item.damage = 100;
            if (item.type == ItemID.DirtBomb) item.useTime = 25;
            if (item.type == ItemID.DirtStickyBomb) item.damage = 100;
            if (item.type == ItemID.DirtStickyBomb) item.useTime = 25;
            if (item.type == ItemID.WetBomb) item.damage = 100;
            if (item.type == ItemID.WetBomb) item.useTime = 25;
            if (item.type == ItemID.DryBomb) item.damage = 100;
            if (item.type == ItemID.DryBomb) item.useTime = 25;
            if (item.type == ItemID.ScarabBomb) item.damage = 100;
            if (item.type == ItemID.ScarabBomb) item.useTime = 25;
            if (item.type == ItemID.HoneyBomb) item.damage = 100;
            if (item.type == ItemID.HoneyBomb) item.useTime = 25;
            if (item.type == ItemID.LavaBomb) item.damage = 100;
            if (item.type == ItemID.LavaBomb) item.useTime = 25;







            //Pre-Hardmode Weapons
            if (item.type == ItemID.Starfury) item.damage = 26;
            if (item.type == ItemID.Starfury) item.useTime = 18;
            if (item.type == ItemID.CopperShortsword) item.damage = 8;
            if (item.type == ItemID.CopperShortsword) item.useTime = 9;
            if (item.type == ItemID.BugNet) item.damage = 10;
            if (item.type == ItemID.GoldenBugNet) item.damage = 30;
            if (item.type == ItemID.FireproofBugNet) item.damage = 20;
            if (item.type == ItemID.FireproofBugNet) item.buffType = BuffID.OnFire;
            if (item.type == ItemID.CrimsonRod) item.damage = 17;
            if (item.type == ItemID.CrimsonRod) item.mana = 1;

            if (item.type == ItemID.EnchantedBoomerang) item.damage = 26;
            if (item.type == ItemID.EnchantedBoomerang) item.useTime = 12;
            if (item.type == ItemID.EnchantedBoomerang) item.shootSpeed = 18;

            if (item.type == ItemID.WoodenBoomerang) item.damage = 16;
            if (item.type == ItemID.WoodenBoomerang) item.useTime = 13;
            if (item.type == ItemID.WoodenBoomerang) item.shootSpeed = 12;



            if (item.type == ItemID.IceBoomerang) item.damage = 22;
            if (item.type == ItemID.IceBoomerang) item.useTime = 12;
            if (item.type == ItemID.IceBoomerang) item.shootSpeed = 15;

            if (item.type == ItemID.Flamarang) item.damage = 45;
            if (item.type == ItemID.Flamarang) item.useTime = 11;
            if (item.type == ItemID.Flamarang) item.shootSpeed = 19;



            if (item.type == ItemID.LightsBane) item.damage = 30;
            if (item.type == ItemID.LightsBane) item.useTime = 15;


            if (item.type == ItemID.HornetStaff) item.damage = 25;


            if (item.type == ItemID.ImpStaff) item.damage = 31;
            if (item.type == ItemID.HornetStaff) item.shootSpeed = 20;
            if (item.type == ItemID.HornetStaff) item.useTime = 12;

            if (item.type == ItemID.Arkhalis) item.damage = 32;


            if (item.type == ItemID.Minishark) item.useTime = 5;
            if (item.type == ItemID.MoltenFury) item.useTime = 14;
            if (item.type == ItemID.MoltenFury) item.damage = 39;

            if (item.type == ItemID.PhoenixBlaster) item.damage = 35;
            if (item.type == ItemID.PhoenixBlaster) item.autoReuse = true;
            if (item.type == ItemID.PhoenixBlaster) item.useTime = 10;

            if (item.type == ItemID.DarkLance) item.damage = 53;
            if (item.type == ItemID.DarkLance) item.scale = 1.7f;


            if (item.type == ItemID.Blowpipe) item.shootSpeed = 15;
            if (item.type == ItemID.Blowpipe) item.damage = 15;
            if (item.type == ItemID.Blowpipe) item.knockBack = 4f;


            if (item.type == ItemID.Blowgun) item.shootSpeed = 17;
            if (item.type == ItemID.Blowgun) item.damage = 42;
            if (item.type == ItemID.Blowgun) item.useTime = 18;
            if (item.type == ItemID.Blowgun) item.useAnimation = 18;




            if (item.type == ItemID.EnchantedSword) item.shootSpeed = 15;
            if (item.type == ItemID.EnchantedSword) item.damage = 30;
            if (item.type == ItemID.EnchantedSword) item.useTime = 19;
            if (item.type == ItemID.Harpoon) item.damage = 50;
            if (item.type == ItemID.Harpoon) item.shootSpeed = 13;
            if (item.type == ItemID.Harpoon) item.crit = 30;
            if (item.type == ItemID.Harpoon) item.useTime = 13;
            if (item.type == ItemID.BladeofGrass) item.damage = 38;
            if (item.type == ItemID.BladeofGrass) item.scale = 1.7f;
            if (item.type == ItemID.BladeofGrass) item.useTime = 15;
            if (item.type == ItemID.BladeofGrass) item.useTurn = true;
            if (item.type == ItemID.FieryGreatsword) item.damage = 54;
            if (item.type == ItemID.FieryGreatsword) item.scale = 2f;
            if (item.type == ItemID.FieryGreatsword) item.useTime = 17;
            if (item.type == ItemID.FieryGreatsword) item.useTurn = true;
            if (item.type == ItemID.Muramasa) item.damage = 35;
            if (item.type == ItemID.Muramasa) item.crit = 12;
            if (item.type == ItemID.Muramasa) item.useTime = 9;
            if (item.type == ItemID.Muramasa) item.scale = 1.5f;
            if (item.type == ItemID.NimbusRod) item.mana = 0;
            if (item.type == ItemID.AquaScepter) item.damage = 34;
            if (item.type == ItemID.AquaScepter) item.shootSpeed = 19;
            if (item.type == ItemID.AquaScepter) item.useTime = 15;
            if (item.type == ItemID.NightsEdge) item.damage = 75;
            if (item.type == ItemID.NightsEdge) item.useTime = 11;
            if (item.type == ItemID.NightsEdge) item.scale = 2.5f;
            if (item.type == ItemID.NightsEdge) item.knockBack = 10f;
            if (item.type == ItemID.NightsEdge) item.crit = 14;
            if (item.type == ItemID.NightsEdge) item.useTurn = true;
            if (item.type == ItemID.BloodButcherer) item.damage = 31;
            if (item.type == ItemID.BloodButcherer) item.scale = 2f;
            if (item.type == ItemID.BloodButcherer) item.useTurn = true;
            if (item.type == ItemID.BeesKnees) item.damage = 37;
            if (item.type == ItemID.BeesKnees) item.useTime = 14;
            if (item.type == ItemID.Trident) item.damage = 30;
            if (item.type == ItemID.Trident) item.useTime = 17;
            if (item.type == ItemID.Swordfish) item.damage = 24;
            if (item.type == ItemID.Swordfish) item.useTime = 10;











            //Hardmode Weapons

            if (item.type == ItemID.GelBalloon) item.damage = 90;

            if (item.type == ItemID.MeteorStaff) item.damage = 69;
            if (item.type == ItemID.MeteorStaff) item.useTime = 6;
            if (item.type == ItemID.MeteorStaff) item.crit = 6;
            if (item.type == ItemID.CrystalStorm) item.useTime = 5;
            if (item.type == ItemID.CrystalStorm) item.shootSpeed = 20;

            if (item.type == ItemID.PearlwoodBow) item.damage = 39;
            if (item.type == ItemID.PearlwoodBow) item.useTime = 13;
            if (item.type == ItemID.PearlwoodBow) item.crit = 11;




            if (item.type == ItemID.MagicDagger) item.damage = 45;
            if (item.type == ItemID.MagicDagger) item.mana = 1;
            if (item.type == ItemID.MagicDagger) item.shootSpeed = 19;
            if (item.type == ItemID.MagicDagger) item.autoReuse = true;



            if (item.type == ItemID.KOCannon) item.damage = 89;
            if (item.type == ItemID.KOCannon) item.crit = 19;
            if (item.type == ItemID.KOCannon) item.useTime = 20;
            if (item.type == ItemID.SlapHand) item.knockBack = 36;
            if (item.type == ItemID.PearlwoodSword) item.damage = 40;
            if (item.type == ItemID.PearlwoodSword) item.useTime = 15;
            if (item.type == ItemID.PearlwoodSword) item.scale = 1.3f;
            if (item.type == ItemID.PearlwoodSword) item.crit = 11;
            if (item.type == ItemID.Cutlass) item.damage = 68;
            if (item.type == ItemID.Cutlass) item.useTime = 13;
            if (item.type == ItemID.Cutlass) item.scale = 1.5f;
            if (item.type == ItemID.Cutlass) item.crit = 18;
            if (item.type == ItemID.Excalibur) item.damage = 123;
            if (item.type == ItemID.Excalibur) item.useTime = 8;
            if (item.type == ItemID.Excalibur) item.scale = 2f;
            if (item.type == ItemID.Excalibur) item.useTurn = true;
            if (item.type == ItemID.Excalibur) item.crit = 12;

            if (item.type == ItemID.ChlorophyteClaymore) item.damage = 140;
            if (item.type == ItemID.ChlorophyteClaymore) item.useTime = 10;
            if (item.type == ItemID.ChlorophyteClaymore) item.shootSpeed = 24;

            if (item.type == ItemID.ChlorophyteSaber) item.damage = 113;
            if (item.type == ItemID.ChlorophyteSaber) item.useTime = 12;
            if (item.type == ItemID.ChlorophyteSaber) item.shootSpeed = 25;
            if (item.type == ItemID.TerraBlade) item.damage = 150;
            if (item.type == ItemID.TerraBlade) item.useTime = 12;
            if (item.type == ItemID.TerraBlade) item.shootSpeed = 40;

            //Eclipse
            if (item.type == ItemID.BrokenHeroSword) item.damage = 80;
            if (item.type == ItemID.BrokenHeroSword) item.useTime = 20;
            if (item.type == ItemID.BrokenHeroSword) item.useAnimation = 20;
            if (item.type == ItemID.BrokenHeroSword) item.crit = 6;
            if (item.type == ItemID.BrokenHeroSword) item.useStyle = ItemUseStyleID.Swing;
            if (item.type == ItemID.BrokenHeroSword) item.DamageType = DamageClass.Melee;
            if (item.type == ItemID.BrokenHeroSword) item.knockBack = 2f;
            if (item.type == ItemID.BrokenHeroSword) item.autoReuse = true;
            if (item.type == ItemID.BrokenHeroSword) item.useTurn = true;






            //PUMPKIN MOON
            /*   if (item.type == ItemID.TheHorsemansBlade) item.damage = 180;
               if (item.type == ItemID.TheHorsemansBlade) item.useTime = 6;
               if (item.type == ItemID.TheHorsemansBlade) item.useAnimation = 6;

               if (item.type == ItemID.TheHorsemansBlade) item.crit = 12;
               if (item.type == ItemID.TheHorsemansBlade) item.knockBack = 12;
               if (item.type == ItemID.TheHorsemansBlade) item.scale = 3f;

               if (item.type == ItemID.RavenStaff) item.damage = 110;
               if (item.type == ItemID.RavenStaff) item.useTime = 13;
               if (item.type == ItemID.RavenStaff) item.crit = 12;
               if (item.type == ItemID.RavenStaff) item.knockBack = 12;
               if (item.type == ItemID.RavenStaff) item.mana = 0;
               if (item.type == ItemID.RavenStaff) item.autoReuse = true;
               if (item.type == ItemID.RavenStaff) item.shootSpeed = 20f;

               if (item.type == ItemID.BatScepter) item.damage = 97;
               if (item.type == ItemID.BatScepter) item.useTime = 6;
               if (item.type == ItemID.BatScepter) item.crit = 6;
               if (item.type == ItemID.BatScepter) item.knockBack = 6;
               if (item.type == ItemID.BatScepter) item.shootSpeed = 23f;
               if (item.type == ItemID.BatScepter) item.mana = 0;

               if (item.type == ItemID.CandyCornRifle) item.damage = 75;
               if (item.type == ItemID.CandyCornRifle) item.useTime = 5;
               if (item.type == ItemID.CandyCornRifle) item.crit = 6;
               if (item.type == ItemID.CandyCornRifle) item.knockBack = 6;
               if (item.type == ItemID.CandyCornRifle) item.shootSpeed = 24f;

               if (item.type == ItemID.JackOLanternLauncher) item.damage = 130;
               if (item.type == ItemID.JackOLanternLauncher) item.useTime = 13;
               if (item.type == ItemID.JackOLanternLauncher) item.crit = 8;
               if (item.type == ItemID.JackOLanternLauncher) item.knockBack = 10;
               if (item.type == ItemID.JackOLanternLauncher) item.shootSpeed = 25f;

               if (item.type == ItemID.ScytheWhip) item.damage = 120;
               if (item.type == ItemID.ScytheWhip) item.useTime = 19;
               if (item.type == ItemID.ScytheWhip) item.crit = 8;
               if (item.type == ItemID.ScytheWhip) item.knockBack = 6;
               if (item.type == ItemID.ScytheWhip) item.shootSpeed = 22f;


               if (item.type == ItemID.StakeLauncher) item.damage = 90;
               if (item.type == ItemID.StakeLauncher) item.useTime = 10;
               if (item.type == ItemID.StakeLauncher) item.crit = 20;
               if (item.type == ItemID.StakeLauncher) item.knockBack = 7;
               if (item.type == ItemID.StakeLauncher) item.shootSpeed = 30f;
            */
            //Endgame Weapons (Moon Lord)

            if (item.type == ItemID.DayBreak) item.useTime = 10;
            if (item.type == ItemID.NebulaArcanum) item.useTime = 15;
            if (item.type == ItemID.NebulaArcanum) item.damage = 80;
            if (item.type == ItemID.NebulaArcanum) item.shootSpeed = 16;
            if (item.type == ItemID.NebulaBlaze) item.useTime = 13;
            if (item.type == ItemID.NebulaBlaze) item.damage = 77;
            if (item.type == ItemID.SDMG) item.useTime = 3;
            if (item.type == ItemID.SDMG) item.damage = 88;
            if (item.type == ItemID.Meowmere) item.useTime = 13;
            if (item.type == ItemID.Meowmere) item.shootSpeed = 30f;
            if (item.type == ItemID.Meowmere) item.damage = 240;
            if (item.type == ItemID.SolarEruption) item.damage = 159;
            if (item.type == ItemID.SolarEruption) item.useTime = 17;








            if (item.type == ItemID.Wood)
                item.ammo = item.type;
            if (item.type == ItemID.MudBlock)
                item.ammo = item.type;
            if (item.type == ItemID.Cactus)
                item.ammo = item.type;










            //Hooks
            if (item.type == ItemID.GrapplingHook) item.damage = 14;
            if (item.type == ItemID.AmethystHook) item.damage = 17;
            if (item.type == ItemID.SquirrelHook) item.damage = 16;
            if (item.type == ItemID.TopazHook) item.damage = 18;
            if (item.type == ItemID.SapphireHook) item.damage = 19;
            if (item.type == ItemID.EmeraldHook) item.damage = 21;
            if (item.type == ItemID.AmberHook) item.damage = 22;
            if (item.type == ItemID.RubyHook) item.damage = 24;
            if (item.type == ItemID.DiamondHook) item.damage = 26;
            if (item.type == ItemID.SlimeHook) item.damage = 17;
            if (item.type == ItemID.WebSlinger) item.damage = 15;
            if (item.type == ItemID.SkeletronHand) item.damage = 38;
            if (item.type == ItemID.FishHook) item.damage = 25;
            if (item.type == ItemID.IvyWhip) item.damage = 29;
            if (item.type == ItemID.BatHook) item.damage = 43;
            if (item.type == ItemID.CandyCaneHook) item.damage = 43;
            if (item.type == ItemID.DualHook) item.damage = 59;
            if (item.type == ItemID.ThornHook) item.damage = 65;
            if (item.type == ItemID.TendonHook) item.damage = 63;
            if (item.type == ItemID.WormHook) item.damage = 63;
            if (item.type == ItemID.IlluminantHook) item.damage = 63;
            if (item.type == ItemID.AntiGravityHook) item.damage = 73;
            if (item.type == ItemID.SpookyHook) item.damage = 70;
            if (item.type == ItemID.ChristmasHook) item.damage = 70;
            if (item.type == ItemID.StaticHook) item.damage = 78;
            if (item.type == ItemID.LunarHook) item.damage = 90;







        }

        public override void AddRecipes()

        {
            Recipe Starfury = Recipe.Create(ItemID.Starfury);
            Starfury.AddIngredient(ItemID.FallenStar, 10);
            Starfury.AddIngredient(ItemID.GoldBar, 5);
            Starfury.AddIngredient(ItemID.EnchantedNightcrawler, 1);
            Starfury.AddTile(TileID.Anvils);
            Starfury.Register();

            Recipe Starfury1 = Recipe.Create(ItemID.Starfury);
            Starfury1.AddIngredient(ItemID.FallenStar, 10);
            Starfury1.AddIngredient(ItemID.PlatinumBar, 5);
            Starfury1.AddIngredient(ItemID.EnchantedNightcrawler, 1);
            Starfury1.AddTile(TileID.Anvils);
            Starfury1.Register();

            Recipe enchantedboomerang = Recipe.Create(ItemID.EnchantedBoomerang);
            enchantedboomerang.AddIngredient(ItemID.FallenStar, 4);
            enchantedboomerang.AddIngredient(ItemID.WoodenBoomerang);
            enchantedboomerang.AddTile(TileID.WorkBenches);
            enchantedboomerang.Register();


            Recipe woodenboomerang = Recipe.Create(ItemID.WoodenBoomerang);
            woodenboomerang.AddIngredient(ItemID.Wood, 12);
            woodenboomerang.AddIngredient(ItemID.StoneBlock, 10);
            woodenboomerang.AddTile(TileID.WorkBenches);
            woodenboomerang.Register();

            Recipe Muramasa = Recipe.Create(ItemID.Muramasa);
            Muramasa.AddIngredient(ItemID.Bone, 30);
            Muramasa.AddIngredient(ItemID.LesserManaPotion, 2);
            Muramasa.AddTile(TileID.Anvils);
            Muramasa.Register();

            Recipe BeeKeeper = Recipe.Create(ItemID.BeeKeeper);
            BeeKeeper.AddIngredient(ItemID.BeeWax, 10);
            BeeKeeper.AddIngredient(ItemID.Hive, 15);
            BeeKeeper.AddIngredient(ItemID.Stinger, 3);
            BeeKeeper.AddTile(TileID.Anvils);
            BeeKeeper.Register();

            Recipe ChainKnife = Recipe.Create(ItemID.ChainKnife);
            ChainKnife.AddIngredient(ItemID.Chain, 6);
            ChainKnife.AddRecipeGroup("IronBar", 2);
            ChainKnife.AddTile(TileID.Anvils);
            ChainKnife.Register();

            Recipe Arkhalis = Recipe.Create(ItemID.Arkhalis);
            Arkhalis.AddIngredient(ItemID.FallenStar, 5);
            Arkhalis.AddIngredient(ItemID.EnchantedSword, 1);
            Arkhalis.AddIngredient(ItemID.Diamond, 5);
            Arkhalis.AddTile(TileID.Anvils);
            Arkhalis.Register();

            Recipe Stormbow = Recipe.Create(ItemID.DaedalusStormbow);
            Stormbow.AddIngredient(ItemID.FallenStar, 5);
            Stormbow.AddIngredient(ItemID.LightKey, 1);
            Stormbow.AddIngredient(ItemID.CrystalShard, 12);
            Stormbow.AddIngredient(ItemID.PearlwoodBow, 1);
            Stormbow.AddTile(TileID.MythrilAnvil);
            Stormbow.Register();

            Recipe PulseBow = Recipe.Create(ItemID.PulseBow);
            PulseBow.AddIngredient(ItemID.Nanites, 10);
            PulseBow.AddIngredient(ItemID.ChlorophyteBar, 8);
            PulseBow.AddIngredient(ItemID.BlueDynastyShingles, 5);
            PulseBow.AddTile(TileID.MythrilAnvil);
            PulseBow.Register();

            Recipe Blizzard = Recipe.Create(ItemID.BlizzardinaBottle);
            Blizzard.AddIngredient(ItemID.BottledWater, 1);
            Blizzard.AddIngredient(ItemID.Snowball, 20);
            Blizzard.AddIngredient(ItemID.Cloud, 10);
            Blizzard.AddIngredient(ItemID.Feather, 1);
            Blizzard.AddTile(TileID.Anvils);
            Blizzard.Register();

            Recipe CloudBottle = Recipe.Create(ItemID.CloudinaBottle);
            CloudBottle.AddIngredient(ItemID.Bottle, 1);
            CloudBottle.AddIngredient(ItemID.Feather, 1);
            CloudBottle.AddIngredient(ItemID.Cloud, 10);
            CloudBottle.AddTile(TileID.Anvils);
            CloudBottle.Register();

            Recipe SlapLOL = Recipe.Create(ItemID.SlapHand);
            SlapLOL.AddIngredient(ItemID.Bone, 60);
            SlapLOL.AddIngredient(ItemID.AlphabetStatueS);
            SlapLOL.AddIngredient(ItemID.AlphabetStatueL);
            SlapLOL.AddIngredient(ItemID.AlphabetStatueA);
            SlapLOL.AddIngredient(ItemID.AlphabetStatueP);
            SlapLOL.AddTile(TileID.Anvils);
            SlapLOL.Register();


            Recipe Hermes = Recipe.Create(ItemID.HermesBoots);
            Hermes.AddIngredient(ItemID.Feather, 1);
            Hermes.AddIngredient(ItemID.Silk, 10);
            Hermes.AddIngredient(ItemID.SwiftnessPotion, 1);
            Hermes.AddTile(TileID.Anvils);
            Hermes.Register();

            Recipe Flurry = Recipe.Create(ItemID.FlurryBoots);
            Flurry.AddIngredient(ItemID.Feather, 1);
            Flurry.AddIngredient(ItemID.Silk, 10);
            Flurry.AddIngredient(ItemID.IceBlock, 50);
            Flurry.AddTile(TileID.Anvils);
            Flurry.Register();

            Recipe IceBlade = Recipe.Create(ItemID.IceBlade);
            IceBlade.AddIngredient(ItemID.IceBlock, 20);
            IceBlade.AddIngredient(ItemID.SnowBlock, 10);
            IceBlade.AddRecipeGroup("IronBar", 5);
            IceBlade.AddTile(TileID.Anvils);
            IceBlade.Register();


            Recipe Nimbus = Recipe.Create(ItemID.NimbusRod);
            Nimbus.AddIngredient(ItemID.Bone, 20);
            Nimbus.AddIngredient(ItemID.Cloud, 10);
            Nimbus.AddIngredient(ItemID.RainCloud, 10);
            Nimbus.AddTile(TileID.Anvils);
            Nimbus.Register();

            Recipe Wbolt = Recipe.Create(ItemID.WaterBolt);
            Wbolt.AddIngredient(ItemID.BottledWater, 15);
            Wbolt.AddIngredient(ItemID.Book, 1);
            Wbolt.AddIngredient(ItemID.WaterCandle, 1);
            Wbolt.AddTile(TileID.Bookcases);
            Wbolt.Register();

            Recipe LavaC = Recipe.Create(ItemID.LavaCharm);
            LavaC.AddIngredient(ItemID.LavaBucket, 5);
            LavaC.AddIngredient(ItemID.Obsidian, 20);
            LavaC.AddIngredient(ItemID.Fireblossom, 3);
            LavaC.AddTile(TileID.Anvils);
            LavaC.Register();

            Recipe DepthMeter = Recipe.Create(ItemID.DepthMeter);
            DepthMeter.AddIngredient(Mod, "GizmoScrap", 4);
            DepthMeter.AddIngredient(ItemID.Chain, 10);
            DepthMeter.AddIngredient(ItemID.SilverBar, 5);
            DepthMeter.AddTile(TileID.Sawmill);
            DepthMeter.Register();

            Recipe Compass = Recipe.Create(ItemID.Compass);
            Compass.AddIngredient(Mod, "GizmoScrap", 4);
            Compass.AddIngredient(ItemID.Glass, 10);
            Compass.AddIngredient(ItemID.CopperBar, 5);
            Compass.AddTile(TileID.Sawmill);
            Compass.Register();

            Recipe Compass1 = Recipe.Create(ItemID.Compass);
            Compass1.AddIngredient(Mod, "GizmoScrap", 4);
            Compass1.AddIngredient(ItemID.Glass, 10);
            Compass1.AddIngredient(ItemID.TinBar, 5);
            Compass1.AddTile(TileID.Sawmill);
            Compass1.Register();

            Recipe Stopwatch = Recipe.Create(ItemID.Stopwatch);
            Stopwatch.AddIngredient(Mod, "GizmoScrap", 8);
            Stopwatch.AddIngredient(ItemID.Switch, 1);
            Stopwatch.AddIngredient(ItemID.Chain, 1);
            Stopwatch.AddTile(TileID.Sawmill);
            Stopwatch.Register();

            Recipe MetalD = Recipe.Create(ItemID.MetalDetector);
            MetalD.AddIngredient(Mod, "GizmoScrap", 8);
            MetalD.AddIngredient(ItemID.SpelunkerPotion, 5);
            MetalD.AddIngredient(ItemID.IronBar, 10);
            MetalD.AddTile(TileID.Sawmill);
            MetalD.Register();

            Recipe MetalD1 = Recipe.Create(ItemID.MetalDetector);
            MetalD1.AddIngredient(Mod, "GizmoScrap", 8);
            MetalD1.AddIngredient(ItemID.SpelunkerPotion, 5);
            MetalD1.AddIngredient(ItemID.LeadBar, 10);
            MetalD1.AddTile(TileID.Sawmill);
            MetalD1.Register();

            Recipe DPSM = Recipe.Create(ItemID.DPSMeter);
            DPSM.AddIngredient(Mod, "GizmoScrap", 8);
            DPSM.AddIngredient(ItemID.CrimtaneBar, 5);
            DPSM.AddIngredient(ItemID.Lens, 3);
            DPSM.AddTile(TileID.Sawmill);
            DPSM.Register();

            Recipe DPSM1 = Recipe.Create(ItemID.DPSMeter);
            DPSM1.AddIngredient(Mod, "GizmoScrap", 8);
            DPSM1.AddIngredient(ItemID.DemoniteBar, 5);
            DPSM1.AddIngredient(ItemID.Lens, 3);
            DPSM1.AddTile(TileID.Sawmill);
            DPSM1.Register();


            Recipe EOC1 = Recipe.Create(ItemID.EyeMask);
            EOC1.AddIngredient(Mod, "FleshyCornea", 4);
            EOC1.AddTile(TileID.Anvils);
            EOC1.Register();

            Recipe EOC2 = Recipe.Create(ItemID.EoCShield);
            EOC2.AddIngredient(Mod, "FleshyCornea", 25);
            EOC2.AddTile(TileID.Anvils);
            EOC2.Register();


            Recipe EOC3 = Recipe.Create(ItemID.EyeOfCthulhuBossBag);
            EOC3.AddIngredient(Mod, "FleshyCornea", 100);
            EOC3.AddTile(TileID.Anvils);
            EOC3.Register();


            Recipe EOC4 = Recipe.Create(ItemID.EyeofCthulhuTrophy);
            EOC4.AddIngredient(Mod, "FleshyCornea", 4);
            EOC4.AddTile(TileID.Anvils);
            EOC4.Register();

            Recipe EOC5 = Recipe.Create(ItemID.Binoculars);
            EOC5.AddIngredient(Mod, "FleshyCornea", 15);
            EOC5.AddTile(TileID.Anvils);
            EOC5.Register();


            Recipe spear = Recipe.Create(ItemID.Spear);
            spear.AddRecipeGroup("IronBar", 8);
            spear.AddTile(TileID.WorkBenches);
            spear.Register();

            Recipe Trident = Recipe.Create(ItemID.Trident);
            Trident.AddIngredient(ItemID.Spear, 1);
            Trident.AddIngredient(ItemID.Seashell, 5);
            Trident.AddIngredient(ItemID.Coral, 5);
            Trident.AddTile(TileID.Anvils);
            Trident.Register();

            Recipe Swordfish = Recipe.Create(ItemID.Swordfish);
            Swordfish.AddIngredient(ItemID.Spear, 1);
            Swordfish.AddIngredient(ItemID.Starfish, 8);
            Swordfish.AddIngredient(ItemID.Coral, 8);
            Swordfish.AddIngredient(ItemID.Bass, 1);
            Swordfish.AddTile(TileID.Anvils);
            Swordfish.Register();


            Recipe CSerpent = Recipe.Create(ItemID.CrystalSerpent);
            CSerpent.AddIngredient(ItemID.SoulofLight, 15);
            CSerpent.AddIngredient(ItemID.PearlstoneBlock, 50);
            CSerpent.AddIngredient(ItemID.Prismite, 2);
            CSerpent.AddIngredient(ItemID.CrystalShard, 10);
            CSerpent.AddTile(TileID.CrystalBall);
            CSerpent.Register();


            Recipe BTongue = Recipe.Create(ItemID.Bladetongue);
            BTongue.AddIngredient(ItemID.Vertebrae, 15);
            BTongue.AddIngredient(ItemID.Ichor, 20);
            BTongue.AddIngredient(ItemID.SoulofNight, 6);
            BTongue.AddTile(TileID.MythrilAnvil);
            BTongue.Register();


            Recipe BeamS = Recipe.Create(ItemID.BeamSword);
            BeamS.AddIngredient(ItemID.CobaltBar, 10);
            BeamS.AddIngredient(ItemID.StoneBlock, 40);
            BeamS.AddIngredient(ItemID.SoulofLight, 5);
            BeamS.AddIngredient(ItemID.ManaCrystal, 1);
            BeamS.AddTile(TileID.MythrilAnvil);
            BeamS.Register();

            Recipe BeamS1 = Recipe.Create(ItemID.BeamSword);
            BeamS1.AddIngredient(ItemID.PalladiumBar, 10);
            BeamS1.AddIngredient(ItemID.StoneBlock, 40);
            BeamS1.AddIngredient(ItemID.SoulofLight, 5);
            BeamS1.AddIngredient(ItemID.ManaCrystal, 1);
            BeamS1.AddTile(TileID.MythrilAnvil);
            BeamS1.Register();

            Recipe FrostB = Recipe.Create(ItemID.Frostbrand);
            FrostB.AddIngredient(ItemID.IceBlade, 1);
            FrostB.AddIngredient(ItemID.IceBlock, 75);
            FrostB.AddIngredient(ItemID.SoulofNight, 6);
            FrostB.AddIngredient(ItemID.Shiverthorn, 4);
            FrostB.AddIngredient(ItemID.FrostCore, 1);

            FrostB.AddTile(TileID.MythrilAnvil);
            FrostB.Register();


            Recipe BGlove = Recipe.Create(ItemID.BladedGlove);
            BGlove.AddIngredient(ItemID.Leather, 5);
            BGlove.AddIngredient(ItemID.ThrowingKnife, 75);
            BGlove.AddTile(TileID.Anvils);
            BGlove.Register();

            Recipe FalconB = Recipe.Create(ItemID.FalconBlade);
            FalconB.AddIngredient(ItemID.Feather, 5);
            FalconB.AddIngredient(ItemID.SilverBroadsword, 1);
            FalconB.AddIngredient(ItemID.PalmWood, 10);
            FalconB.AddTile(TileID.Anvils);
            FalconB.Register();

            Recipe FalconB1 = Recipe.Create(ItemID.FalconBlade);
            FalconB1.AddIngredient(ItemID.Feather, 5);
            FalconB1.AddIngredient(ItemID.TungstenBroadsword, 1);
            FalconB1.AddIngredient(ItemID.PalmWood, 10);
            FalconB1.AddTile(TileID.Anvils);
            FalconB1.Register();

            Recipe ankletwind = Recipe.Create(ItemID.AnkletoftheWind);
            ankletwind.AddIngredient(ItemID.JungleSpores, 10);
            ankletwind.AddIngredient(ItemID.Vine, 2);
            ankletwind.AddIngredient(ItemID.Feather, 3);
            ankletwind.AddIngredient(ItemID.Amethyst, 1);
            ankletwind.AddTile(TileID.Anvils);
            ankletwind.Register();

            Recipe snowballcan = Recipe.Create(ItemID.SnowballCannon);
            snowballcan.AddIngredient(ItemID.Snowball, 50);
            snowballcan.AddIngredient(ItemID.Glass, 5);
            snowballcan.AddIngredient(ItemID.IceBlock, 15);
            snowballcan.AddRecipeGroup("IronBar", 5);
            snowballcan.AddTile(TileID.IceMachine);
            snowballcan.Register();

            Recipe froststaff = Recipe.Create(ItemID.FrostStaff);
            froststaff.AddIngredient(ItemID.FrostCore, 1);
            froststaff.AddIngredient(ItemID.IceBlock, 30);
            froststaff.AddIngredient(ItemID.Diamond, 1);
            froststaff.AddIngredient(ItemID.SoulofNight, 10);
            froststaff.AddTile(TileID.MythrilAnvil);
            froststaff.Register();

            Recipe iceboom = Recipe.Create(ItemID.IceBoomerang);
            iceboom.AddIngredient(ItemID.WoodenBoomerang, 1);
            iceboom.AddIngredient(ItemID.IceBlock, 10);
            iceboom.AddIngredient(ItemID.Shiverthorn, 2);
            iceboom.AddTile(TileID.Anvils);
            iceboom.Register();

            Recipe iceskates = Recipe.Create(ItemID.IceSkates);
            iceskates.AddRecipeGroup("IronBar", 4);
            iceskates.AddIngredient(ItemID.Silk, 5);
            iceskates.AddIngredient(ItemID.BorealWood, 7);
            iceskates.AddTile(TileID.IceMachine);
            iceskates.Register();


            Recipe icebow = Recipe.Create(ItemID.IceBow);
            icebow.AddIngredient(ItemID.WoodenBow, 1);
            icebow.AddIngredient(ItemID.FrostCore, 1);
            icebow.AddIngredient(ItemID.IceBlock, 35);
            icebow.AddIngredient(ItemID.CobaltBar, 5);
            icebow.AddIngredient(ItemID.SoulofLight, 4);
            icebow.AddTile(TileID.MythrilAnvil);
            icebow.Register();


            Recipe icebow1 = Recipe.Create(ItemID.IceBow);
            icebow1.AddIngredient(ItemID.WoodenBow, 1);
            icebow1.AddIngredient(ItemID.FrostCore, 1);
            icebow1.AddIngredient(ItemID.IceBlock, 35);
            icebow1.AddIngredient(ItemID.PalladiumBar, 5);
            icebow1.AddIngredient(ItemID.SoulofLight, 4);
            icebow1.AddTile(TileID.MythrilAnvil);
            icebow1.Register();



            Recipe icesickle = Recipe.Create(ItemID.IceSickle);
            icesickle.AddIngredient(ItemID.FrostCore, 1);
            icesickle.AddIngredient(ItemID.IceBlock, 35);
            icesickle.AddIngredient(ItemID.CobaltBar, 5);
            icesickle.AddIngredient(ItemID.SoulofNight, 4);
            icesickle.AddTile(TileID.MythrilAnvil);
            icesickle.Register();


            Recipe icesickle1 = Recipe.Create(ItemID.IceSickle);
            icesickle1.AddIngredient(ItemID.FrostCore, 1);
            icesickle1.AddIngredient(ItemID.IceBlock, 35);
            icesickle1.AddIngredient(ItemID.PalladiumBar, 5);
            icesickle1.AddIngredient(ItemID.SoulofNight, 4);
            icesickle1.AddTile(TileID.MythrilAnvil);
            icesickle1.Register();


            Recipe flowerfrost = Recipe.Create(ItemID.FlowerofFrost);
            flowerfrost.AddIngredient(ItemID.FrostCore, 1);
            flowerfrost.AddIngredient(ItemID.NaturesGift, 1);
            flowerfrost.AddIngredient(ItemID.IceBlock, 30);
            flowerfrost.AddIngredient(ItemID.SoulofNight, 4);
            flowerfrost.AddTile(TileID.MythrilAnvil);
            flowerfrost.Register();


            Recipe amarok = Recipe.Create(ItemID.Amarok);
            amarok.AddIngredient(ItemID.FrostCore, 1);
            amarok.AddIngredient(ItemID.WoodYoyo, 1);
            amarok.AddIngredient(ItemID.IceBlock, 25);
            amarok.AddIngredient(ItemID.SoulofLight, 4);
            amarok.AddTile(TileID.MythrilAnvil);
            amarok.Register();

            Recipe shoespikes = Recipe.Create(ItemID.ShoeSpikes);
            shoespikes.AddIngredient(ItemID.Leather, 3);
            shoespikes.AddIngredient(ItemID.CopperBar, 5);
            shoespikes.AddIngredient(ItemID.ThrowingKnife, 25);
            shoespikes.AddTile(TileID.Anvils);
            shoespikes.Register();

            Recipe shoespikes1 = Recipe.Create(ItemID.ShoeSpikes);
            shoespikes1.AddIngredient(ItemID.Leather, 3);
            shoespikes1.AddIngredient(ItemID.TinBar, 5);
            shoespikes1.AddIngredient(ItemID.ThrowingKnife, 25);
            shoespikes1.AddTile(TileID.Anvils);
            shoespikes1.Register();

            Recipe climbingclaws = Recipe.Create(ItemID.ClimbingClaws);
            climbingclaws.AddIngredient(ItemID.Leather, 3);
            climbingclaws.AddIngredient(ItemID.CopperBar, 5);
            climbingclaws.AddIngredient(ItemID.ThrowingKnife, 25);
            climbingclaws.AddTile(TileID.Anvils);
            climbingclaws.Register();

            Recipe climbingclaws1 = Recipe.Create(ItemID.ClimbingClaws);
            climbingclaws1.AddIngredient(ItemID.Leather, 3);
            climbingclaws1.AddIngredient(ItemID.CopperBar, 5);
            climbingclaws1.AddIngredient(ItemID.ThrowingKnife, 25);
            climbingclaws1.AddTile(TileID.Anvils);
            climbingclaws1.Register();


            Recipe extractinator = Recipe.Create(ItemID.Extractinator);
            extractinator.AddRecipeGroup("IronBar", 15);
            extractinator.AddIngredient(ModContent.ItemType<BrassIngot>(), 8);
            extractinator.AddIngredient(ItemID.Glass, 10);
            extractinator.AddTile(TileID.HeavyWorkBench);
            extractinator.Register();

            Recipe obj = Recipe.Create(ItemID.Blowpipe);
            obj.AddIngredient(ItemID.Wood, 30);

            obj.AddIngredient(ItemID.Acorn, 5);
            obj.AddTile(TileID.Anvils);
            obj.Register();
            Recipe obj2 = Recipe.Create(ItemID.BandofRegeneration, 1);
            obj2.AddIngredient(ItemID.LesserHealingPotion, 5);
            obj2.AddRecipeGroup("IronBar", 5);
            obj2.AddTile(TileID.Anvils);
            obj2.Register();
            Recipe obj3 = Recipe.Create(ItemID.BandofStarpower);
            obj3.AddIngredient(ItemID.ManaCrystal, 2);
            obj3.AddRecipeGroup("IronBar", 5);
            obj3.AddTile(TileID.Anvils);
            obj3.Register();
            Recipe obj4 = Recipe.Create(ItemID.FlowerofFire, 1);
            obj4.AddIngredient(ItemID.HellstoneBar, 12);
            obj4.AddIngredient(ItemID.Fireblossom, 5);
            obj4.AddIngredient(ItemID.Bone, 10);
            obj4.AddTile(TileID.Hellforge);
            obj4.Register();
            Recipe obj5 = Recipe.Create(ItemID.DarkLance);
            obj5.AddIngredient(ItemID.HellstoneBar, 8);
            obj5.AddIngredient(ItemID.DemoniteBar, 15);
            obj5.AddIngredient(ItemID.Bone, 10);
            obj5.AddTile(TileID.Hellforge);
            obj5.Register();
            Recipe obj6 = Recipe.Create(ItemID.DarkLance);
            obj6.AddIngredient(ItemID.HellstoneBar, 8);
            obj6.AddIngredient(ItemID.CrimtaneBar, 15);
            obj6.AddIngredient(ItemID.Bone, 10);
            obj6.AddTile(TileID.Hellforge);
            obj6.Register();
          
        }


    }

    public class RecipeGroups : ModSystem
    {
        static string ItemXOrY(int id1, int id2) => $"{Lang.GetItemName(id1)} {Language.GetTextValue($"Mods.RealmOne.RecipeGroups.Or")} {Lang.GetItemName(id2)}";

        static string AnyItem(int id) => $"{Lang.misc[37]} {Lang.GetItemName(id)}";

        public override void AddRecipeGroups()
        {
            RecipeGroup group;
            group = new RecipeGroup(() => AnyItem(ItemID.CopperBar),
               ItemID.CopperBar,
               ItemID.TinBar

           );
            RecipeGroup.RegisterGroup("RealmOne:AnyCopperBar", group);


            group = new RecipeGroup(() => AnyItem(ItemID.GoldBar),
               ItemID.GoldBar,
               ItemID.PlatinumBar

           );
            RecipeGroup.RegisterGroup("RealmOne:AnyGoldBar", group);

        }
    }
}