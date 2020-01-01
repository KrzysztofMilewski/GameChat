namespace GameChat.Games.FourInALine
{
    public class BoardField
    {
        private FieldState _fieldState;

        public BoardField(int x, int y)
        {
            X = x;
            Y = y;
            _fieldState = FieldState.Empty;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public FieldState FieldState
        {
            get { return _fieldState; }
            set
            {
                //once set, it cannot be changed anymore
                if (_fieldState != FieldState.Empty)
                    return;

                _fieldState = value;
            }
        }
    }
}
