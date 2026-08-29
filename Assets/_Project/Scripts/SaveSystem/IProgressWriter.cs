internal interface IProgressWriter : IProgressUser
{
    public void SaveProgress(ProgressData progressData);
}
