using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ButtonExtension
{
    [Flags]
    public enum ButtonState
    {
        Active = 2,
        Disable = 4,
        Pressed = 8,
        Selected = 16
    }

    public enum PointerType
    {
        PointerUp,
        PointerDown
    }

    public class Button : UIBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private PointerType _pointerType = PointerType.PointerUp;
        [SerializeField] private bool _interactable = true;

        private ButtonState _currentState;
        private bool _pressed;
        private bool _selected;

        public bool Pressed
        {
            get => _pressed;
            private set
            {
                if (_pressed == value)
                    return;

                _pressed = value;
                UpdateState();
            }
        }
        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected == value)
                    return;

                _selected = value;
                UpdateState();
            }
        }
        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (_interactable == value)
                    return;
            
                _interactable = value;
                UpdateState();
            }
        }

        private event Action Clicked;
        
        public event Action<ButtonState> StateChanged;

        protected override void Start()
        {
            UpdateState();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            UpdateState();
        }
#endif

        public void Subscribe(Action action) => 
            Clicked += action;

        public void Unsubscribe(Action action) => 
            Clicked -= action;

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
            
            if (!_interactable || _pointerType != PointerType.PointerDown)
                return;
            
            CallActions();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_interactable || _pointerType != PointerType.PointerUp)
                return;

            CallActions();
        }

        private void CallActions()
        {
            Clicked?.Invoke();
        }

        private void UpdateState()
        {
            UpdateActiveState();
            UpdatePressedState();
            UpdateSelectedState();

            StateChanged?.Invoke(_currentState);
        }

        private void UpdateActiveState()
        {
            if (Interactable)
                _currentState = _currentState & ~ButtonState.Disable | ButtonState.Active;
            else
                _currentState = _currentState & ~ButtonState.Active | ButtonState.Disable;
        }

        private void UpdatePressedState()
        {
            if (Pressed && Interactable)
                _currentState |= ButtonState.Pressed;
            else
                _currentState &= ~ButtonState.Pressed;
        }

        private void UpdateSelectedState()
        {
            if (Selected && Interactable)
                _currentState |= ButtonState.Selected;
            else
                _currentState &= ~ ButtonState.Selected;
        }
    }
}