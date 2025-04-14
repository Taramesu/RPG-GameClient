using QFramework;

//¿ò¼Ü½á¹¹
//Ä£¿é×¢²á
namespace RpgGame
{
    public class RpgGame : Architecture<RpgGame>
    {
        protected override void Init()
        {
            RegisterModel(new EntityModel());

            RegisterSystem(new EntitySystem());
            RegisterSystem(new MoveSystem());
            RegisterSystem(new InputSystem());
            RegisterSystem(new CameraSystem());
        }
    }
}
