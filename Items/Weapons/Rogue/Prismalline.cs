using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Materials;
using Terraria.ID;
using CalamityMod.Projectiles.Rogue;

namespace CalamityMod.Items.Weapons.Rogue
{
    public class Prismalline : RogueWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismalline");
            Tooltip.SetDefault("Throws daggers that split after a while");
        }

        public override void SafeSetDefaults()
        {
            item.width = 46;
            item.damage = 22;
            item.crit += 4;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useAnimation = 14;
            item.useStyle = 1;
            item.useTime = 14;
            item.knockBack = 5f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.height = 46;
            item.value = Item.buyPrice(0, 36, 0, 0);
            item.rare = 5;
            item.shoot = ModContent.ProjectileType<PrismallineProj>();
            item.shootSpeed = 16f;
            item.Calamity().rogue = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Crystalline>());
            recipe.AddIngredient(ModContent.ItemType<EssenceofEleum>(), 5);
            recipe.AddIngredient(ModContent.ItemType<SeaPrism>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
