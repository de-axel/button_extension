using System;
using System.Linq;
using FancyButton.Core;
using UnityEngine;

namespace FancyButton.Data
{
    [Serializable]
    public class ScaleSettings : IButtonSettings
    {
        public ButtonState State;
        public AnimationCurve Curve;
        public float Value;
        public float Duration;
    }
    
    [CreateAssetMenu(fileName = "Button scale settings", menuName = "Button settings/Scale settings")]
    public class ButtonScaleSettings : ButtonSettings
    {
        [SerializeField] private ScaleSettings[] _settings;

        public override bool IsAvailableState(ButtonState state)
        {
            return _settings.Any(setting => setting.State == state);
        }

        public override T GetScaleSettings<T>(ButtonState state)
        {
            foreach (ScaleSettings setting in _settings)
            {
                if (setting.State == state)
                    return (T)Convert.ChangeType(setting, typeof(T));
            }
            
            return default;
        }
    }
}
