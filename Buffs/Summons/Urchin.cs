﻿using CalamityMod.CalPlayer;
using CalamityMod.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs
{
    public class UrchinBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Urchin");
            Description.SetDefault("The urchin will protect you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            CalamityPlayer modPlayer = player.Calamity();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Urchin>()] > 0)
            {
                modPlayer.vUrchin = true;
            }
            if (!modPlayer.vUrchin)
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
