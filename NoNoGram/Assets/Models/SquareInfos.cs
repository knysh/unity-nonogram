using System;
using System.Collections.Generic;

namespace NoNoGramBackend
{
    [Serializable]
    public class SquareInfos
    {
        public List<SquareInfoColumn> squares;

        public RowCounters rowCounters;

        public ColumnCounters columnCounters;
    }
}
