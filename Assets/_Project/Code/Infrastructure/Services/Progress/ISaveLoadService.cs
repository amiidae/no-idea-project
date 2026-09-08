using System.Threading.Tasks;

namespace Code.Services.Progress
{
    public interface ISaveLoadService
    {
        void AddProgressWatcher(IProgressWatcher progressWatcher);
        void RemoveProgressWatcher(IProgressWatcher progressWatcher);
        
        bool TryGetLoadedProgressData(out ProgressData progressData);
        
        Task SaveProgress();
        
        Task LoadProgress();
    }
}
