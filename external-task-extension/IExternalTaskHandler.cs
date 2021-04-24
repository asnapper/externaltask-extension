namespace ch.swisstxt.mh3.externaltask.extension
{
    public interface IExternalTaskHandler<TJob>
    {
        void HandleError(ExternalTask<TJob> task, string errorMessage);
        void HandleStart(ExternalTask<TJob> task);
        void HandleSuccess(ExternalTask<TJob> task);
        void HandleTask(ExternalTask<TJob> task);
    }
}


