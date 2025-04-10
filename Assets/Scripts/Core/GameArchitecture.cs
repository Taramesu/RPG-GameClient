using QFramework;

//¿ò¼Ü½á¹¹
//Ä£¿é×¢²á
namespace RpgGame
{
    public class RpgGame : Architecture<RpgGame>
    {
        protected override void Init()
        {
            RegisterSystem<IInputSystem>(new InputSystem());
        }
    }
}
