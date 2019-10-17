using Terraria.ModLoader;
namespace CalamityMod.Items
{
    public class OccultPlatform : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            item.SetNameOverride("Otherworldly Platform");
            item.width = 8;
            item.height = 10;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.OccultPlatform>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OccultStone");
            recipe.SetResult(this, 2);
            recipe.AddTile(null, "DraedonsForge");
            recipe.AddRecipe();
        }
    }
}
