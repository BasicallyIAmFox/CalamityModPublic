﻿using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs
{
    public class TriumphBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Triumph");
            Description.SetDefault("Enemy contact damage is reduced, the lower their health the greater the reduction");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            longerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Calamity().triumph = true;
        }
    }
}
