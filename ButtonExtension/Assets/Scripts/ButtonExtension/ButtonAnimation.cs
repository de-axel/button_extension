using ButtonExtension.Data;
using UnityEngine;

namespace ButtonExtension
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonAnimation<T> : MonoBehaviour where T : ButtonSettings
    {
        [SerializeField] private Button _button;
        [SerializeField] private T _buttonSettings;

        protected T Settings => _buttonSettings;

        protected virtual void Reset() => 
            _button = GetComponent<Button>();

        protected virtual void Awake() => 
            _button.StateChanged += OnStateChanged;

        protected abstract void OnStateChanged(ButtonState buttonState);

        protected virtual void OnDestroy() => 
            _button.StateChanged -= OnStateChanged;
    }
}