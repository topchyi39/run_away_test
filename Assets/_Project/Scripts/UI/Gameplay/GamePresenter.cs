using Scripts.GameSystem;
using Scripts.UI.MVP;

namespace Scripts.UI.Gameplay
{
    public class GamePresenter : Presenter<Game, GameView>
    {
        public GamePresenter(Game model) : base(model)
        {
        }

        public void UpdateTimer(float time)
        {
            View?.UpdateTimer(time.ToString("F1"));
        }

        public void ShowGameOver(float time, string killReason)
        {
            View?.ShowGameOver(time.ToString("F1"), killReason.ToLower());
        }

        public void HideGameOver()
        {
            View?.HideGameOver();
        }

        public void ShowGameplay()
        {
            View?.ShowGameplay();
        }

        public void Restart()
        {
            Model.RestartGame();
        }
    }
}