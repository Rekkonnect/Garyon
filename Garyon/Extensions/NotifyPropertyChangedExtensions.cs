using Garyon.Reflection;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="INotifyPropertyChanged"/>,
/// <see cref="INotifyPropertyChanged"/> and their related types.
/// </summary>
public static class NotifyPropertyChangedExtensions
{
    extension(NotifyCollectionChangedEventArgs e)
    {
        /// <summary>
        /// Gets the <see cref="NotifyCollectionChangedEventArgs.NewItems"/>
        /// cast to the specified type.
        /// </summary>
        public IEnumerable<T> NewItems<T>()
        {
            return e.NewItems?.Cast<T>() ?? [];
        }
    }
}
