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
        
        public class Pool : MemoryPool<IMemoryPool>, IDisposable
        {
            protected void OnCreated(FallObjectView item)
            {
            
            }

            protected void OnDestroyed(FallObjectView item)
            {
           
            }

            protected void OnSpawned(FallObjectView item)
            {
           
            }

            protected void OnDespawned(FallObjectView item)
            {
            
            }

            protected void Reinitialize(FallObjectView foo)
            {
            
            }
        }
    }
    
}