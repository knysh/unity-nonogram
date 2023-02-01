using System.Collections.Generic;
using NoNoGramBackend.Squares;

namespace NoNoGramBackend.Models
{
    public class SquareInfoColumn
    {
        public List<SquareInfo> Column { get; set; }

        public List<int> GetBlackLines()
        {
            return SquareUtil.GetBlackLinesCounters(Column);
        }
    }
}
