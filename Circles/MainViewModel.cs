using Circles.Commands;
using Circles.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;

namespace Circles;

internal class MainViewModel : INotifyPropertyChanged
{
    private ObservableCollection<CircleModel> _circlesList;
    private Point _startPoint;
    private int _radius;
    private ICommand _addCircle;
    public MainViewModel()
    {
        CirclesList = new ObservableCollection<CircleModel>();
        _startPoint = new Point(200, 200);
        _radius = 100;
    }
    public ObservableCollection<CircleModel> CirclesList
    {
        get => _circlesList;
        set
        {
            _circlesList = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddCircle
    {
        get
        {
            return _addCircle ??= new RelayCommand((obj) =>
            {

                for (int i = 0; i < CirclesList.Count; i++)
                {
                    var newPoint = PositionCalculation(_startPoint, _radius, CirclesList.Count + 1, i);
                    var pp = CirclesList[i];
                    pp.X = newPoint.X;
                    pp.Y = newPoint.Y;
                }

                var positionForNewCircle = PositionCalculation(_startPoint, _radius, CirclesList.Count + 1, CirclesList.Count);

                var newCircle = new CircleModel
                {
                    X = positionForNewCircle.X,
                    Y = positionForNewCircle.Y,
                    Radius = _radius / 4,
                    BorderColor = new SolidColorBrush(Colors.Black),
                    FillColor = new SolidColorBrush(Colors.AliceBlue),
                };
                CirclesList.Add(newCircle);
            }, m => CirclesList.Count < 10);
        }
    }

    private Point PositionCalculation(Point startPoint, int radius, int circleCount, int circleNumber)
    {
        if (circleCount <= 1)
        {
            return new Point(startPoint.X, startPoint.Y);
        }

        double corner = 360.0 / (circleCount <= 0 ? 1 : circleCount);
        double angle = Math.PI * (circleNumber * corner) / 180.0;

        (double sinAngle, double cosAngle) = Math.SinCos(angle);

        var x = startPoint.X + radius * cosAngle;
        var y = startPoint.Y + radius * sinAngle;

        return new Point((int)Math.Round(x), (int)Math.Round(y));
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
