using Bnny.Scripts.Data;

namespace Bnny.Scripts.SaveSystem
{
    public interface IProgressReader : IProgressUser
    {
        public void LoadProgress(ProgressData progressData);
    }
}
