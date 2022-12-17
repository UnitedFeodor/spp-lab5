using System.Text;

namespace stringformatter
{
    public class StringFormatter : IStringFormatter
    {
        public static readonly StringFormatter Shared = new StringFormatter();

        private FSM _finalStateMachine = new FSM();
        public string Format(string template, object target)
        {
            
        }
    }

    
}