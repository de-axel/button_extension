using UnityEngine;

namespace FancyButtons
{
    [AddComponentMenu("Fancy Button/Selectable")]
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;

        private Button _selectedButton;
        
        private void Awake()
        {
            foreach (Button button in _buttons)
                button.StateChanged += OnStateChange;
        }

        private void OnStateChange(ButtonState state)
        {
            if ((state & ButtonState.Pressed) == 0) return;

            foreach (Button button in _buttons)
            {
                if (button.Pressed && _selectedButton != button)
                {
                    _selectedButton = button;
                    button.Selected = true;
                }
                else if (_selectedButton != button)
                {
                    button.Selected = false;
                }
            }
        }

        private void OnDestroy()
        {
            foreach (Button button in _buttons)
                button.StateChanged -= OnStateChange;
        }
    }
}
