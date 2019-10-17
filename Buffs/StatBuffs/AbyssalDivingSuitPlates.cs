﻿using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs
{
    public class AbyssalDivingSuitPlates : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Abyssal Diving Suit Plates");
            Description.SetDefault("The plates will absorb 15% damage");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Calamity().abyssalDivingSuitPlates = true;
        }
    }
}
