using System.Collections;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Player;

namespace Assets.Scripts.UI.Coins
{
    public class Coin : MonoBehaviour
    {
        private Tween _transition;
        private PlayerController _player;
        private float _timeTransit;

        public void TransitTo(Transform endPosition, float transitTime, PlayerController player)
        {
            _player = player;
            _timeTransit = transitTime;
            _transition = transform.DOMove(endPosition.position, transitTime);
            _transition.onComplete += TransitComplete;
        }

        private void TransitComplete()
        {
            _transition.onComplete += TransitComplete;
            _player.OnCoinTransit(_timeTransit);
            Destroy(gameObject);
        }
    }
}