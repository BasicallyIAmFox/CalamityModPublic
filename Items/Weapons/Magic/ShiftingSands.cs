using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic
{
    public class ShiftingSands : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shifting Sands");
            Tooltip.SetDefault("Casts a sand shard that follows the mouse cursor");
        }

        public override void SetDefaults()
        {
            item.damage = 85;
            item.magic = true;
            item.channel = true;
            item.mana = 20;
            item.width = 58;
            item.height = 58;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 5f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item20;
            item.shoot = ModContent.ProjectileType<ShiftingSandsProj>();
            item.shootSpeed = 7f;
			item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MagicMissile);
            recipe.AddIngredient(ModContent.ItemType<GrandScale>());
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
            recipe.AddIngredient(ItemID.SpectreBar, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
