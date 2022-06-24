using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Plants
{
    public class SlicePlant : MonoBehaviour
    {
        public bool CanTake { get; private set; }

        private void OnCollisionStay(Collision collision)
        {
            CanTake = true;
        }
    }
}