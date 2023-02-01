using System.Collections.Generic;
using NoNoGramBackend.Squares;

namespace NoNoGramBackend.Models
{
    public class SquareInfoRow
    {
        public List<SquareInfo> Row { get; set; }

        public List<int> GetBlackLines()
        {
            return SquareUtil.GetBlackLinesCounters(Row);
        }
    }
}
