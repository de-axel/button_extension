using System.Collections;
using ButtonExtension.Data;
using UnityEngine;

namespace ButtonExtension
{
    [RequireComponent(typeof(RectTransform))]
    public class ButtonShineAnimation : ButtonAnimation<ButtonShineSettings>
    {
        [SerializeField] private RectTransform _shinePrefab;

        private RectTransform _shine;
        private Coroutine _animationRoutine;
        private Coroutine _delayRoutine;
        private Vector3 _defaultScale;
        private float _buttonWidth;

        protected override void Awake()
        {
            base.Awake();
        
            _defaultScale = transform.localScale;
        
            GetButtonWidth();
            TryCreateShine();
        }

        protected override void OnStateChanged(ButtonState buttonState)
        {
            if (Settings.IsAvailableState(buttonState))
                PlayAnimation(Settings.GetScaleSettings<ShineSettings>(buttonState));
            else
                StopAnimation();
        }

        private void PlayAnimation(ShineSettings settings)
        {
            if (_delayRoutine != null)
                StopCoroutine(_delayRoutine);
            
            _delayRoutine = StartCoroutine(DelayBeforeAnimation());
            
            IEnumerator DelayBeforeAnimation()
            {
                yield return new WaitForSeconds(settings.Delay);
                _animationRoutine = StartCoroutine(Animation());
            }
        
            IEnumerator Animation()
            {
                float currentDuration = 0;
                Vector2 currentAnchorPosition = new(-_buttonWidth, 0);
                Vector2 targetAnchorPosition = new(_buttonWidth, 0);
            
                while (currentDuration < settings.Duration)
                {
                    if (_shine)
                        _shine.anchoredPosition = Vector2.Lerp(currentAnchorPosition, targetAnchorPosition, settings.MoveShineCurve.Evaluate(currentDuration / settings.Duration));

                    Vector3 targetScale = _defaultScale + Vector3.one * settings.ScaleButtonCurve.Evaluate(currentDuration / settings.Duration);
                    transform.localScale = Vector3.Lerp(_defaultScale, targetScale, settings.ScaleButtonCurve.Evaluate(currentDuration / settings.Duration));
                
                    currentDuration += Time.unscaledDeltaTime;
                    yield return null;
                }

                ResetShinePosition();
                PlayAnimation(settings);
            }
        }

        private void StopAnimation()
        {
            if (_animationRoutine != null)
                StopCoroutine(_animationRoutine);
        
            if (_delayRoutine != null)
                StopCoroutine(_delayRoutine);
        
            ResetShinePosition();
        }

        private void ResetShinePosition()
        {
            if (_shine)
                _shine.anchoredPosition = new Vector2(-_buttonWidth, 0);
        }

        private void GetButtonWidth()
        {
            _buttonWidth = GetComponent<RectTransform>().rect.width;
        }
    
        private void TryCreateShine()
        {
            if (_shinePrefab == null) return;
        
            _shine = Instantiate(_shinePrefab, transform);
            _shine.SetSiblingIndex(0);
            ResetShinePosition();
        }
    }
}
