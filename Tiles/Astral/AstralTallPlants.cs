﻿
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using CalamityMod.Dusts;

namespace CalamityMod.Tiles
{
    public class AstralTallPlants : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;

            dustType = ModContent.DustType<AstralBasic>();

            soundStyle = 1;
            soundType = 6;

            AddMapEntry(new Color(127, 111, 144));

            base.SetDefaults();
        }
    }
}
