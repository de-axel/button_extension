using Data;
using UnityEngine;
using UnityEngine.UI;

namespace FancyButtons
{
    [AddComponentMenu("Fancy Button/Button Sprite Animator")]
    public class ButtonSpriteAnimator : ButtonAnimator<ButtonSpriteSettings>
    {
        [SerializeField] private Image _image;

        protected override void Reset()
        {
            _image = GetComponent<Image>();
            base.Reset();
        }

        protected override void OnStateChanged(ButtonState buttonState)
        {
            if (Settings.IsAvailableState(buttonState))
                SwapSprite(Settings.GetScaleSettings<SpriteSettings>(buttonState));
        }

        private void SwapSprite(SpriteSettings settings)
        {
            _image.sprite = settings.Sprite;
        }
    }
}