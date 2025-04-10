namespace RpgGame
{
    public enum ControlEnum
    {
        forward, backward, left, right
    }

    public class ControlEvent
    {
        public int id;
        public ControlEnum Control;
    }
}
