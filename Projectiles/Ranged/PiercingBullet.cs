﻿using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
namespace CalamityMod.Projectiles.Ranged
{
    public class PiercingBullet : ModProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/Ranged/AMRShot";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Piercing Blow");
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.light = 0.5f;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 10;
            Projectile.scale = 1.18f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            AIType = ProjectileID.BulletHighVelocity;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.Calamity().pointBlankShotDuration = CalamityGlobalProjectile.DefaultPointBlankDuration;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            //Avoid touching things that you probably aren't meant to damage
            if (target.defense > 999 || target.Calamity().DR >= 0.95f || target.Calamity().unbreakableDR)
                return;

            //DR applies after defense, so undo it first
            damage = (int)(damage * (1 / (1 - target.Calamity().DR)));

            //Then proceed to ignore all defense
            int penetratableDefense = (int)Math.Max(target.defense - Main.player[Projectile.owner].GetArmorPenetration<GenericDamageClass>(), 0);
            int penetratedDefense = Math.Min(penetratableDefense, target.defense);
            damage += (int)(0.5f * penetratedDefense);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            OnHitEffects(target.Center, crit);
            target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            OnHitEffects(target.Center, crit);
            target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 600);
        }

        private void OnHitEffects(Vector2 targetPos, bool crit)
        {
            if (crit)
            {
                var source = Projectile.GetSource_FromThis();
                int bulletCount = 10;
                for (int x = 0; x < bulletCount; x++)
                {
                    if (Projectile.owner == Main.myPlayer)
                    {
                        CalamityUtils.ProjectileBarrage(source, Projectile.Center, targetPos, x < bulletCount / 2, 500f, 500f, 0f, 500f, 12f, ModContent.ProjectileType<AMR2>(), (int)(Projectile.damage * 0.2), Projectile.knockBack, Projectile.owner);
                    }
                }
            }
        }
    }
}
