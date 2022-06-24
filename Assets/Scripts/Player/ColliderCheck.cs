using Assets.Scripts.Plants;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Player
{
    public class ColliderCheck : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Plant plant))
            {
                _player.StartCut();
            }

            if (other.TryGetComponent(out SlicePlant cutPlant))
            {
                if (_player.CanAddToStack) return;
                _player.AddToStack(cutPlant.transform);
                Destroy(cutPlant.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            
            if (other.TryGetComponent(out Plant plant))
            {
                _player.EndCut();
            }
        }
    }
}