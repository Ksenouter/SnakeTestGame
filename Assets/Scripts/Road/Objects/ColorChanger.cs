using Colors;
using UnityEngine;

namespace Road.Objects
{
    public class ColorChanger : MonoBehaviour
    {

        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private ParticleSystem[] particles;
        
        public GameColor CurrentColor;
        
        #region MonoBehaviour CallBacks
        
        private void Start()
        {
            ColorsManager.Instance.SetNewRandomColor();
            CurrentColor = ColorsManager.Instance.CurrentColor;
            meshRenderer.material.color = CurrentColor.Color;
            
            var particlesColor = CurrentColor.Color;
            particlesColor.a = particles[0].main.startColor.color.a;
            foreach (var smoke in particles)
            {
                var smokeMain = smoke.main;
                smokeMain.startColor = particlesColor;
            }
        }
        
        #endregion
        
    }
}
