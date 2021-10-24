using Models.Events;

namespace Colors
{
    public static class Events
    {
        
        public static readonly ParamEvent<GameColor> ColorChanged = new ParamEvent<GameColor>();
        
    }
}
