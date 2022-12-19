using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringformatter
{
    public class FSM
    {
        private const string _errors = "";
        private const string _lettersDigitsWhitespace = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ ";

        private const string _curlyBracketOpen = "{";
        private const string _curlyBracketClose = "}";

        private string[] _states = { _errors, _lettersDigitsWhitespace, _curlyBracketOpen, _curlyBracketClose, };


        private int[,] _transitions =
        {
            // unknown letterDigit_ {  } 
            {0, 0, 0, 0, },  // 0 - error
            {1, 1, 2, 3, },  // 1
            {0, 4, 1, 0, },  // 2
            {0, 0, 0, 1, },  // 3
            {0, 4, 0, 1, }   // 4

        };


        private int _activeState;
        public int ActiveState
        {
            get => _activeState;
            set
            {
                PreviousState = _activeState;
                _activeState = value;
            }
        }
        public int PreviousState { get => _previousState; set => _previousState = value; }

        private int _previousState;

        public int GetNextState(char c)
        {

            int inputSymbolType = Array.FindIndex(_states, x => x.Contains(c));
            if (inputSymbolType == -1)
            {
                inputSymbolType = 0;
            }
            int res = _transitions[ActiveState, inputSymbolType];
            ActiveState = res;
            return res;
        }

    }
}
