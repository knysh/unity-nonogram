using System.Collections.Generic;
using System.Linq;
using NoNoGramBackend.Squares;

namespace NoNoGramBackend.Models
{
    public class SquareInfos
    {
        public List<SquareInfoRow> Squares { get; set; }

        public ColumnCouners ColumnCouners { get; set; }

        public RowCouners RowCouners { get; set; }
    }
}
