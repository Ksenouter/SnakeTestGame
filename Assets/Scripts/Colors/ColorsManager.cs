using System.Linq;
using UnityEngine;

namespace Colors
{
    public class ColorsManager : MonoBehaviour
    {

        [SerializeField] private Color[] inspectorColors;

        public static ColorsManager Instance { get; private set; }
        public GameColor CurrentColor { get; private set; }
        public GameColor SecondaryColor { get; private set; }

        private GameColor[] _colors;

        #region MonoBehaviour CallBacks

        private void Awake()
        {
            Instance = this;
            
            InitializeColors();
            SetRandomColor();
        }
        
        #endregion
        
        #region Private Methods

        private void InitializeColors()
        {
            _colors = new GameColor[inspectorColors.Length];
            for (var i = 0; i < inspectorColors.Length; i++)
                _colors[i] = new GameColor(i, inspectorColors[i]);
        }
        
        #endregion
        
        #region Interface

        public void SetRandomColor()
        {
            SetColor(GetRandomColor());
        }
        
        public void SetColor(GameColor newColor)
        {
            CurrentColor = newColor;
            SecondaryColor = GetNewRandomColor(newColor);
            Events.ColorChanged.Publish(CurrentColor);
        }

        public void SetNewRandomColor()
        {
            SetColor(GetNewRandomColor(CurrentColor));
        }
        
        public GameColor GetNewRandomColor(GameColor oldColor)
        {
            byte randomColorNum;
            do
            {
                randomColorNum = (byte)Random.Range(0, _colors.Length);
            } while (randomColorNum == oldColor.ColorNum);
            
            return GetColorByNum(randomColorNum);
        }
        
        public GameColor GetRandomColor()
        {
            return _colors[Random.Range(0, _colors.Length)];
        }

        public GameColor GetColorByNum(byte colorNum)
        {
            return _colors.FirstOrDefault(gameColor => gameColor.ColorNum == colorNum);
        }
        
        #endregion

    }

    public class GameColor
    {
        
        public readonly int ColorNum;
        public readonly Color Color;

        public GameColor(int num, Color color)
        {
            ColorNum = num;
            Color = color;
        }
        
    }
}
