using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for the observable models.
/// </summary>
public static class ObservableExtensions
{
    extension<T>(ObservableCollection<T> source)
    {
        public void RemoveRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                source.Remove(item);
            }
        }
    }
}
