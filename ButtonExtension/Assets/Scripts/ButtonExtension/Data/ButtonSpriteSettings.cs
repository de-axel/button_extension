using System;
using System.Linq;
using UnityEngine;

namespace ButtonExtension.Data
{
    [Serializable]
    public class SpriteSettings : IButtonSettings
    {
        public ButtonState State;
        public Sprite Sprite;
    }
    
    [CreateAssetMenu(fileName = "Button sprite settings", menuName = "Button settings/Sprite settings")]
    public class ButtonSpriteSettings : ButtonSettings
    {
        [SerializeField] private SpriteSettings[] _settings;
        
        public override bool IsAvailableState(ButtonState state)
        {
            return _settings.Any(setting => setting.State == state);
        }

        public override T GetScaleSettings<T>(ButtonState state)
        {
            foreach (SpriteSettings setting in _settings)
            {
                if (setting.State == state)
                    return (T)Convert.ChangeType(setting, typeof(T));
            }
            
            return default;
        }
    }
}
