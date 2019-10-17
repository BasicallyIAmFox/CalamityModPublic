
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityMod.Walls;

namespace CalamityMod.Tiles
{
    public class PlantyMush : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;

            TileMerge.MergeGeneralTiles(Type);
            TileMerge.MergeAbyssTiles(Type);

            dustType = 2;
            drop = ModContent.ItemType<Items.PlantyMush>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Planty Mush");
            AddMapEntry(new Color(0, 120, 0), name);
            mineResist = 1f;
            soundType = 0;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!closer && j < Main.maxTilesY - 205)
            {
                if (Main.tile[i, j].liquid <= 0)
                {
                    Main.tile[i, j].liquid = 255;
                    Main.tile[i, j].lava(false);
                }
            }
        }

        public override void RandomUpdate(int i, int j)
        {
            int num8 = WorldGen.genRand.Next((int)Main.rockLayer, (int)(Main.rockLayer + (double)Main.maxTilesY * 0.143));
            if (Main.tile[i, j + 1] != null)
            {
                if (!Main.tile[i, j + 1].active() && Main.tile[i, j + 1].type != (ushort)ModContent.TileType<ViperVines>())
                {
                    if (Main.tile[i, j + 1].liquid == 255 &&
                        (Main.tile[i, j + 1].wall == (ushort)ModContent.WallType<MossyGravelWall>() ||
                        Main.tile[i, j + 1].wall == (ushort)ModContent.WallType<AbyssGravelWall>()) &&
                        !Main.tile[i, j + 1].lava())
                    {
                        bool flag13 = false;
                        for (int num52 = num8; num52 > num8 - 10; num52--)
                        {
                            if (Main.tile[i, num52].bottomSlope())
                            {
                                flag13 = false;
                                break;
                            }
                            if (Main.tile[i, num52].active() && !Main.tile[i, num52].bottomSlope())
                            {
                                flag13 = true;
                                break;
                            }
                        }
                        if (flag13)
                        {
                            int num53 = i;
                            int num54 = j + 1;
                            Main.tile[num53, num54].type = (ushort)ModContent.TileType<ViperVines>();
                            Main.tile[num53, num54].active(true);
                            WorldGen.SquareTileFrame(num53, num54, true);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendTileSquare(-1, num53, num54, 3, TileChangeType.None);
                            }
                        }
                    }
                }
            }
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            CustomTileFraming.FrameTileForCustomMerge(i, j, Type, ModContent.TileType<AbyssGravel>(), false, false, false, false, resetFrame);
            return false;
        }
    }
}
