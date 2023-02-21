﻿using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Plates;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories
{
    public class GodlySoulArtifact : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Auric Soul Artifact");
            Tooltip.SetDefault("Loyalty\n" +
                "For each Fiery Draconid you have summoned, you gain 1 minion slot");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Violet>();
            Item.value = CalamityGlobalItem.Rarity15BuyPrice;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.Calamity();
            modPlayer.auricSArtifact = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<ExodiumCluster>(25).
                AddIngredient<Plagueplate>(25).
                AddIngredient<YharonSoulFragment>(5).
                AddTile(TileID.DemonAltar).
                Register();
        }
    }
}
