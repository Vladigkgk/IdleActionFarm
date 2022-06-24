using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Zones
{
    public class SellZone : MonoBehaviour
    {
        [SerializeField] private Transform _toSell;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player)) 
            {
                player.SellBlocks(_toSell);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                player.StopSell();
            }
        }
    }
}