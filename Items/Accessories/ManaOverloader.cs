﻿using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories
{
    public class ManaOverloader : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mana Polarizer");
            Tooltip.SetDefault("Increases max mana by 50 and magic damage by 6%\n" +
                               "Life regen lowered by 3 if mana is above 50% of its maximum\n" +
                               "Grants spectre healing, the amount healed scales with your mana\n" +
							   "The more mana you have, the more you heal");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = Item.buyPrice(0, 15, 0, 0);
            item.rare = 9;
            item.accessory = true;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.Calamity();
            modPlayer.manaOverloader = true;
            player.statManaMax2 += 50;
        }
    }
}
