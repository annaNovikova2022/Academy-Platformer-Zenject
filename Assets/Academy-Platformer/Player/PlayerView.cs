using System;
using UnityEngine;
using Zenject;

namespace PlayerSpace
{
    public class PlayerView : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Start()
        {
            gameObject.SetActive(false);
        }
        
        public class Factory : PlaceholderFactory<PlayerView>
        { }
    }
}