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
    private Timer _timer;
    private int _radius;
    private int _cornerPerTick;
    private int _additionalCorner;
    private Random _r;

    private Point _startPoint;

    private ICommand _addCircle;

    public MainViewModel()
    {
        CirclesList = new ObservableCollection<CircleModel>();
        _startPoint = new Point(250, 250);
        _radius = 100;
        _cornerPerTick = 5;
        _r = new Random();

        TimerCallback tm = new TimerCallback(obj =>
        {
            if (CirclesList.Count <= 1)
            {
                return;
            }
            AdditionalCorner += _cornerPerTick;
        });

        _timer = new Timer(tm, null, 0, 100);
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

    public int AdditionalCorner
    {
        get => _additionalCorner;
        set
        {
            _additionalCorner = value;
            UpdatePosition();
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
                    var newPoint = PositionCalculation(_startPoint, _radius, CirclesList.Count + 1, i + 1, _additionalCorner);
                    var pp = CirclesList[i];
                    pp.X = newPoint.X - (_radius / 4);
                    pp.Y = newPoint.Y - (_radius / 4);
                }

                var positionForNewCircle = PositionCalculation(_startPoint, _radius, CirclesList.Count + 1, CirclesList.Count + 1, _additionalCorner);

                var newCircle = new CircleModel
                {
                    X = positionForNewCircle.X - (_radius / 4),
                    Y = positionForNewCircle.Y - (_radius / 4),
                    Radius = _radius / 4,
                    BorderColor = new SolidColorBrush(GetRandomColor()),
                    FillColor = new SolidColorBrush(GetRandomColor()),
                };
                CirclesList.Add(newCircle);
            }, m => CirclesList.Count < 10);
        }
    }

    private System.Windows.Media.Color GetRandomColor()
    {
        return System.Windows.Media.Color.FromArgb(100, (byte)_r.Next(255), (byte)_r.Next(255), (byte)_r.Next(255));
    }

    private void UpdatePosition()
    {
        for (int i = 0; i < CirclesList.Count; i++)
        {
            var newPoint = PositionCalculation(_startPoint, _radius, CirclesList.Count, i+1, _additionalCorner);
            var pp = CirclesList[i];
            pp.X = newPoint.X - (_radius / 4);
            pp.Y = newPoint.Y - (_radius / 4);
        }
    }

    private Point PositionCalculation(Point startPoint, int radius, int circleCount, int circleNumber, int additionalCorner = 0)
    {
        if (circleCount <= 1)
        {
            return new Point(startPoint.X, startPoint.Y);
        }

        double corner = 360.0 / (circleCount <= 0 ? 1 : circleCount);
        double angle = Math.PI * (circleNumber * corner + additionalCorner) / 180.0;

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
