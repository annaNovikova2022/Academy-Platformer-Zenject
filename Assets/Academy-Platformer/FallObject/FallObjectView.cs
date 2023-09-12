using System;
using UnityEngine;
using Zenject;

namespace FallObject
{
    public class FallObjectView : MonoBehaviour
    {

        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        public event Action<Collision2D> OnCollisionEnter2DNotify; 
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter2DNotify?.Invoke(other);
        }
        
        public class Pool : MemoryPool<FallObjectView>, IDisposable
        {
            protected override void OnCreated(FallObjectView item)
            {
            
            }

            protected override void OnDestroyed(FallObjectView item)
            {
           
            }

            protected override void OnSpawned(FallObjectView item)
            {
           
            }

            protected override void OnDespawned(FallObjectView item)
            {
            
            }

            protected override void Reinitialize(FallObjectView foo)
            {
            
            }
        }
    }
    
}