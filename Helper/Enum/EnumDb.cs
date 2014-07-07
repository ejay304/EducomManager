using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper.Enum
{
    public abstract class EnumDb
    {
        public String name { get; set; }
        protected readonly String value;

        protected EnumDb(String value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }


        public String getValue()
        {
            return value;
        }
    }
}
