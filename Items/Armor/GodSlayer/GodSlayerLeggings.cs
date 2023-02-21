﻿using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.GodSlayer
{
    [AutoloadEquip(EquipType.Legs)]
    public class GodSlayerLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("God Slayer Leggings");
            Tooltip.SetDefault("5% increased movement speed\n" +
                "10% increased damage and critical strike chance");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = CalamityGlobalItem.Rarity14BuyPrice;
            Item.defense = 35;
            Item.rare = ModContent.RarityType<DarkBlue>();
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
            player.GetDamage<GenericDamageClass>() += 0.1f;
            player.GetCritChance<GenericDamageClass>() += 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<CosmiliteBar>(10).
                AddIngredient<AscendantSpiritEssence>(2).
                AddTile<CosmicAnvil>().
                Register();
        }
    }
}
