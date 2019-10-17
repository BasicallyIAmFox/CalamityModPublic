﻿using Terraria;
using CalamityMod.Projectiles;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityMod.Items
{
    public class Levi : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Levi");
            Tooltip.SetDefault("Summons a baby Leviathan pet");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ZephyrFish);
            item.shoot = ModContent.ProjectileType<LeviPet>();
            item.buffType = ModContent.BuffType<Buffs.Levi>();
            item.rare = 10;
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
