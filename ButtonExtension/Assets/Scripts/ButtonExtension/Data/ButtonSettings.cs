using UnityEngine;

namespace ButtonExtension.Data
{
    public abstract class ButtonSettings : ScriptableObject
    {
        public abstract bool IsAvailableState(ButtonState state);

        public abstract T GetScaleSettings<T>(ButtonState state) where T : IButtonSettings;
    }
}