using System;
using System.Collections.Generic;
using System.Reflection;

namespace STA.Common.TypeFinder
{
    public interface ITypeFinder
    {
        IList<Assembly> GetFilteredAssemblyList();
    }
}
