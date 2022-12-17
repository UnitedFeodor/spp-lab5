using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringformatter
{
    public interface IStringFormatter
    {
        string Format(string template, object target);
    }
}
