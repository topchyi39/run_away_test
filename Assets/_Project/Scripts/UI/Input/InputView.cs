using Scripts.UI.Gameplay;
using Scripts.UI.MVP;
using UnityEngine;

namespace Scripts.UI.Input
{
    public class InputView : View<InputPresenter>
    {
        [SerializeField] private InputScreen screen;
        
        public void Show()
        {
            screen.Show();
        }
        
        public void Hide()
        {
            screen.Hide();
        }
    }
}