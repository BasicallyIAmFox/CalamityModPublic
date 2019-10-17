using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityMod.Items
{
    public class FrostyFlare : RogueWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frosty Flare");
            Tooltip.SetDefault("Sticks to enemies\n" +
                "Generates a localized hailstorm\n'Do not insert in flare gun'");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 32;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.width = 10;
            item.height = 22;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.useTurn = false;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.knockBack = 2f;
            item.value = Item.buyPrice(0, 0, 5, 0);
            item.rare = 5;
            item.shoot = ModContent.ProjectileType<Projectiles.FrostyFlare>();
            item.shootSpeed = 22f;
            item.maxStack = 999;
            item.consumable = true;
            item.Calamity().rogue = true;
        }
    }
}
