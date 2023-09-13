using System;
using UnityEngine;
using Zenject;

namespace FallObject
{
    public class FallObjectView : MonoBehaviour
    {

        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public FallObjectModel Model => _model;
        
        public event Action<Collision2D> OnCollisionEnter2DNotify; 
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private FallObjectModel _model;

        
        public FallObjectConfig ObjectConfig => _objectConfig;

        private FallObjectConfig _objectConfig;
        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter2DNotify?.Invoke(other);
        }
        
        public class Pool : MemoryPool<FallObjectView>, IDisposable
        {
            /*protected void OnCreated(FallObjectView item)
            {

            }

            protected void OnDestroyed(FallObjectView item)
            {
           
            }*/  //Где-то как-то надо выбрать тип

            protected void OnSpawned(FallObjectView item)
            {
                item.gameObject.SetActive(true);
            }

            protected void OnDespawned(FallObjectView item)
            {
                item.gameObject.SetActive(false);
                
            }

            /*protected void Reinitialize(FallObjectView foo)
            {
            
            }*/
        }
    }
    
}