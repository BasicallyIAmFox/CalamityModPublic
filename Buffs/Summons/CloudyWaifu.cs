﻿using CalamityMod.CalPlayer;
using Terraria;
using CalamityMod.Projectiles;
using Terraria.ModLoader;

namespace CalamityMod.Buffs
{
    public class CloudyWaifu : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cloud Elemental");
            Description.SetDefault("The cloud elemental will protect you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            CalamityPlayer modPlayer = player.Calamity();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<CloudElementalMinion>()] > 0)
            {
                modPlayer.cWaifu = true;
            }
            if (!modPlayer.cWaifu)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}
