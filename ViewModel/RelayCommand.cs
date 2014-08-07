using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel
{
    /// <filename>RelayCommand.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe utilisée dans les viewModèle pour réaliser la relation entre une commande et l'action à réaliser</summary>
    public class RelayCommand<T> : ICommand
    {
        Action<T> _TargetExecuteMethod;

        Func<T, bool> _TargetCanExecuteMethod;

        /// <summary>
        /// Va déléguer la commande à une action
        /// </summary>
        /// <param name="executeMethod"></param>
        public RelayCommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }


        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand Members
        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                T tparm = (T)parameter;

                return _TargetCanExecuteMethod(tparm);
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        // Beware - should use weak references if command instance lifetime is longer than lifetime of UI objects that get hooked up to command

        // Prism commands solve this in their implementation

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((T)parameter);
            }
        }
        #endregion

    }
}

