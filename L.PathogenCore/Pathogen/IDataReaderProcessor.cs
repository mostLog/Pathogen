using System;
using System.Collections.Generic;
using System.Text;

namespace L.PathogenCore
{
    public interface IDataReaderProcessor
    {
        IList<InfectionTarget> Reader();
    }
}
