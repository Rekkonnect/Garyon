using System;
using System.Linq;
using System.Reflection;

namespace Garyon.QualityControl.Types
{
    /// <summary>Provides definitions for example types.</summary>
    public class ExampleTypes
    {
        /// <summary>This type does not inherit any other type.</summary>
        protected interface IA { }
        /// <summary>This type does not inherit any other type.</summary>
        protected interface IB { }
        /// <summary>This type does not inherit any other type.</summary>
        protected interface IC { }
        /// <summary>This type inherits <seealso cref="IA"/>, <seealso cref="IB"/>.</summary>
        protected interface ID : IA, IB { } // IA, IB
        /// <summary>This type inherits <seealso cref="IA"/>, <seealso cref="IB"/>, <seealso cref="IC"/>.</summary>
        protected interface IE : IA, IB, IC { } // IA, IB, IC
        /// <summary>This type inherits <seealso cref="ID"/>, <seealso cref="IA"/>, <seealso cref="IB"/>, <seealso cref="IE"/>, <seealso cref="IC"/>.</summary>
        protected interface IF : ID, IE { } // ID, IA, IB, IE, IC
        /// <summary>This type inherits <seealso cref="IF"/>, <seealso cref="ID"/>, <seealso cref="IA"/>, <seealso cref="IB"/>, <seealso cref="IE"/>, <seealso cref="IC"/>.</summary>
        protected interface IG : IF { } // IF, ID, IA, IB, IE, IC
        /// <summary>This type does not inherit any other type.</summary>
        protected interface IH { }
        /// <summary>This type inherits <seealso cref="IH"/>.</summary>
        protected interface II : IH { } // IH
        /// <summary>This type does not inherit any other type.</summary>
        protected interface IJ { }
        /// <summary>This type inherits <seealso cref="ID"/>, <seealso cref="IA"/>, <seealso cref="IB"/>, <seealso cref="IJ"/>, <seealso cref="II"/>, <seealso cref="IH"/>.</summary>
        protected interface IK : ID, IJ, II { } // ID, IA, IB, IJ, II, IH

        /// <summary>This type inherits <seealso cref="ID"/>, <seealso cref="IA"/>, <seealso cref="IB"/>, <seealso cref="IJ"/>.</summary>
        protected class CA : ID, IJ { } // ID, IA, IB, IJ
        /// <summary>This type inherits <seealso cref="CA"/>, <seealso cref="IK"/>, <seealso cref="ID"/>, <seealso cref="IA"/>, <seealso cref="IB"/>, <seealso cref="IJ"/>, <seealso cref="II"/>, <seealso cref="IH"/>.</summary>
        protected class CB : CA, IK { } // CA, IK, ID, IA, IB, IJ, II, IH
        /// <summary>This type inherits <seealso cref="CB"/>, <seealso cref="CA"/>, <seealso cref="IC"/>, <seealso cref="IK"/>, <seealso cref="ID"/>, <seealso cref="IA"/>, <seealso cref="IB"/>, <seealso cref="IJ"/>, <seealso cref="II"/>, <seealso cref="IH"/>.</summary>
        protected class CC : CB, IC { } // CB, CA, IC, IK, ID, IA, IB, IJ, II, IH
        /// <summary>This type inherits <seealso cref="CC"/>, <seealso cref="CB"/>, <seealso cref="CA"/>, <seealso cref="IE"/>, <seealso cref="IC"/>, <seealso cref="IK"/>, <seealso cref="ID"/>, <seealso cref="IA"/>, <seealso cref="IB"/>, <seealso cref="IJ"/>, <seealso cref="II"/>, <seealso cref="IH"/>.</summary>
        protected class CD : CC, IE { } // CC, CB, CA, IE, IC, IK, ID, IA, IB, IJ, II, IH

        protected struct SA : ID { } // ID, IA, IB
        protected struct SB : IH { } // IH
        protected struct SC : IK { } // IK, ID, IA, IB, IJ, II, IH

        protected enum EA { }

        protected delegate void DA();

        protected class ExceptionA : Exception { }
        protected class ExceptionA<T> : ExceptionA { }

        protected class AttributeA : Attribute { }

        protected static class StaticClass { }

        protected class GenericClass<T> { }
        protected class GenericClass<T1, T2> { }
        protected class GenericClass<T1, T2, T3> { }

        protected static class GenericStaticClass<T> { }
        protected static class GenericStaticClass<T1, T2> { }
        protected static class GenericStaticClass<T1, T2, T3> { }

        protected static class GenericWithNestedGeneric<T1, T2>
        {
            public static readonly Type Type = typeof(GenericWithNestedGeneric<,>);

            public static class Nested<T3, T4, T5>
            {
                public static readonly Type Type = typeof(GenericWithNestedGeneric<,>.Nested<,,>);
                
                public static class Nested2<T6, T7, T8>
                {
                    public static readonly Type Type = typeof(GenericWithNestedGeneric<,>.Nested<,,>.Nested2<,,>);
                    public static readonly MethodInfo ContainedMethod;

                    static Nested2()
                    {
                        ContainedMethod = Type.GetMember(nameof(Method)).First() as MethodInfo;
                    }

                    public static void Method<T9, T10>() { }
                }
            }
        }
    }
}
