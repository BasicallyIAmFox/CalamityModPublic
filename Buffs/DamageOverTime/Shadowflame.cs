﻿using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs
{
    public class Shadowflame : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Shadowflame");
            Description.SetDefault("Losing life");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Calamity().shadowflame = true;
        }
    }
}
