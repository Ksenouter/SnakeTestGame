using UnityEngine;

namespace Road
{
    public class RoadFragment : MonoBehaviour
    {

        [SerializeField] private float destroyPositionZ = -10;
        
        private float _currentSpeed;
        
        #region MonoBehavior CallBacks

        private void Update()
        {
            transform.Translate(Vector3.back * (Time.deltaTime * _currentSpeed));
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
            _currentSpeed = newSpeed;
        }

        public float GetPositionZ()
        {
            return transform.position.z;
        }
        
        #endregion
        
    }
}
