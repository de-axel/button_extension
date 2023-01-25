using System;
using System.Linq;
using FancyButtons;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ShineSettings : IButtonSettings
    {
        public ButtonState State;
        public AnimationCurve MoveShineCurve;
        public AnimationCurve ScaleButtonCurve;
        public float ScaleValue;
        public float Duration;
        public float Delay;
    }
    
    [CreateAssetMenu(fileName = "Button shine settings", menuName = "Button settings/Shine settings")]
    public class ButtonShineSettings : ButtonSettings
    {
        [SerializeField] private ShineSettings[] _settings;

        public override bool IsAvailableState(ButtonState state)
        {
            return _settings.Any(setting => setting.State == state);
        }

        public override T GetScaleSettings<T>(ButtonState state)
        {
            foreach (ShineSettings setting in _settings)
            {
                if (setting.State == state)
                    return (T)Convert.ChangeType(setting, typeof(T));
            }
            
            return default;
        }
    }
}
