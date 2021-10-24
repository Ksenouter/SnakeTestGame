using Game;
using UnityEngine;

namespace Road.Objects
{
    public class DeadlyObject : MonoBehaviour
    {

        [SerializeField] private string triggerElementName = "Snake Head";
        
        #region MonoBehaviour CallBacks

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (other.gameObject.name != triggerElementName) return;
            
            GameManager.Instance.GameOver();
        }

        #endregion
        
    }
}
