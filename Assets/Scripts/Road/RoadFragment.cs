using UnityEngine;

namespace Road
{
    public class RoadFragment : MonoBehaviour
    {

        [SerializeField] private float gameSpeedMultiplier = 1;
        [SerializeField] private float destroyPositionZ = -10;
        
        private float _currentSpeed;
        
        #region MonoBehavior CallBacks
        
        private void FixedUpdate()
        {
            transform.Translate(Vector3.back * _currentSpeed * Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            if (transform.position.z > destroyPositionZ) return;
            
            RoadManager.Instance.CreateNewRoadFragment();
            RoadManager.Instance.DestroyRoadFragment(this);
        }

        #endregion
        
        #region Interface

        public void SetPosition(float positionZ)
        {
            transform.position = new Vector3(0, 0, positionZ);
        }

        public void SetSpeed(float newSpeed)
        {
            _currentSpeed = newSpeed * gameSpeedMultiplier;
        }

        public float GetPositionZ()
        {
            return transform.position.z;
        }
        
        #endregion
        
    }
}
