using Terraria.ModLoader;
namespace CalamityMod.Items
{
    public class EutrophicCandle : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 20;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.EutrophicCandle>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Navystone>(), 4);
            recipe.AddIngredient(ModContent.ItemType<SeaPrism>(), 1);
            recipe.SetResult(this, 1);
            recipe.AddTile(null, "EutrophicCrafting");
            recipe.AddRecipe();
        }
    }
}
