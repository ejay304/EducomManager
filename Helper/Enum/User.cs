using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper.Enum
{
    public class User : EnumDb
    {
        public static readonly User adviser = new User("adviser", "Conseiller");
        public static readonly User assistant = new User("assistant", "Assistant");
        public static readonly User administrator = new User("administrator", "Administrateur");

        public static List<User> list { get; set; }
            
        private User(String value, String name) : base(value,name) {
        }

        public static int indexByValue(String value)
        {
            for (int i = 0; i < User.list.Count; i++)
                if (String.Compare(User.list.ElementAt(i).getValue(), value) == 0)
                    return i;
            return -1;
        }

        static User()
        {
            list = new List<User>();
            list.Add(adviser);
            list.Add(assistant);
            list.Add(administrator);
        }
        
    }
}
