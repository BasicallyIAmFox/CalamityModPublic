using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.Calamitas;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags
{
    public class CalamitasBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<CalamitasRun3>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 9;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();

            // Materials
            DropHelper.DropItem(player, ModContent.ItemType<CalamityDust>(), 14, 18);
            DropHelper.DropItem(player, ModContent.ItemType<BlightedLens>(), 1, 3);
            DropHelper.DropItem(player, ModContent.ItemType<EssenceofChaos>(), 5, 9);
            DropHelper.DropItemCondition(player, ModContent.ItemType<Bloodstone>(), CalamityWorld.downedProvidence, 35, 45);

            // Weapons
            DropHelper.DropItemChance(player, ModContent.ItemType<TheEyeofCalamitas>(), 3);
            DropHelper.DropItemChance(player, ModContent.ItemType<Animosity>(), 3);
            DropHelper.DropItemChance(player, ModContent.ItemType<CalamitasInferno>(), 3);
            DropHelper.DropItemChance(player, ModContent.ItemType<BlightedEyeStaff>(), 3);

            // Equipment
            DropHelper.DropItem(player, ModContent.ItemType<CalamityRing>());
            DropHelper.DropItemChance(player, ModContent.ItemType<ChaosStone>(), 10);

            // Vanity
            DropHelper.DropItemChance(player, ModContent.ItemType<CalamitasMask>(), 7);
        }
    }
}
