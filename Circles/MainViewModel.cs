using Circles.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Circles;

internal class MainViewModel : INotifyPropertyChanged
{
    private ObservableCollection<FigureModel> figures;

    public MainViewModel()
    {
        Figures = new ObservableCollection<FigureModel>()
        {
            new FigureModel()
            {
                X = 50,
                Y = 50,
                Radius = 30
            },
            new FigureModel()
            {
                X = 100,
                Y = 50,
                Radius = 40
            },
            new FigureModel()
            {
                X = 150,
                Y = 50,
                Radius = 20
            },
        };
    }

    public ObservableCollection<FigureModel> Figures
    {
        get => figures;
        set
        {
            figures = value;
            //OnPropertyChanged();
        }
    }


    #region MVVM 
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    #endregion
}
