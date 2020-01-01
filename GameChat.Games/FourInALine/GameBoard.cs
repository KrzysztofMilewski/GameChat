using System.Collections.Generic;
using System.Linq;

namespace GameChat.Games.FourInALine
{
    public class GameBoard
    {
        public List<BoardField> Fields { get; set; }

        public GameBoard()
        {
            Fields = new List<BoardField>(64);

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                    Fields.Add(new BoardField(x, y));
            }
        }

        public FieldState this[int x, int y]
        {
            get
            {
                return Fields.SingleOrDefault(f => f.X == x && f.Y == y).FieldState;
            }
            set
            {
                var field = Fields.SingleOrDefault(f => f.X == x && f.Y == y);
                field.FieldState = value;
            }
        }


        public List<BoardField> GetRow(int y)
        {
            return Fields.Where(f => f.Y == y).OrderBy(f => f.X).ToList();
        }

        public List<BoardField> GetColumn(int x)
        {
            return Fields.Where(f => f.X == x).OrderBy(f => f.Y).ToList();
        }

        public List<BoardField> GetBoardForDisplay()
        {
            return Fields.OrderByDescending(f => f.Y).ThenBy(f => f.X).ToList();
        }
    }
}
