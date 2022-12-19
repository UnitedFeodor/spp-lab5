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
        private const string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const string _digits = "0123456789";
        private const string _underscore = "_";
        private const string _curlyBracketOpen = "{";
        private const string _curlyBracketClose = "}";
        private const string _squareBracketOpen = "[";
        private const string _squareBracketClose = "]";

        private string[] _states = { _errors, _letters, _digits, _underscore, _curlyBracketOpen, _curlyBracketClose, _squareBracketOpen, _squareBracketClose };


        private int[,] _transitions =
        {
            // unknown letter digit _ {  }  [  ]
            {0, 0, 0, 0, 0, 0, 0, 0 },  // 0 - error
            {1, 1, 1, 1, 2, 3, 1, 1 },  // 1
            {0, 4, 0, 0, 1, 0, 0, 0 },  // 2
            {0, 0, 0, 0, 0, 1, 0, 0 },  // 3
            {0, 4, 4, 4, 0, 1, 5, 0 },  // 4
            {0, 0, 6, 0, 0, 0, 0, 0 },  // 5
            {0, 0, 6, 0, 0, 0, 0, 7 },  // 6
            {0, 0, 0, 0, 0, 1, 0, 0 }   // 7
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
           

            int res = _transitions[ActiveState, Array.FindIndex(_states, x => x.Contains(c))];
            ActiveState = res;
            return res;
        }

    }
}
