using System;

namespace Scripts.UI.MVP
{
    public interface IModel
    {
        
    }

    public interface IPresenter
    {
        Type ViewType { get; }
        void Bind(IView view);
    }

    public interface IView
    {
        void Bind(IPresenter presenter);
    }
}