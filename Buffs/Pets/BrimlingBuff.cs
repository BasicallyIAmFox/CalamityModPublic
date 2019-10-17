using Terraria;
using CalamityMod.Projectiles;
using Terraria.ModLoader;

namespace CalamityMod.Buffs
{
    public class BrimlingBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Brimling");
            Description.SetDefault("Protect her or suffer in agony");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.Calamity().brimling = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<BrimlingPet>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<BrimlingPet>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
