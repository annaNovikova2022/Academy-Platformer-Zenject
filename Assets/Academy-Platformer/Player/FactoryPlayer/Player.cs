using System;
using PlayerSpace;
using UnityEngine;
using Zenject;

namespace PlayerSpace
{
    public class Player
    {
        private PlayerView _playerPrefab;

        public Player(PlayerView playerView)
        {
            _playerPrefab = playerView;
            playerView = GameObject.Instantiate(_playerPrefab);
        }
        
        public class Factory : PlaceholderFactory<PlayerView, Player>
        { }
    }
}
