using UnityEngine;

namespace Scripts.UI.MVP
{
    public abstract class View<TP> : MonoBehaviour, IView where TP : IPresenter
    {
        protected TP Presenter { get; private set; }
        
        public void Bind(IPresenter presenter)
        {
            Presenter = (TP)presenter;
        }
    }
}