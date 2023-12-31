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
        
        public class Pool : MemoryPool<Sprite, FallObjectView>, IDisposable
        {
            protected override void OnSpawned(FallObjectView item)
            {
                item.gameObject.SetActive(true);
            }

            protected override void OnDespawned(FallObjectView item)
            {
                item.gameObject.transform.position = Vector3.zero;
                item.gameObject.SetActive(false);
            }
        }
    }
    
}