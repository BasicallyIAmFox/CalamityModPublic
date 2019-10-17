﻿using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs
{
    public class MarkedforDeath : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Marked");
            Description.SetDefault("Damage reduction reduced");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.Calamity().marked = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Calamity().marked = true;
        }
    }
}
