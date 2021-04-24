namespace ch.swisstxt.mh3.externaltask.extension
{
    public interface IExternalTaskHandler<TJob>
    {
        void handleError(ExternalTask<TJob> task, string errorMessage);
        void handleStart(ExternalTask<TJob> task);
        void handleSuccess(ExternalTask<TJob> task);
        void handleTask(ExternalTask<TJob> task);
    }
}


