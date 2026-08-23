namespace Code.Services.Progress
{
    public interface ISaveLoadService
    {
        ProgressData ProgressData { get; }
        
        void AddProgressWatcher(IProgressWatcher progressWatcher);
        void RemoveProgressWatcher(IProgressWatcher progressWatcher);
    }
}
