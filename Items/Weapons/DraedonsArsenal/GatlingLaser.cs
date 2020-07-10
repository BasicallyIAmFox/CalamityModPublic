using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal
{
    public class GatlingLaser : ModItem
	{
		private int BaseDamage = 430;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gatling Laser");
			Tooltip.SetDefault("Large laser cannon used primarily by Yharim's fleet and base defense force\n" +
				"Highly accurate, but lacks the power to punch through defensive targets");
		}

		public override void SetDefaults()
		{
			item.width = 58;
			item.height = 24;
			item.magic = true;
			item.damage = BaseDamage;
			item.knockBack = 1f;
			item.useTime = 2;
			item.useAnimation = 2;
			item.noUseGraphic = true;
			item.autoReuse = false;
			item.channel = true;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/GatlingLaserFireStart");
			item.noMelee = true;

			item.value = Item.buyPrice(1, 80, 0, 0);
			item.rare = 10;
			item.Calamity().customRarity = CalamityRarity.RareVariant;

			item.shoot = ModContent.ProjectileType<GatlingLaserProj>();
			item.shootSpeed = 24f;
			item.useAmmo = AmmoID.Bullet;

			item.Calamity().Chargeable = true;
		}

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[item.shoot] <= 0;

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<GatlingLaserProj>(), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}

		// Disable vanilla ammo consumption
		public override bool ConsumeAmmo(Player player)
		{
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<MysteriousCircuitry>(), 15);
			recipe.AddIngredient(ModContent.ItemType<DubiousPlating>(), 15);
			recipe.AddIngredient(ModContent.ItemType<AuricBar>(), 5);
			recipe.AddIngredient(ModContent.ItemType<LaserRifle>());
			recipe.AddIngredient(ItemID.LaserMachinegun);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
