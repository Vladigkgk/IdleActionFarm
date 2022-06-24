using System.Collections;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Player;

namespace Assets.Scripts.Stack
{
    public class StackBlock : MonoBehaviour
    {
        private Vector3 _oldPosition;
        private Tween _transition;
        private PlayerController _player;

        public void TransitToSell(Transform toSell, float transitTime, PlayerController player)
        {
            _player = player;

            _oldPosition = transform.localPosition;
            _transition = transform.DOMove(toSell.position, transitTime);

            _transition.onComplete += TransitToSellComplete;

        }

        public void TransitToStack(Vector3 endPostion, float transitTime)
        {
            transform.DOLocalMove(endPostion, transitTime);
        }

        private void TransitToSellComplete()
        {
            _player.OnBlockSold(transform);
            transform.localPosition = _oldPosition;
            _transition.onComplete -= TransitToSellComplete;
            _transition = null;
            gameObject.SetActive(false);
        }
    }
}