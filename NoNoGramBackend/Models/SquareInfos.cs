using System.Collections.Generic;
using System.Linq;
using NoNoGramBackend.Squares;

namespace NoNoGramBackend.Models
{
    public class SquareInfos
    {
        public List<SquareInfoColumn> Squares { get; set; }

        public ColumnCounters ColumnCounters { get; set; }

        public RowCounters RowCounters { get; set; }
    }
}
