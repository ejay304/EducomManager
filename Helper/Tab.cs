using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrototypeEDUCOM.Helper
{
    public class Tab
    {
        public String header { get; set; }

        public UserControl content { get; set; }

        public String icon { get; set; }

        public Object entity { get; set; }
        public Tab(String header, UserControl content, Object entity, String icon)
        {
            this.header = header;
            this.content = content;
            this.entity = entity;
            this.icon = icon;
        }
    }
}
