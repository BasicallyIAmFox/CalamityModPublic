using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls
{
    public class NavystoneWallSafe : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = 96;
            drop = ModContent.ItemType<Items.NavystoneWallSafe>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Navystone Wall Safe");
            AddMapEntry(new Color(0, 50, 50), name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
