using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Garyon.Tests.Resources
{
    public static class AssertionHelpers
    {
        /// <summary>Ignores the current test, throwing the appropriate exception and displays a message related to unsupported instruction set.</summary>
        public static void UnsupportedInstructionSet()
        {
            Assert.Ignore("The system does not support the required instruction set to test this function.");
        }
    }
}
