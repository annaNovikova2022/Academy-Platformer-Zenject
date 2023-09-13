using System;
using UnityEngine;
using Zenject;

namespace PlayerSpace
{
    public class PlayerView : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public class Factory : PlaceholderFactory<PlayerView>
        { }
    }
}