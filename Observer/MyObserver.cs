namespace Observer
{
    namespace MyObserver
    {
        public interface ISubject
        {
            public void RegisterObserver(IObserver observer);
            public void RemoveObserver(IObserver observer);
            public void NotifyObserver();
        }

        public interface IObserver
        {
            public void Update();
        }

        public interface IDisplayElement
        {
            public void Display();
        }
    }
}