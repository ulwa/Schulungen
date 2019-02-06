using System.Runtime.CompilerServices;
using System.ComponentModel;
namespace SageAufbaukursCSharp.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion INotifyPropertyChanged

        public void NotifyPropertyChanged([CallerMemberName]string nameProp = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProp));
        }
    }
}
