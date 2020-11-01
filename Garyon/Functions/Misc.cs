namespace Garyon.Functions
{
    /// <summary>Contains functions that cannot be categorized otherwise.</summary>
    public static class Misc
    {
        /// <summary>Swaps the values of two variables.</summary>
        /// <typeparam name="T">The type of the variables.</typeparam>
        /// <param name="a">The first variable.</param>
        /// <param name="b">The second variable.</param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
    }
}
