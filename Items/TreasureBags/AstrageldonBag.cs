using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Mounts;
using CalamityMod.Items.PermanentBoosters;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags
{
    public class AstrageldonBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<AstrumAureus>();

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
            item.expert = true;
            item.rare = ItemRarityID.Cyan;
        }

        public override bool CanRightClick() => true;

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();

            // Materials
            DropHelper.DropItem(player, ModContent.ItemType<AstralJelly>(), 12, 16);
            DropHelper.DropItem(player, ModContent.ItemType<Stardust>(), 30, 40);
            DropHelper.DropItem(player, ItemID.FallenStar, 20, 30);

            // Weapons
            float w = DropHelper.BagWeaponDropRateFloat;
            DropHelper.DropEntireWeightedSet(player,
                DropHelper.WeightStack<Nebulash>(w),
                DropHelper.WeightStack<AuroraBlazer>(w),
                DropHelper.WeightStack<AlulaAustralis>(w),
                DropHelper.WeightStack<BorealisBomber>(w),
                DropHelper.WeightStack<AuroradicalThrow>(w)
            );

            // Equipment
            DropHelper.DropItemCondition(player, ModContent.ItemType<SquishyBeanMount>(), NPC.downedMoonlord);
            DropHelper.DropItem(player, ModContent.ItemType<GravistarSabaton>());
            DropHelper.DropItemChance(player, ModContent.ItemType<LeonidProgenitor>(), 0.1f);

            // Vanity
            DropHelper.DropItemChance(player, ModContent.ItemType<AureusMask>(), 7);

            // Other
            DropHelper.DropItemCondition(player, ModContent.ItemType<StarlightFuelCell>(), CalamityWorld.revenge && !player.Calamity().adrenalineBoostTwo);
        }
    }
}
