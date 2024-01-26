using Scripts.UI;
using Scripts.UI.Gameplay;
using Scripts.UI.Input;
using UnityEngine;

namespace Scripts.Input
{
    public class TouchScreenInput
    {
        public Vector2 MoveAxis => _inputActions.Gameplay.MoveAxis.ReadValue<Vector2>();

        private readonly InputPresenter _presenter;
        private readonly PlayerInputActions _inputActions;
        
        public TouchScreenInput()
        {
            _presenter = new InputPresenter(this);
            UIManager.Instance.Bind<InputScreen>(_presenter);
            
            _inputActions = new PlayerInputActions();
        }

        public void Enable()
        {
            _inputActions?.Enable();
            _presenter.EnableScreen();
        }

        public void Disable()
        {
            _inputActions?.Disable();
            _presenter.DisableScreen();
        }
    }
}