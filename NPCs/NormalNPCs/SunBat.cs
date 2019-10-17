﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityMod.Buffs;
using CalamityMod.Items;
namespace CalamityMod.NPCs
{
    public class SunBat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sun Bat");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.lavaImmune = true;
            npc.aiStyle = 14;
            aiType = 151;
            npc.damage = 35;
            npc.width = 26;
            npc.height = 20;
            npc.defense = 10;
            npc.lifeMax = 120;
            npc.knockBackResist = 0.65f;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath4;
            banner = npc.type;
            bannerItem = ModContent.ItemType<SunBatBanner>();
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            npc.spriteDirection = (npc.direction > 0) ? 1 : -1;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.playerSafe || !Main.hardMode || spawnInfo.player.Calamity().ZoneAbyss ||
                spawnInfo.player.Calamity().ZoneSunkenSea)
            {
                return 0f;
            }
            return SpawnCondition.Underground.Chance * 0.12f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 120, true);
            player.AddBuff(ModContent.BuffType<HolyFlames>(), 120, true);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 64, hitDirection, -1f, 0, default, 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 64, hitDirection, -1f, 0, default, 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.NextBool(3))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EssenceofCinder>());
            }
        }
    }
}
