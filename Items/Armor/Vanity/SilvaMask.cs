using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class SilvaMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silva Mask");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 26;
            item.value = Item.buyPrice(0, 8, 0, 0);
            item.vanity = true;
            item.rare = ItemRarityID.Purple;
            item.Calamity().customRarity = CalamityRarity.DarkBlue;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SilvaArmor>() && legs.type == ModContent.ItemType<SilvaLeggings>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
    }
}
