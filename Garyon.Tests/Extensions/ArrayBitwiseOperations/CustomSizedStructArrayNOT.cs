using Garyon.Functions.UnmanagedHelpers;
using Garyon.QualityControl.SizedStructs;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations
{
    public class CustomSizedStructArrayNOT : ArrayBitwiseOperationsHelpersVector128
    {
        protected unsafe void NOTCustomStructArray<TStruct>()
            where TStruct : unmanaged, ISizedStruct
        {
            PerformManipulationCustomStructArray<TStruct>(NOTArrayVector128CustomType);
        }
        protected unsafe void PerformManipulationCustomStructArray<TStruct>(ArrayManipulationOperation<TStruct, TStruct> operation)
            where TStruct : unmanaged, ISizedStruct
        {
            PerformManipulation(new TStruct[ArrayLength], new TStruct[ArrayLength], operation);
        }
        protected unsafe void PerformManipulationCustomStructArray<TStruct>(MaskableArrayManipulationOperation<TStruct> operation)
            where TStruct : unmanaged, ISizedStruct
        {
            PerformManipulation(new TStruct[ArrayLength], new TStruct[ArrayLength], operation);
        }

        protected override unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index) => ValueManipulation.NOT((TTarget)base.GetExpectedResult<TOrigin, TTarget>(origin, index));

        [Test]
        public unsafe void NOTStruct3Array()
        {
            NOTCustomStructArray<Struct3>();
        }
        [Test]
        public unsafe void NOTStruct5Array()
        {
            NOTCustomStructArray<Struct5>();
        }
        [Test]
        public unsafe void NOTStruct6Array()
        {
            NOTCustomStructArray<Struct6>();
        }
        [Test]
        public unsafe void NOTStruct7Array()
        {
            NOTCustomStructArray<Struct7>();
        }
        [Test]
        public unsafe void NOTStruct9Array()
        {
            NOTCustomStructArray<Struct9>();
        }
        [Test]
        public unsafe void NOTStruct10Array()
        {
            NOTCustomStructArray<Struct10>();
        }
        [Test]
        public unsafe void NOTStruct11Array()
        {
            NOTCustomStructArray<Struct11>();
        }
        [Test]
        public unsafe void NOTStruct12Array()
        {
            NOTCustomStructArray<Struct12>();
        }
        [Test]
        public unsafe void NOTStruct13Array()
        {
            NOTCustomStructArray<Struct13>();
        }
        [Test]
        public unsafe void NOTStruct14Array()
        {
            NOTCustomStructArray<Struct14>();
        }
        [Test]
        public unsafe void NOTStruct15Array()
        {
            NOTCustomStructArray<Struct15>();
        }
        [Test]
        public unsafe void NOTStruct17Array()
        {
            NOTCustomStructArray<Struct17>();
        }
        [Test]
        public unsafe void NOTStruct18Array()
        {
            NOTCustomStructArray<Struct18>();
        }
        [Test]
        public unsafe void NOTStruct19Array()
        {
            NOTCustomStructArray<Struct19>();
        }
        [Test]
        public unsafe void NOTStruct20Array()
        {
            NOTCustomStructArray<Struct20>();
        }
        [Test]
        public unsafe void NOTStruct21Array()
        {
            NOTCustomStructArray<Struct21>();
        }
        [Test]
        public unsafe void NOTStruct22Array()
        {
            NOTCustomStructArray<Struct22>();
        }
        [Test]
        public unsafe void NOTStruct23Array()
        {
            NOTCustomStructArray<Struct23>();
        }
        [Test]
        public unsafe void NOTStruct24Array()
        {
            NOTCustomStructArray<Struct24>();
        }
        [Test]
        public unsafe void NOTStruct25Array()
        {
            NOTCustomStructArray<Struct25>();
        }
        [Test]
        public unsafe void NOTStruct26Array()
        {
            NOTCustomStructArray<Struct26>();
        }
        [Test]
        public unsafe void NOTStruct27Array()
        {
            NOTCustomStructArray<Struct27>();
        }
        [Test]
        public unsafe void NOTStruct28Array()
        {
            NOTCustomStructArray<Struct28>();
        }
        [Test]
        public unsafe void NOTStruct29Array()
        {
            NOTCustomStructArray<Struct29>();
        }
        [Test]
        public unsafe void NOTStruct30Array()
        {
            NOTCustomStructArray<Struct30>();
        }
        [Test]
        public unsafe void NOTStruct31Array()
        {
            NOTCustomStructArray<Struct31>();
        }
    }
}
