using FancyButtons;
using UnityEngine;

namespace Data
{
    public abstract class ButtonSettings : ScriptableObject
    {
        public abstract bool IsAvailableState(ButtonState state);

        public abstract T GetScaleSettings<T>(ButtonState state) where T : IButtonSettings;
    }
}