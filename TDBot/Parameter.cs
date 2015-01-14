using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDBot
{
    class Parameter
    {
        private String name;
        private String value;
        private const char separator = ':';

        public Parameter(String ParamString)
        {
            int index;
            if (ParamString == null || ParamString.Length == 0)
            {
                name = "null";
                value = "null";
            }

            index = ParamString.IndexOf(separator);
            if (index < 0)     // no separators
            {
                name = ParamString.Trim();
                value = "null";
                if (name.Length == 0) name = "null";
            }
            else
            {
                name = ParamString.Substring(0, index).Replace(separator, ' ').Trim();
                value = ParamString.Substring(index).Replace(separator, ' ').Trim();
                if (name.Length == 0) name = "null";
                if (value.Length == 0) value = "null";
            }
        }

        public override String ToString()
        {
            return (this.name + separator + this.value);
        }

        public String GetValue()
        {
            return this.value;
        }

        public String GetName()
        {
            return this.name;
        }

        public void SetValue(String val)
        {
            this.value = val;
        }
    }
}