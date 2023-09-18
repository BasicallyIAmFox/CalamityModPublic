﻿using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon
{
    [LegacyName("YoungDuke")]
    public class MutatedTruffleSummon : BaseMinionProjectile
    {
        public override int AssociatedProjectileTypeID => ModContent.ProjectileType<MutatedTruffleSummon>();
        public override int AssociatedBuffTypeID => ModContent.BuffType<MutatedTruffleBuff>();
        public override ref bool AssociatedMinionBool => ref ModdedOwner.MutatedTruffleBool;
        public override float MinionSlots => 3f;
        public override float EnemyDistanceDetection => MutatedTruffle.EnemyDistanceDetection;

        public enum AIState { Idle, Dashing, Toothball, Vortex }
        public AIState State { get => (AIState)Projectile.ai[0]; set => Projectile.ai[0] = (int)value; }
        public ref float AITimer => ref Projectile.ai[1];

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[Type] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = Projectile.height = 82;
        }

        public override void MinionAI()
        {
            switch (State)
            {
                case AIState.Idle:
                    IdleState();
                    break;
                case AIState.Dashing:
                    DashingState();
                    break;
                case AIState.Toothball:
                    ToothballState();
                    break;
                case AIState.Vortex:
                    VortexState();
                    break;
            }
        }

        #region AI Methods

        private void IdleState()
        {
            // The minion will hover around the owner.
            if (!Projectile.WithinRange(Owner.Center, 320f))
            {
                Projectile.velocity = (Projectile.velocity + Projectile.SafeDirectionTo(Owner.Center)) * 0.9f;
                SyncVariables();
            }

            // The minion will teleport on the owner if they get far enough.
            if (!Projectile.WithinRange(Owner.Center, 1200f))
            {
                Projectile.Center = Owner.Center;
                SyncVariables();
            }

            Projectile.rotation = 0f;
            Projectile.spriteDirection = MathF.Sign(Projectile.velocity.X);

            if (Target is not null)
                SwitchState(AIState.Dashing);
        }

        private void DashingState()
        {
            if (Target is not null)
            {
                if (!Projectile.WithinRange(Target.Center, 480f))
                {
                    float inertia = 4f;
                    Projectile.velocity = (Projectile.velocity * inertia + CalamityUtils.CalculatePredictiveAimToTarget(Projectile.Center, Target, MutatedTruffle.DashSpeed)) / (inertia + 1f);

                    Projectile.rotation = Projectile.velocity.ToRotation();
                    Projectile.spriteDirection = MathF.Sign(Target.Center.X - Projectile.Center.X);

                    SyncVariables();
                }
                else if (Projectile.velocity.Length() < MutatedTruffle.DashSpeed - 15f)
                {
                    Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTarget(Projectile.Center, Target, MutatedTruffle.DashSpeed - 10f);

                    Projectile.rotation = Projectile.velocity.ToRotation();
                    Projectile.spriteDirection = MathF.Sign(Target.Center.X - Projectile.Center.X);

                    SyncVariables();
                }

                AITimer++;

                if (AITimer > MutatedTruffle.DashTime)
                    SwitchState(AIState.Toothball);
            }
            else
                SwitchState(AIState.Idle);
        }

        private void ToothballState()
        {
            if (Target is not null)
            {
                Vector2 targetDirection = Projectile.SafeDirectionTo(Target.Center);
                float targettingSpeed = 30f;
                float inertia = 18f;

                if (!Projectile.WithinRange(Target.Center, 480f))
                {
                    Projectile.velocity = (Projectile.velocity * inertia + targetDirection * targettingSpeed) / (inertia + 1f);
                    SyncVariables();
                }

                if (Projectile.WithinRange(Target.Center, 400f))
                {
                    Projectile.velocity = (Projectile.velocity * inertia + -targetDirection * targettingSpeed) / (inertia + 1f);
                    SyncVariables();
                }

                if (AITimer % MutatedTruffle.ToothballFireRate == 0f)
                {
                    if (Main.myPlayer == Projectile.owner)
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, CalamityUtils.CalculatePredictiveAimToTarget(Projectile.Center, Target, MutatedTruffle.ToothballSpeed), ModContent.ProjectileType<MutatedTruffleToothball>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Target.whoAmI);

                    if (!Main.dedServ)
                    {
                        Dust.NewDustPerfect(Projectile.Right.RotatedBy(Projectile.rotation), 7, Projectile.rotation.ToRotationVector2().RotatedByRandom(MathHelper.PiOver4 / 2) * Main.rand.NextFloat(1f, 3f));

                        SoundEngine.PlaySound(SoundID.NPCDeath13 with { Volume = .5f, PitchVariance = .1f }, Projectile.Center);
                    }

                    SyncVariables();
                }

                Projectile.rotation = Projectile.SafeDirectionTo(Target.Center).ToRotation();
                Projectile.spriteDirection = MathF.Sign(Target.Center.X - Projectile.Center.X);

                AITimer++;

                if (AITimer > MutatedTruffle.ToothballsUntilNextState * MutatedTruffle.ToothballFireRate)
                    SwitchState(AIState.Vortex);
            }
            else
                SwitchState(AIState.Idle);
        }

        private void VortexState()
        {
            if (Target is not null)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj is null || !proj.active || proj.type != ModContent.ProjectileType<MutatedTruffleVortex>() || proj.owner != Owner.whoAmI || proj.timeLeft < MutatedTruffle.VortexTimeUntilNextState)
                        continue;

                    Vector2 spinPosition = proj.Center + -Vector2.UnitY.RotatedBy(Main.GlobalTimeWrappedHourly * 6f) * 400f;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.SafeDirectionTo(spinPosition) * 30f, .4f);
                }

                Projectile.rotation = Projectile.velocity.ToRotation();

                AITimer++;

                if (AITimer >= MutatedTruffle.VortexTimeUntilNextState)
                    SwitchState(AIState.Dashing);
            }
            else
                SwitchState(AIState.Idle);
        }

        private void SwitchState(AIState state)
        {
            State = state;
            AITimer = 0f;

            Projectile.spriteDirection = 1;

            if (state == AIState.Idle && !Main.dedServ)
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/OldDukeHuff") with { Volume = .5f, Pitch = .1f }, Projectile.Center);

            if ((state == AIState.Dashing || state == AIState.Toothball) && !Main.dedServ)
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/OldDukeRoar") with { Volume = .3f, Pitch = .1f }, Projectile.Center);

            if (state == AIState.Vortex)
            {
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Target.Center, Vector2.Zero, ModContent.ProjectileType<MutatedTruffleVortex>(), (int)(Projectile.damage * 1.25f), Projectile.knockBack, Owner.whoAmI);

                if (!Main.dedServ)
                    SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/OldDukeVomit") with { Volume = .4f, Pitch = .1f }, Projectile.Center);
            }

            SyncVariables();
        }

        private void SyncVariables()
        {
            Projectile.netUpdate = true;
            Projectile.netSpam = 0;
        }

        #endregion

        public override bool? CanDamage() => (State == AIState.Dashing) ? null : false;

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            Rectangle frame = texture.Frame(1, Main.projFrames[Type], 0, Projectile.frame);
            Vector2 origin = frame.Size() * 0.5f;
            float drawRotation = Projectile.rotation + (Projectile.spriteDirection == -1 && State != AIState.Idle ? MathHelper.Pi : 0f);
            SpriteEffects effects = (Projectile.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            if (CalamityConfig.Instance.Afterimages && State == AIState.Dashing)
            {
                for (int i = 0; i < Projectile.oldPos.Length; i++)
                {
                    Color afterimageDrawColor = Color.Green with { A = 25 } * Projectile.Opacity * (1f - i / (float)Projectile.oldPos.Length);
                    Vector2 afterimageDrawPosition = Projectile.oldPos[i] + Projectile.Size * 0.5f - Main.screenPosition;
                    Main.EntitySpriteDraw(texture, afterimageDrawPosition, frame, afterimageDrawColor, drawRotation, origin, Projectile.scale, effects, 0);
                }
            }

            Main.EntitySpriteDraw(texture, drawPosition, frame, Projectile.GetAlpha(lightColor), drawRotation, origin, Projectile.scale, effects, 0);

            return false;
        }
    }
}
