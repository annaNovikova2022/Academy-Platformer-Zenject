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
                item.AudioSource.clip = null;
                item.gameObject.SetActive(false);
            }
            protected override void Reinitialize(SoundModels soundModel, SoundView item)
            {
                item.gameObject.SetActive(true);
                item.AudioSource.clip = soundModel.Clip;
            }
        }
    }
}