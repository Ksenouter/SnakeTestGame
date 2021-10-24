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
            if (!other.CompareTag("Edible")) return;

            var edibleComponent = other.GetComponent<IEdibleObject>();
            if (edibleComponent == null) return;

            var eatColor = edibleComponent.GetEatColor();
            if (eatColor == null || eatColor == headSnakeElement.CurrentColor)
                edibleComponent.Eat();
            else
                GameManager.Instance.GameOver();
        }

        #endregion

    }
}
