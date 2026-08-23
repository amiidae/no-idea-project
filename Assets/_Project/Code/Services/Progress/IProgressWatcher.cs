public interface IProgressWriter : IProgressWatcher
{
    void WriteProgress(ProgressData progressData);
}

public interface IProgressReader : IProgressWatcher
{
    void ReadProgress(ProgressData progressData);
}


public interface IProgressWatcher { }

