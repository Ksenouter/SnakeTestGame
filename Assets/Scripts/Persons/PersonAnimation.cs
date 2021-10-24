using DG.Tweening;
using UnityEngine;

namespace Persons
{
    public class PersonAnimation : MonoBehaviour
    {

        [SerializeField] private float rotationStrength, scaleStrength;
        [SerializeField] private int rotationVibrato, scaleVibrato;

        private Tween _rotationTween, _scaleTween;
        
        #region MonoBehaviour CallBacks
        
        private void Start()
        {
            _rotationTween = transform.DOShakeRotation(ShakesDuration, rotationStrength, rotationVibrato, 90,
                    false).SetLoops(-1);
            _scaleTween = transform.DOShakeScale(ShakesDuration, scaleStrength, scaleVibrato, 90,
                    false).SetLoops(-1);
        }

        private void OnDestroy()
        {
            _rotationTween.Kill();
            _scaleTween.Kill();
        }

        #endregion

        private const float ShakesDuration = 10f;

    }
}
