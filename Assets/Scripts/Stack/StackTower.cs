using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Stack
{
    public class StackTower : MonoBehaviour
    {
        [SerializeField] private StackRow _stackRow;
        [SerializeField] private float _waitingTime;
        [SerializeField] private float _transitTimeToSell;
        [SerializeField] private float _transitTimeToStack;

        private List<StackBlock> _blocksInTower;
        private Coroutine _coroutine;
        private PlayerController _player;


        public void InitTower(int maxStack, PlayerController player)
        {
            _blocksInTower = new List<StackBlock>();
            int countRow = maxStack / _stackRow.GetComponentsInChildren<StackBlock>().Length + 1;
            Vector3 spawnPosition = transform.position;
            for (int i = 0; i < countRow; i++)
            {
                StackRow spawnedRow = Instantiate(_stackRow, spawnPosition, Quaternion.identity, transform);
                var blocks = spawnedRow.InitRow();
                _blocksInTower.AddRange(blocks);

                spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + spawnedRow.transform.lossyScale.y / 4f, spawnPosition.z);
            }

            DisableAllBlocks();
            _player = player;
            _player.UpdateStackTransform += OnUpdateStack;
        }

        
        private void DisableAllBlocks()
        {
            foreach (var block in _blocksInTower)
            {
                block.gameObject.SetActive(false);
            }
        }

        private void OnUpdateStack(int currentValue, Transform addPosition)
        {
            if (currentValue == 0) return;
            StackBlock block = _blocksInTower[currentValue - 1];
            Vector3 localPosition = block.transform.localPosition;
            block.transform.position = addPosition.position;
            block.gameObject.SetActive(true);
            block.TransitToStack(localPosition, _transitTimeToStack);

        }

        public void OnSellBlocks(int currentValue, Transform toSell)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(SellingBlocks(currentValue, toSell));
        }

        public void OnStopBlocks()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = null;

        }

        private IEnumerator SellingBlocks(int currentValue, Transform toSell)
        {

            for (int i = currentValue - 1 ; i >= 0; i--)
            {
                _blocksInTower[i].TransitToSell(toSell, _transitTimeToSell, _player);
                yield return new WaitForSeconds(_waitingTime);
            }
        }
    }
}