using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Coins
{
    public class MoverCoins : MonoBehaviour
    {
        [SerializeField] private Coin _coin;
        [SerializeField] private float _transitTime;

        public void SpawnCoin(Transform startPosition, PlayerController player)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(startPosition.position);
            Coin coin = Instantiate(_coin, position, Quaternion.identity, transform);
            coin.TransitTo(transform, _transitTime, player);
        }
    }
}