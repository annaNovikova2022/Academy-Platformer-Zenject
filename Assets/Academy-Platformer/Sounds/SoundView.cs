using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Sounds
{
    public class SoundView : MonoBehaviour
    {
        public AudioSource AudioSource => audioSource;

        [SerializeField] private AudioSource audioSource;

        public class Pool : MemoryPool<SoundModels, SoundView>
        {
             private List<SoundView> _views = new List<SoundView>();

            protected override void OnCreated(SoundView item)
            {
                _views.Add(item);
            }
            protected override void OnDespawned(SoundView item)
            {
                item.gameObject.SetActive(false);
                item.AudioSource.clip = null;
            }
            protected override void Reinitialize(SoundModels soundModel, SoundView item)
            {
                item.gameObject.SetActive(true);
                item.AudioSource.clip = soundModel.Clip;
            }
            public void DisableCompletedSounds()
            {
                foreach (var soundView in _views)
                {
                    if (!soundView.AudioSource.isPlaying && soundView.gameObject.activeInHierarchy)
                    {
                        Despawn(soundView);
                    }
                }
            }
            public void MuteSound()
            {
                foreach (var soundView in _views)
                {
                    if (soundView.gameObject.activeInHierarchy)
                    {
                        Despawn(soundView);
                    }
                }
            }
        }
    }
}