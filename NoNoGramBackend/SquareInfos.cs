using System.Collections.Generic;
using System.Linq;

namespace NoNoGramBackend
{
    public class SquareInfos
    {
        public List<SquareInfoRow> Squares { get; set; }

        public List<List<int>> GetBlackColumnLines ()
        {
            var columnsBlackLines = new List<List<int>> ();

            GetColumns().ForEach(column =>
            {
                var blackLines = new List<int>();

                var counter = 0;
                column.ForEach(square =>
                {
                    if (square.Color.Equals(Color.BLACK))
                    {
                        counter++;
                    }

                    if (square.Color.Equals(Color.WHITE) && counter > 0)
                    {
                        blackLines.Add(counter);
                        counter = 0;
                    }
                });

                columnsBlackLines.Add(blackLines);
            });


            return columnsBlackLines;
        }

        public List<List<SquareInfo>> GetColumns()
        {
            var columns = new List<List<SquareInfo>>();

            for (int i = 0; i < Squares.First().Row.Count; i++)
            {
                var column = new List<SquareInfo>();
                Squares.ForEach(row =>
                {
                    column.Add(row.Row[i]);
                });

                columns.Add(column);
            }

            return columns;
        }
    }
}
