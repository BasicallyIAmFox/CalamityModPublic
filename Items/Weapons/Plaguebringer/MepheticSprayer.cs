using Terraria;
using CalamityMod.Projectiles;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityMod.Items
{
    public class MepheticSprayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blight Spewer");
        }

        public override void SetDefaults()
        {
            item.damage = 110;
            item.ranged = true;
            item.width = 76;
            item.height = 36;
            item.useTime = 10;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2f;
            item.UseSound = SoundID.Item34;
            item.value = Item.buyPrice(0, 80, 0, 0);
            item.rare = 8;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<CorossiveFlames>();
            item.shootSpeed = 7.5f;
            item.useAmmo = 23;
        }
    }
}
