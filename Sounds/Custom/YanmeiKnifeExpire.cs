using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Sounds.Custom
{
    public class YanmeiKnifeExpire : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            soundInstance = sound.CreateInstance();
            soundInstance.Volume = volume;
            soundInstance.Pan = pan;
            Main.PlaySoundInstance(soundInstance);
            return soundInstance;
        }
    }
}
