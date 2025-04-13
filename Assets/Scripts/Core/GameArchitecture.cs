using QFramework;

//¿ò¼Ü½á¹¹
//Ä£¿é×¢²á
namespace RpgGame
{
    public class RpgGame : Architecture<RpgGame>
    {
        protected override void Init()
        {
            RegisterSystem(new InputSystem());
            RegisterSystem(new MoveSystem());
            RegisterSystem(new CameraSystem());

            RegisterModel(new TransFormModel());
            RegisterModel(new EntityModel());
        }
    }
}
