using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common;
using Riders.DL.Json;

namespace Riders.BL
{
    internal static class DataContextManager
    {
        public static DataContext Current { get; } = new JsonDataContext(".");
    }
}
