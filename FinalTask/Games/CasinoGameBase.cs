
namespace FinalTask.Games
{
    public abstract class CasinoGameBase
    {
        public abstract void PlayGame();
        public event Action? OnWin;
        public event Action? OnLoose;
        public event Action? OnDraw;
        protected void OnWinInvoke()
        {
            OnWin?.Invoke();
        }
        protected void OnLooseInvoke()
        {
            OnLoose?.Invoke();
        }
        protected void OnDrawInvoke()
        {
            OnDraw?.Invoke();
        }
        public CasinoGameBase()
        {
            FactoryMethod();
        }
        protected abstract void FactoryMethod();
        public void Result(string result)
        {
            Console.WriteLine(result);
        }



    }
}
