using UnityEngine;
using DG.Tweening;

namespace Person
{
    public class PersonAnimation : MonoBehaviour
    {

        [SerializeField] private float rotationStrength, scaleStrength;
        [SerializeField] private int rotationVibrato, scaleVibrato;
        
        #region MonoBehaviour CallBacks
        
        private void Start()
        {
            transform.DOShakeRotation(ShakesDuration, rotationStrength, rotationVibrato, 90, false)
                .SetLoops(-1);
            transform.DOShakeScale(ShakesDuration, scaleStrength, scaleVibrato, 90, false)
                .SetLoops(-1);
        }
        
        #endregion

        private const float ShakesDuration = 10f;

    }
}
