using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_gastosPessoais.Misc
{
    interface IWindowHost
    {
        void Show(object context);
        bool? ShowDialog(object context);
    }
}
