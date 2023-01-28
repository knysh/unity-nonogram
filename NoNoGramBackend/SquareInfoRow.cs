using System.Collections.Generic;

namespace NoNoGramBackend
{
    public class SquareInfoRow
    {
        public List<SquareInfo> Row { get; set; }

        public List<int> GetBlackLines ()
        {
            var blackLines = new List<int>();
            var counter = 0;
            Row.ForEach(square =>
            {
                if(square.Color.Equals(Color.BLACK))
                {
                    counter++;
                }

                if(square.Color.Equals(Color.WHITE) && counter > 0)
                {
                    blackLines.Add(counter);
                    counter = 0;
                }
            });

            return blackLines;
        }

    }
}
