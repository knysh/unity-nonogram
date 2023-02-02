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

        public static ColumnCounters GetColumnCounters(List<SquareInfoColumn> columns)
        {
            var columnsCounters = new List<LineCounters>();
            columns.ForEach(column =>
            {
                var lineCounter = new LineCounters
                {
                    Counters = column.GetBlackLines()
                };

                columnsCounters.Add(lineCounter);
            });

            return new ColumnCounters
            {
                LineCounters = columnsCounters
            };
        }

        public static RowCounters GetRowsCounters(List<SquareInfoColumn> squares)
        {
            var rowsCounters = new List<LineCounters>();

            GetRows(squares).ForEach(row =>
            {
                var lineCounters = new LineCounters
                {
                    Counters = GetBlackLinesCounters(row)
                };


                rowsCounters.Add(lineCounters);
            });

            return new RowCounters
            {
                LineCounters = rowsCounters
            };
        }

        private static List<List<SquareInfo>> GetRows(List<SquareInfoColumn> squares)
        {
            var rows = new List<List<SquareInfo>>();

            for (int i = 0; i < squares.First().Column.Count; i++)
            {
                var row = new List<SquareInfo>();
                squares.ForEach(column =>
                {
                    row.Add(column.Column[i]);
                });

                rows.Add(row);
            }

            return rows;
        }

    }
}
