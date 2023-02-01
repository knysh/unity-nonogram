using NoNoGramBackend.Models;
using System.Collections.Generic;
using System.Linq;

namespace NoNoGramBackend.Squares
{
    public static class SquareUtil
    {
        public static List<int> GetBlackLinesCounters(List<SquareInfo> line)
        {
            var blackLines = new List<int>();
            var counter = 0;
            line.ForEach(square =>
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

            return blackLines;
        }

        public static ColumnCouners GetColumnCouners(List<SquareInfoRow> squares)
        {
            var columnsCounters = new List<LineCouners>();

            GetColumns(squares).ForEach(column =>
            {
                var lineCouners = new LineCouners
                {
                    Couners = GetBlackLinesCounters(column)
                };


                columnsCounters.Add(lineCouners);
            });

            return new ColumnCouners
            {
                LineCouners = columnsCounters
            };
        }

        private static List<List<SquareInfo>> GetColumns(List<SquareInfoRow> squares)
        {
            var columns = new List<List<SquareInfo>>();

            for (int i = 0; i < squares.First().Row.Count; i++)
            {
                var column = new List<SquareInfo>();
                squares.ForEach(row =>
                {
                    column.Add(row.Row[i]);
                });

                columns.Add(column);
            }

            return columns;
        }

    }
}
