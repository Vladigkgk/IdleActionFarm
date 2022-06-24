using Assets.Scripts.Plants;
using Assets.Scripts.Player.PlayerInput;
using Assets.Scripts.Stack;
using Assets.Scripts.UI.Coins;
using Assets.Scripts.Weapon;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private int _maxStack;
        [SerializeField] private Sickle _sickle;
        [SerializeField] private StackTower _stack;
        [SerializeField] private MoverCoins _moverCoins;

        private int _countStack;
        private int _coinsValue;
        private PlayerInputContoller _playerInput;
        private static int CutId => Animator.StringToHash("Cut");

        public Animator Animator { get; private set; }

        public bool CanAddToStack => _countStack >= _maxStack;
        public UnityAction<int, int> UpdateStack;
        public UnityAction<int, Transform> UpdateStackTransform;
        public UnityAction<int, float> UpdateCoinsValue;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _playerInput = GetComponent<PlayerInputContoller>();

            UpdateStack?.Invoke(_countStack, _maxStack);
            UpdateCoinsValue?.Invoke(_coinsValue , 0);
            _stack.InitTower(_maxStack, this);
        }

        public void StartCut()
        {
            if (_countStack == _maxStack) return;
            _playerInput.MoveInput(Vector2.zero);
            CutPlant();
            _sickle.Collider.enabled = true;
        }

        public void EndCut()
        {
            _sickle.Collider.enabled = false;
        }

        public void AddToStack(Transform addPosition)
        {
            _countStack += 1;
            UpdateStack?.Invoke(_countStack, _maxStack);
            UpdateStackTransform?.Invoke(_countStack, addPosition);

        }

        public void SellBlocks(Transform toSell)
        {
            _stack.OnSellBlocks(_countStack, toSell);
        }

        public void StopSell()
        {
            _stack.OnStopBlocks();
        }

        public void OnBlockSold(Transform endTransform)
        {
            _countStack -= 1;
            UpdateStack?.Invoke(_countStack, _maxStack);
            _moverCoins.SpawnCoin(endTransform, this);
        }

        public void OnCoinTransit(float timeTransit)
        {
            _coinsValue += 15;
            UpdateCoinsValue?.Invoke(_coinsValue, timeTransit);
        }

        private void CutPlant()
        {
            Animator.SetTrigger(CutId);
        }
    }
}