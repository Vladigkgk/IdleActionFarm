using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Plants
{
    public class Plant : MonoBehaviour
    {
        [SerializeField] private float _timeCultivate = 10f;

        private Tween _cultivating;
        private float _scaleY;
        private Collider _collider;
        private MeshRenderer _meshRenderer;
        private GameObject _rootPlant;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();

            _scaleY = transform.localScale.y;
        }

        public void StartCultivate()
        {
            _cultivating = transform.DOScaleY(_scaleY, _timeCultivate);
            _meshRenderer.enabled = true;
            _cultivating.onComplete += CultivateComplete;
        }

        public void SetRootPlant(GameObject rootPlant)
        {
            _rootPlant = rootPlant;            
        }

        private void CultivateComplete()
        {
            _collider.enabled = true;
            Destroy(_rootPlant.gameObject);
            _cultivating.onComplete -= CultivateComplete;
            _cultivating = null;
        }

        public void DisableColliderAndMeshRenderer()
        {
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            transform.localScale = new Vector3(transform.localScale.x, 0f, transform.localScale.z);
        }
    }
}