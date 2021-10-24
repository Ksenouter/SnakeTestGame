using UnityEngine;

namespace Animations
{
    public class RotateAnimation : MonoBehaviour
    {

        [SerializeField] private Vector3 rotationVector;
        [SerializeField] private float rotationSpeed;
        
        #region MonoBehaviour CallBacks

        private void Update()
        {
            transform.Rotate(rotationVector * (rotationSpeed * Time.deltaTime));
        }

        #endregion
        
    }
}
