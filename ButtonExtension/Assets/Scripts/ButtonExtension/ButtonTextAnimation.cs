using ButtonExtension.Data;
using TMPro;
using UnityEngine;

namespace ButtonExtension
{
    public class ButtonTextAnimation : ButtonAnimation<ButtonTextSettings>
    {
        [SerializeField] private TMP_Text _text;

        protected override void Reset()
        {
            base.Reset();
            _text = GetComponent<TMP_Text>();
        }

        protected override void OnStateChanged(ButtonState buttonState)
        {
            if (Settings.IsAvailableState(buttonState))
                SwapMaterial(Settings.GetScaleSettings<TextSettings>(buttonState));
        }

        private void SwapMaterial(TextSettings settings)
        {
            _text.material = settings.Material;
        }
    }
}
