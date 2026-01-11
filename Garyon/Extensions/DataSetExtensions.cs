using System.Data;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="DataSet"/> and relevant types.
/// </summary>
public static class DataSetExtensions
{
#if HAS_DATA_ROW_FIELD_METHOD
    /// <summary>
    /// Gets a value from a data row on the given table,
    /// by the column name, and casts it onto the specified type.
    /// </summary>
    /// <typeparam name="T">The type to cast the value onto.</typeparam>
    /// <param name="table">The table which contains the row.</param>
    /// <param name="row">The row whose column value to get.</param>
    /// <param name="name">The name of the column to get.</param>
    /// <param name="field">
    /// The field to store the value into if the column name exists,
    /// otherwise the parameter location remains unchanged. For the
    /// purposes of not setting the value if not found, it's required
    /// to be passed by <see langword="ref"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the column name was found in the
    /// table, otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// It is not validated whether the table contains
    /// the provided row or not, so long as both the
    /// table and the row contain the requested column name,
    /// the value is successfully retrieved.
    /// </remarks>
    public static bool ReadField<T>(this DataTable table, DataRow row, string name, ref T field)
    {
        var contained = table.Columns.Contains(name);
        if (contained)
        {
            field = row.Field<T>(name) ?? field;
        }
        return contained;
    }
#endif
}
