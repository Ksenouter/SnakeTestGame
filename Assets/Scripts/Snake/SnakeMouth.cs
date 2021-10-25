using Game;
using Road.Objects;
using UnityEngine;

namespace Snake
{
    public class SnakeMouth : MonoBehaviour
    {

        [SerializeField] private SnakeElement headSnakeElement;

        #region MonoBehavior CallBacks

        private void OnTriggerEnter(Collider other)
        {
            var otherEdible = other.CompareTag("Edible");
            var otherObstacle = other.CompareTag("Obstacle");
            if (!otherEdible && !otherObstacle) return;

            var edibleComponent = other.GetComponent<IEdibleObject>();
            if (edibleComponent == null) return;
            
            if (GameManager.Instance.GodMode) edibleComponent.Eat();
            else if (otherObstacle) return;
            
            var eatColor = edibleComponent.GetEatColor();
            if (eatColor == null || eatColor == headSnakeElement.CurrentColor)
                edibleComponent.Eat();
            else
                GameManager.Instance.GameOver();
        }

        #endregion

    }
}
