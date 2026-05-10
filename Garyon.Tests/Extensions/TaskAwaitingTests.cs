using Garyon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class TaskAwaitingTests
{
    [Test]
    public async Task WaitAllAwaitsEnumerableTasksTest()
    {
        var completed = new bool[3];
        IEnumerable<Task> tasks =
        [
            CompleteAsync(0),
            CompleteAsync(1),
            CompleteAsync(2),
        ];

        await tasks.WaitAll();

        await Assert.That(completed.All(static value => value)).IsTrue();

        async Task CompleteAsync(int index)
        {
            await Task.Yield();
            completed[index] = true;
        }
    }

    [Test]
    public async Task WaitAllEnumeratesEntireAsyncSequenceBeforeAwaitingTasksTest()
    {
        var createdCount = 0;
        var enumerationCompleted = CreateTaskSource();
        var taskSources = new List<TaskCompletionSource<bool>>
        {
            CreateTaskSource(),
            CreateTaskSource(),
            CreateTaskSource(),
        };

        var waitingTask = GetTasks().WaitAll();
        await enumerationCompleted.Task;

        await Assert.That(createdCount).IsEqualTo(3);
        await Assert.That(waitingTask.IsCompleted).IsFalse();

        foreach (var taskSource in taskSources)
            taskSource.SetResult(true);

        await waitingTask;

        async IAsyncEnumerable<Task> GetTasks()
        {
            foreach (var taskSource in taskSources)
            {
                createdCount++;
                await Task.Yield();
                yield return taskSource.Task;
            }

            enumerationCompleted.SetResult(true);
        }
    }

    [Test]
    public async Task WaitAllIterativelyAwaitsEachTaskBeforeEnumeratingNextTaskTest()
    {
        var createdCount = 0;
        var firstCreated = CreateTaskSource();
        var secondCreated = CreateTaskSource();
        var thirdCreated = CreateTaskSource();
        var firstTask = CreateTaskSource();
        var secondTask = CreateTaskSource();
        var thirdTask = CreateTaskSource();

        var waitingTask = GetTasks().WaitAllIteratively();

        await firstCreated.Task;
        await Assert.That(createdCount).IsEqualTo(1);
        await Assert.That(secondCreated.Task.IsCompleted).IsFalse();

        firstTask.SetResult(true);
        await secondCreated.Task;
        await Assert.That(createdCount).IsEqualTo(2);
        await Assert.That(thirdCreated.Task.IsCompleted).IsFalse();

        secondTask.SetResult(true);
        await thirdCreated.Task;
        await Assert.That(createdCount).IsEqualTo(3);

        thirdTask.SetResult(true);
        await waitingTask;

        async IAsyncEnumerable<Task> GetTasks()
        {
            createdCount++;
            firstCreated.SetResult(true);
            yield return firstTask.Task;

            createdCount++;
            secondCreated.SetResult(true);
            yield return secondTask.Task;

            createdCount++;
            thirdCreated.SetResult(true);
            yield return thirdTask.Task;

            await Task.CompletedTask;
        }
    }

    private static TaskCompletionSource<bool> CreateTaskSource()
    {
        return new(TaskCreationOptions.RunContinuationsAsynchronously);
    }
}
