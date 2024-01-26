using Scripts.Input;
using Scripts.UI.MVP;

namespace Scripts.UI.Input
{
    public class InputPresenter : Presenter<TouchScreenInput, InputView>
    {
        public InputPresenter(TouchScreenInput model) : base(model)
        {
        }

        public void EnableScreen()
        {
            View?.Show();
        }

        public void DisableScreen()
        {
            View?.Hide();
        }
    }
}