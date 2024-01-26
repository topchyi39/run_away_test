namespace Scripts.GameSystem
{
    public class Timer
    {
        public float EvaluateTime => _time;

        private float _time;
        private bool _canTick;
        
        public void Start()
        {
            _canTick = true;
            _time = 0;
        }

        public void Tick(float time)
        {
            if (!_canTick) return;
            
            _time += time;
        }

        public void Stop()
        {
            _canTick = false;
        }
        
    }
}