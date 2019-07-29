using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf_gastosPessoais.Misc
{
    public class WindowHost : IWindowHost
    {
        public WindowHost()
        {
            window = new WindowHostView();
        }

        protected Window window;

        public virtual void Show(object context)
        {
            window.DataContext = context;
            window.Show();
        }

        public virtual bool? ShowDialog(object context)
        {
            window.DataContext = context;
            return window.ShowDialog();
        }
    }
}
