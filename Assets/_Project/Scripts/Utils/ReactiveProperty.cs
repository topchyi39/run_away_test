using System;

namespace Scripts.Utils
{
    public interface IReadProperty<T>
    {
        T Value { get; }
        void Subscribe(Action<T> action);
    }
    
    public class ReactiveProperty<T> : IReadProperty<T>
    {
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _valueChanged?.Invoke(_value);
            }
            
        }
        
        private T _value;
        private Action<T> _valueChanged;

        public void Subscribe(Action<T> action)
        {
            _valueChanged += action;
        }
    }
}