namespace RealmOne.Tiles.PlantTiles
{
    /*  public class MiniWattleBush : ModTile
      {
          public enum PlantStage : byte
          {
              Planted,
              Growing,
              Grown
          }
          private const int FrameWidth = 32;

          public override void SetStaticDefaults()
          {
              Main.tileFrameImportant[Type] = true;
              Main.tileNoFail[Type] = true;
              Main.tileLighted[Type] = true;

              Main.tileFrameImportant[Type] = true;
              Main.tileObsidianKill[Type] = true;
              Main.tileCut[Type] = true;
              Main.tileNoFail[Type] = true;
              TileID.Sets.ReplaceTileBreakUp[Type] = true;
              TileID.Sets.IgnoredInHouseScore[Type] = true;
              TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
              TileID.Sets.SwaysInWindBasic[Type] = true;
              TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

              HitSound = SoundID.Grass;

              TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
              TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
              TileObjectData.addTile(Type);

              MineResist = 1f;
              MinPick = 20;

              DustType = DustID.Sunflower;
              LocalizedText name = CreateMapEntryName();
              name.SetDefault("Mini Wattle Bush");

              AddMapEntry(new Color(215, 240, 60));
          }
          public bool CanBeHarvested(int i, int j) => Main.tile[i, j].HasTile && GetStage(i, j) == PlantStage.Grown;

          public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
          {
              Tile tile = Framing.GetTileSafely(i, j);
              if (tile.TileFrameY <= 18 && (tile.TileFrameX <= 36 || tile.TileFrameX >= 72))
              {
                  r = 0.6f;
                  g = 0.7f;
                  b = 0.15f;
              }
          }
          public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY) => offsetY = -2;

          public override bool CanPlace(int i, int j)
          {
              Tile tile = Framing.GetTileSafely(i, j);

              if (!tile.HasTile)
                  return true;

              int tileType = tile.TileType;
              if (tileType == Type)
              {
                  PlantStage stage = GetStage(i, j);
                  return stage == PlantStage.Grown;
              }
              else
              {
                  if (Main.tileCut[tileType] || TileID.Sets.BreakableWhenPlacing[tileType] || tileType == TileID.WaterDrip || tileType == TileID.LavaDrip || tileType == TileID.HoneyDrip || tileType == TileID.SandDrip)
                  {
                      bool foliageGrass = tileType == TileID.Plants || tileType == TileID.Plants2;
                      bool moddedFoliage = tileType >= TileID.Count && (Main.tileCut[tileType] || TileID.Sets.BreakableWhenPlacing[tileType]);
                      bool harvestableVanillaHerb = Main.tileAlch[tileType] && WorldGen.IsHarvestableHerbWithSeed(tileType, tile.TileFrameX / 18);

                      if (foliageGrass || moddedFoliage || harvestableVanillaHerb)
                      {
                          WorldGen.KillTile(i, j);

                          if (!tile.HasTile && Main.netMode == NetmodeID.MultiplayerClient)
                              NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);

                          return true;
                      }
                  }
                  return false;
              }
          }

          public override void RandomUpdate(int i, int j)
          {
              Tile tile = Framing.GetTileSafely(i, j);
              PlantStage stage = GetStage(i, j);

              if (stage == PlantStage.Planted) //Grow only if just planted
              {
                  tile.TileFrameX += FrameWidth;

                  if (Main.netMode != NetmodeID.SinglePlayer)
                      NetMessage.SendTileSquare(-1, i, j, 1);
              }
          }
          public override IEnumerable<Item> GetItemDrops(int i, int j)
          {
              PlantStage stage = GetStage(i, j);

              Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
              Player nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];

              int herbItemType = ModContent.ItemType<Wattle>();
              int herbItemStack = 1;

              int seedItemType = ModContent.ItemType<MiniWattleBushItem>();
              int seedItemStack = 1;

              if (nearestPlayer.active && nearestPlayer.HeldItem.type == ItemID.StaffofRegrowth)
              {
                  // Increased yields with Staff of Regrowth, even when not fully grown
                  herbItemStack = Main.rand.Next(1, 3);
                  seedItemStack = Main.rand.Next(1, 6);
              }
              else if (stage == PlantStage.Grown)
              {
                  // Default yields, only when fully grown
                  herbItemStack = 1;
                  seedItemStack = Main.rand.Next(1, 4);
              }

              if (herbItemType > 0 && herbItemStack > 0)
              {
                  yield return new Item(herbItemType, herbItemStack);
              }

              if (seedItemType > 0 && seedItemStack > 0)
              {
                  yield return new Item(seedItemType, seedItemStack);
              }
          }

          public override bool IsTileSpelunkable(int i, int j)
          {
              PlantStage stage = GetStage(i, j);

              // Only glow if the herb is grown
              return stage == PlantStage.Grown;
          }
          public override bool CanDrop(int i, int j)
          {
              PlantStage stage = GetStage(i, j);

              if (stage == PlantStage.Planted)
              {
                  // Do not drop anything when just planted
                  return false;
              }
              return true;
          }
          public override void KillMultiTile(int i, int j, int frameX, int frameY)
          {
              Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 48, ModContent.ItemType<MiniWattleBushItem>());
              Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 48, ModContent.ItemType<Items.Misc.Plants.Wattle>(), Main.rand.Next(2, 4));

              if (Main.netMode != NetmodeID.Server)
              {
                  int WattleGore1 = Mod.Find<ModGore>("WattleGore1").Type;
                  int WattleGore2 = Mod.Find<ModGore>("WattleGore2").Type;

                  var entitySource = new EntitySource_TileBreak(i, j);

                  for (int g = 0; g < 1; g++)
                  {
                      Gore.NewGore(entitySource, new Vector2(i * 16, j * 16), new Vector2(Main.rand.Next(1)), WattleGore1);
                      Gore.NewGore(entitySource, new Vector2(i * 16, j * 16), new Vector2(Main.rand.Next(1)), WattleGore2);
                  }
              }
          }
          private static PlantStage GetStage(int i, int j)
          {
              Tile tile = Framing.GetTileSafely(i, j);
              return (PlantStage)(tile.TileFrameX / FrameWidth);
          }
          public override void NumDust(int i, int j, bool fail, ref int num)
          {
              num = 1;
          }

          private static void SetStage(int i, int j, PlantStage stage)
          {
              Tile tile = Framing.GetTileSafely(i, j);
              tile.TileFrameX = (short)(FrameWidth * (int)stage);
          }
      }
      */
}