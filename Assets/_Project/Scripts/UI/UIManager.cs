using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.UI.MVP;
using Scripts.UI.Screens;

namespace Scripts.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private Dictionary<Type, Screen> _screens;
        
        protected override void Init()
        {
            _screens = GetComponentsInChildren<Screen>().ToDictionary(item => item.GetType(), item => item);
        }

        public void Bind<T>(IPresenter presenter) where T : Screen
        {
            var type = typeof(T);
            
            if (_screens.TryGetValue(type, out var screen))
            {
                screen.Bind(presenter);
            }
        }

        public bool TryGetScreen<T>(out T outScreen) where T: Screen
        {
            var type = typeof(T);
            if (_screens.TryGetValue(type, out var screen))
            {
                outScreen = (T)screen;
                return true;
            }

            outScreen = null;
            return false;
        }
    }
}