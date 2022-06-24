using Assets.Scripts.Player.PlayerInput;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.MobileInput
{
    public class MobileInputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _contrainer;
        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _handler;
        [SerializeField] private float _handlerRange;

        private PlayerInputContoller _playerInput;

        private void Start()
        {
            _playerInput = FindObjectOfType<PlayerInputContoller>();
            SetActiveState(false);
        }

        public void VirtualMoveInput(Vector2 moveDiretion)
        {
            _playerInput.MoveInput(moveDiretion);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_contrainer, eventData.position, eventData.pressEventCamera, out Vector2 position);
            UpdateJoystickPosition(position);
            SetActiveState(true);
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_background, eventData.position, eventData.pressEventCamera, out Vector2 position);

            position = GetDeltaPosition(position);

            Vector2 lerpPosition = GetLerpPosition(position);

            VirtualMoveInput(position);
            UpdateHandlerPosition(lerpPosition * _handlerRange);

            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            VirtualMoveInput(Vector2.zero);
            SetActiveState(false);
        }

        private void UpdateJoystickPosition(Vector2 position)
        {

                _background.anchoredPosition = position;
                _handler.anchoredPosition = Vector2.zero;

        }

        private void SetActiveState(bool state)
        {
            _background.gameObject.SetActive(state);
            _handler.gameObject.SetActive(state);
        }

        private Vector2 GetDeltaPosition(Vector2 position)
        {
            float x = (position.x / _background.sizeDelta.x) * 2.5f;
            float y = (position.y / _background.sizeDelta.y) * 2.5f;
            return new Vector2(x, y);
        }

        private Vector2 GetLerpPosition(Vector2 position)
        {
            return Vector2.ClampMagnitude(position, 1);
        }

        private void UpdateHandlerPosition(Vector2 position)
        {
            _handler.anchoredPosition = position;
        }
    }
}