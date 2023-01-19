using System;
using System.Linq;
using UnityEngine;

namespace ButtonExtension.Data
{
    [Serializable]
    public class ColorSettings : IButtonSettings
    {
        public ButtonState State;
        public AnimationCurve Curve;
        public Color Color;
        public float Duration;
    }

    [CreateAssetMenu(fileName = "Button color settings", menuName = "Button settings/Color settings")]
    public class ButtonColorSettings : ButtonSettings
    {
        [SerializeField] private ColorSettings[] _settings;
        
        public override bool IsAvailableState(ButtonState state)
        {
            return _settings.Any(setting => setting.State == state);
        }

        public override T GetScaleSettings<T>(ButtonState state)
        {
            foreach (ColorSettings setting in _settings)
            {
                if (setting.State == state)
                    return (T)Convert.ChangeType(setting, typeof(T));
            }

            return default;
        }
    }
}
