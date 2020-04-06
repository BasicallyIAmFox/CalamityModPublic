using CalamityMod.Projectiles.Melee.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee
{
    public class MarniteSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marnite Spear");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.damage = 20;
            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 21;
            item.useStyle = 5;
            item.useTime = 21;
            item.knockBack = 5.25f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.height = 50;
            item.value = Item.buyPrice(0, 2, 0, 0);
            item.rare = 2;
            item.shoot = ModContent.ProjectileType<MarniteSpearProjectile>();
            item.shootSpeed = 5f;
			item.Calamity().trueMelee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AnyGoldBar", 5);
            recipe.AddIngredient(ItemID.Granite, 9);
            recipe.AddIngredient(ItemID.Marble, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
