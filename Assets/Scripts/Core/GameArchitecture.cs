using QFramework;

//��ܽṹ
//ģ��ע��
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
