using QFramework;

//��ܽṹ
//ģ��ע��
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
