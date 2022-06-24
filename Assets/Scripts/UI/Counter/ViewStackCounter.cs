using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.UI.Counter
{
    public class ViewStackCounter : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private TMP_Text _maxValue;
        [SerializeField] private TMP_Text _currentValue;

        private void Awake()
        {
            _player.UpdateStack += UpdateView;
        }

        private void UpdateView(int currentValue, int maxValue)
        {
            _currentValue.text = currentValue.ToString();
            _maxValue.text = maxValue.ToString();
        }

        private void OnDestroy()
        {
            _player.UpdateStack += UpdateView;
        }
    }
}