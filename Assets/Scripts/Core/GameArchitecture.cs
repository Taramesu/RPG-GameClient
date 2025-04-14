using QFramework;

//��ܽṹ
//ģ��ע��
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
