using Bnny.Scripts.Data;

namespace Bnny.Scripts.SaveSystem
{
    public interface IProgressWriter : IProgressUser
    {
        public void SaveProgress(ProgressData progressData);
    }
}
