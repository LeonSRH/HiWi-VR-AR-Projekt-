using System.Threading.Tasks;

namespace Common.Extensions {
    public static class TaskExtension {
        public static bool IsRunning(this Task task) {
            return !(task.IsCanceled || task.IsCompleted || task.IsFaulted);
        }
    }
}