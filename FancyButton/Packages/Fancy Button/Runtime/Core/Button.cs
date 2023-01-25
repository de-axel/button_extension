using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FancyButton.Core
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
    
    [Serializable]
    public class ButtonEditorCallback
    {
        public UnityEvent Event;
    }

    [DisallowMultipleComponent]
    [AddComponentMenu("Fancy Button/Button")]
    public class Button : UIBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private PointerType _pointerType = PointerType.PointerUp;
        [SerializeField] private bool _interactable = true;
        
        private readonly List<CanvasGroup> _canvasGroupCache = new List<CanvasGroup>();
        
        private ButtonState _currentState;
        private bool _groupsAllowInteraction = true;
        private bool _pressed;
        private bool _selected;

        [HideInInspector] public ButtonEditorCallback ButtonEditorCallback;
        [HideInInspector] public bool UseEditorCallback;
        
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

        protected override void OnCanvasGroupChanged()
        {
            // Figure out if parent groups allow interaction
            // If no interaction is alowed... then we need
            // to not do that :)
            var groupAllowInteraction = true;
            Transform t = transform;
            while (t != null)
            {
                t.GetComponents(_canvasGroupCache);
                bool shouldBreak = false;
                for (var i = 0; i < _canvasGroupCache.Count; i++)
                {
                    // if the parent group does not allow interaction
                    // we need to break
                    if (_canvasGroupCache[i].enabled && !_canvasGroupCache[i].interactable)
                    {
                        groupAllowInteraction = false;
                        shouldBreak = true;
                    }
                    // if this is a 'fresh' group, then break
                    // as we should not consider parents
                    if (_canvasGroupCache[i].ignoreParentGroups)
                        shouldBreak = true;
                }
                if (shouldBreak)
                    break;

                t = t.parent;
            }

            if (groupAllowInteraction != _groupsAllowInteraction)
            {
                _groupsAllowInteraction = groupAllowInteraction;
                Interactable = _groupsAllowInteraction;
            }
        }

        private void CallActions()
        {
            if (UseEditorCallback)
                ButtonEditorCallback.Event?.Invoke();
            
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