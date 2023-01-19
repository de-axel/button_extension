using ButtonExtension.Data;
using UnityEngine;
using UnityEngine.UI;

namespace ButtonExtension
{
    [RequireComponent(typeof(Image))]
    public class ButtonSpriteAnimation : ButtonAnimation<ButtonSpriteSettings>
    {
        [SerializeField] private Image _image;

        protected override void Reset()
        {
            base.Reset();
            _image = GetComponent<Image>();
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