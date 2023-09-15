using System;
using System.Collections.Generic;

namespace Sounds
{
    public class SoundController
    {
        private SoundView.Pool _soundPool;
        private SoundConfig _soundConfig;

        public SoundController(SoundView.Pool soundPool,
            SoundConfig soundConfig)
        {
            _soundPool = soundPool;
            _soundConfig = soundConfig;
        }
        
        public void Play(SoundName soundName, float volume = 1, bool loop = false)
        {
            SwitchOff();

            var model = _soundConfig.Get(soundName);
            var sound = _soundPool.Spawn(model);

            sound.AudioSource.loop = loop;
            sound.AudioSource.volume = volume;
            sound.AudioSource.Play();
        }

        public void SwitchOff()
        {
            _soundPool.DisableCompletedSounds();
        }

        public void Stop()
        {
            _soundPool.MuteSound();
        }
        
        
    }
}