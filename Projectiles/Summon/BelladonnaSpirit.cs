﻿using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon
{
    public class BelladonnaSpirit : ModProjectile
    {
        public Player Owner => Main.player[Projectile.owner];

        public CalamityPlayer moddedOwner => Owner.Calamity();

        public ref float PetalFireTimer => ref Projectile.ai[0];

        public bool CheckForSpawning = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Belladonna Spirit");
            Main.projFrames[Projectile.type] = 5;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;

            Projectile.width = 28;
            Projectile.height = 48;

            Projectile.DamageType = DamageClass.Summon;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            NPC potentialTarget = Projectile.Center.MinionHoming(1200f, Owner);
            if (potentialTarget is not null)
                TargetNPC();

            CheckMinionExistance(); // Checks if the minion can still exist.
            SpawnEffect(); // Makes a dust effect where the minion spawns.
            DoAnimation(); // Does the animation of the minion.
            PointInDirection(potentialTarget); // Points in the right directions.
            FollowPlayer(); // Vibes around the player.
            Projectile.MinionAntiClump(); // The minions push eachother to not clump
        }

        #region Methods

        public void CheckMinionExistance()
        {
            Owner.AddBuff(ModContent.BuffType<BelladonnaSpiritBuff>(), 1);
            if (Projectile.type == ModContent.ProjectileType<BelladonnaSpirit>())
            {
                if (Owner.dead)
                    moddedOwner.belladonaSpirit = false;
                if (moddedOwner.belladonaSpirit)
                    Projectile.timeLeft = 2;
            }
        }

        public void SpawnEffect()
        {
            if (CheckForSpawning == false)
            {
                for (int i = 0; i < 45; i++)
                {
                    float angle = MathHelper.TwoPi / 45f * i;
                    Vector2 velocity = angle.ToRotationVector2() * 4f;
                    Dust spawnDust = Dust.NewDustPerfect(Projectile.Center + velocity * 2.75f, 39, velocity);
                    spawnDust.noGravity = true;
                    spawnDust.scale = velocity.Length() * 0.1f;
                    spawnDust.velocity *= 0.3f;
                }
                CheckForSpawning = true;
            }
        }

        public void DoAnimation()
        {
            Projectile.frameCounter++;
            Projectile.frame = Projectile.frameCounter / 5 % Main.projFrames[Projectile.type];
        }

        public void PointInDirection(NPC target)
        {
            if (target is not null) // If there's a target look at the target.
            {
                Projectile.spriteDirection = ((target.Center.X - Projectile.Center.X) < 0).ToDirectionInt();
            }
            else // If there's not a target, the minion just points where it's going.
            {
                if (Math.Abs(Projectile.velocity.X) > 0.01f)
                    Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X);
            }
        }

        public void FollowPlayer()
        {
            if (Projectile.WithinRange(Owner.Center, 1200f) && !Projectile.WithinRange(Owner.Center, 300f)) // If the minion starts to get far, force the minion to go to you.
            {
                Projectile.velocity = (Owner.Center - Projectile.Center) / 30f;
                Projectile.netUpdate= true;
            }            
            else if (!Projectile.WithinRange(Owner.Center, 160f)) // The minion will change directions to you if it's going away from you, meaning it'll just hover around you.
            {
                Projectile.velocity = (Projectile.velocity * 37f + Projectile.SafeDirectionTo(Owner.Center) * 17f) / 40f;
                Projectile.netUpdate = true;
            }

            // Teleport to the owner if sufficiently far away.
            if (!Projectile.WithinRange(Owner.Center, 1200f))
            {
                Projectile.position = Owner.Center;
                Projectile.velocity *= 0.3f;
                Projectile.netUpdate = true;
            }
        }

        public void TargetNPC()
        {
            PetalFireTimer++;
            Projectile.velocity.Y -= MathHelper.Lerp(0, 0.005f, PetalFireTimer % 75f); 
            // The minion will slowly go up until it throws the petal.
            // This essentially makes the minion stay above you and trigger the "Turn back to player", it'll do this continuously, giving the effect of bouncing.
            PetalFireTimer = (PetalFireTimer == 76f) ? 1f : PetalFireTimer; // The timer cannot go above 75f.
            if (Main.myPlayer == Projectile.owner)
                FirePetals();
            Projectile.netUpdate = true;
        }

        public void FirePetals()
        {
            int petalID = ModContent.ProjectileType<BelladonnaPetal>();
            if (PetalFireTimer % 75f == 0f) // Every 75 frames, throws a petal.
            {
                Vector2 petalShootVelocity = (-Vector2.UnitY * Main.rand.NextFloat(7f, 8.5f)) +
                    Vector2.UnitX * Projectile.velocity.X +
                    Vector2.UnitY * Projectile.velocity.Y * 0.35f;
                // Throws the petal upwards with a random force and inherits the minion's speed.
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, petalShootVelocity, petalID, Projectile.damage, Projectile.knockBack, Projectile.owner);
                if (Main.projectile.IndexInRange(p))
                {
                    Main.projectile[p].originalDamage = Projectile.originalDamage;
                    Main.projectile[p].rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
                }
                Projectile.netUpdate = true;
            }
        }

        public override bool? CanDamage() => false;

        #endregion
    }
}
