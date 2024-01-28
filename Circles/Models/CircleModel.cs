using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Circles.Models;

internal class CircleModel : INotifyPropertyChanged
{
    private int _x;
    private int _y;
    private int _radius;

    private Brush _fillColor;
    private Brush _borderColor;

    public int X
    {
        get => _x;
        set
        {
            _x = value;
            OnPropertyChanged();
        }
    }

    public int Y
    {
        get => _y;
        set
        {
            _y = value;
            OnPropertyChanged();
        }
    }

    public int Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            OnPropertyChanged();
        }
    }

    public Brush FillColor
    {
        get => _fillColor;
        set
        {
            _fillColor = value;
            OnPropertyChanged();
        }
    }

    public Brush BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            OnPropertyChanged();
        }
    }

    #region MVVM
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    #endregion
}
