using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotfixProject.HotFix.UI
{
    public class BindComponentAttribute : Attribute
    {
        private string component;

        public BindComponentAttribute(string component)
        {
            this.component = component;
        }

        public string Component
        {
            get
            {
                return component;
            }
        }
    }
}
