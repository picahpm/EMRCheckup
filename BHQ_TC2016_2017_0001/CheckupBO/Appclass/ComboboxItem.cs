using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckupBO
{
   public class ComboboxItem
    {
       public ComboboxItem(string txt, object value)
       {
           Text = txt;
           Value = value;
       }
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
