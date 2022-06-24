using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using TMPro;

using UnityEngine.UI;

namespace Assets.Scripts.UI.Coins
{
    public class ViewCoinsValue : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private TMP_Text _coinsValue;
        [SerializeField] private Image _background;

        private void Awake()
        {
            _player.UpdateCoinsValue += OnUpdateCoinsValue;
        }

        private void OnUpdateCoinsValue(int coinsValue, float timeTransit)
        {
            _coinsValue.text = coinsValue.ToString();
        }

        private void OnDestroy()
        {
            _player.UpdateCoinsValue -= OnUpdateCoinsValue;

        }

    }
}