using ButtonExtension.Data;
using TMPro;
using UnityEngine;

namespace ButtonExtension
{
    public class ButtonTextAnimator : ButtonAnimator<ButtonTextSettings>
    {
        [SerializeField] private TMP_Text _text;

        protected override void Reset()
        {
            _text = GetComponent<TMP_Text>();
            base.Reset();
        }

        protected override void OnStateChanged(ButtonState buttonState)
        {
            if (Settings.IsAvailableState(buttonState))
                SwapMaterial(Settings.GetScaleSettings<TextSettings>(buttonState));
        }

        private void SwapMaterial(TextSettings settings)
        {
            _text.fontMaterial = settings.Material;
            _text.alpha = settings.Alpha;
        }
    }
}
