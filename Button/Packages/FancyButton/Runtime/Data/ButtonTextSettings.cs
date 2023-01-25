using System;
using System.Linq;
using FancyButton.Core;
using UnityEngine;

namespace FancyButton.Data
{
    [Serializable]
    public class TextSettings : IButtonSettings
    {
        public ButtonState State;
        public Material Material;
        public float Alpha;
    }
    
    [CreateAssetMenu(fileName = "Button text settings", menuName = "Button settings/Text settings")]
    public class ButtonTextSettings : ButtonSettings
    {
        [SerializeField] private TextSettings[] _settings;
        
        public override bool IsAvailableState(ButtonState state)
        {
            return _settings.Any(setting => setting.State == state);
        }

        public override T GetScaleSettings<T>(ButtonState state)
        {
            foreach (TextSettings setting in _settings)
            {
                if (setting.State == state)
                    return (T)Convert.ChangeType(setting, typeof(T));
            }
            
            return default;
        }
    }
}
