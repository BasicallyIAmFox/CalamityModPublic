using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon
{
    public class HivePod : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hive Pod");
            Tooltip.SetDefault("Summons an astral hive to protect you");
        }

        public override void SetDefaults()
        {
            item.damage = 90;
            item.mana = 10;
            item.summon = true;
            item.sentry = true;
            item.width = 46;
            item.height = 50;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item78;
            item.shoot = ModContent.ProjectileType<Hive>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                position = Main.MouseWorld;
                speedX = 0;
                speedY = 0;
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
                player.UpdateMaxTurrets();
            }
            return false;
        }
    }
}
