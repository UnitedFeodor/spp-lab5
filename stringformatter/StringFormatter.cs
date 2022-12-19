using System.Text;

namespace stringformatter
{
    public class StringFormatter : IStringFormatter
    {
        public static readonly StringFormatter Shared = new StringFormatter();

        private FSM _finalStateMachine = new FSM();

        private readonly ExpressionTreeCache _cache = new ExpressionTreeCache();

        public string Format(string template, object target)
        {
            _finalStateMachine.ActiveState = 1;
            StringBuilder stringBuilderRes = new();
            
            int expressionStart = 0;
            for (int i = 0; i < template.Length; i++)
            {
                
                _finalStateMachine.GetNextState(template[i]);

                switch (_finalStateMachine.ActiveState)
                {
                    case 0:
                        {
                            throw new ArgumentException($"Invalid symbol '{template[i]}' at position {i}");
                        }
                    case 1:
                        {
                            if (_finalStateMachine.PreviousState == 4 || _finalStateMachine.PreviousState == 7) //variable name or array element end
                            {
                                // cache stuff
                            }
                            else
                            {
                                stringBuilderRes.Append(template[i]);
                            }
                            break;
                        }
                    case 4:
                        {
                            if (_finalStateMachine.PreviousState == 2)
                            {

                                expressionStart = i;
                            }
                            break;
                        }
                }

            }
            if (_finalStateMachine.ActiveState == 1)
            {
                return stringBuilderRes.ToString();
            }
            else
            {
                throw new ArgumentException($"Invalid symbol '{template[template.Length - 1]}' at position {template.Length - 1}");
            }
        }
    }

}

    
