using UnityEngine;
using EzySlice;
using Assets.Scripts.Plants;

namespace Assets.Scripts.Weapon
{
    public class Sickle : MonoBehaviour
    {
        [SerializeField] private float _force = 5f;
        [SerializeField] private ParticleSystem _hitParticle;

        public Collider Collider { get; private set; }

        private void Awake()
        {
            Collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Plant plant)) return;
            _hitParticle.gameObject.SetActive(true);
            MeshRenderer renderer = plant.GetComponent<MeshRenderer>(); 
            GameObject[] cuttingGO = SliceGO(other.gameObject, renderer.material);
            plant.DisableColliderAndMeshRenderer();
            AddCutPlant(cuttingGO[0]);
            cuttingGO[1].transform.SetParent(plant.transform);
            plant.SetRootPlant(cuttingGO[1]);
            plant.StartCultivate();
        }

        private GameObject[] SliceGO(GameObject objectToSlice, Material crossMaterial = null)
        {
            Vector3 position = new Vector3(transform.position.x, objectToSlice.transform.position.y - objectToSlice.transform.position.y / 10f, transform.position.z);
            return objectToSlice.SliceInstantiate(position, Vector3.up, crossMaterial);
        }

        private void AddCutPlant(GameObject cutPlant)
        {
            MeshCollider meshCollider = cutPlant.AddComponent<MeshCollider>();
            meshCollider.convex = true;

            Rigidbody rigidbody = cutPlant.AddComponent<Rigidbody>();
            rigidbody.useGravity = true;

            rigidbody.AddForce(Vector3.forward * _force, ForceMode.Force);

            cutPlant.AddComponent<SlicePlant>();
        }
    }
}