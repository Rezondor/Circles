using Circles.Commands;
using Circles.Helpers;
using Circles.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;

namespace Circles.ViewModels;

internal class MainViewModel : INotifyPropertyChanged
{
    private ObservableCollection<CircleModel> _circlesList;
    private Timer _timer;
    private int _radius;
    private int _cornerPerTick;
    private int _additionalCorner;

    private Point _startPoint;

    private ICommand _addCircle;

    private ColorsHelper _colorsHelper;

    public MainViewModel()
    {
        CirclesList = new ObservableCollection<CircleModel>();
        _startPoint = new Point(250, 250);
        _radius = 100;
        _cornerPerTick = 5;
        _colorsHelper = new ColorsHelper();

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
                    var newPoint = PositionHelper.PositionCalculation(_startPoint, _radius, CirclesList.Count + 1, i + 1, _additionalCorner);
                    var pp = CirclesList[i];
                    pp.X = newPoint.X - _radius / 4;
                    pp.Y = newPoint.Y - _radius / 4;
                }

                var positionForNewCircle = PositionHelper.PositionCalculation(_startPoint, _radius, CirclesList.Count + 1, CirclesList.Count + 1, _additionalCorner);

                var newCircle = new CircleModel
                {
                    X = positionForNewCircle.X - _radius / 4,
                    Y = positionForNewCircle.Y - _radius / 4,
                    Radius = _radius / 4,
                    BorderColor = new SolidColorBrush(_colorsHelper.GetRandomColor()),
                    FillColor = new SolidColorBrush(_colorsHelper.GetRandomColor()),
                };
                CirclesList.Add(newCircle);
            }, m => CirclesList.Count < 10);
        }
    }

    private void UpdatePosition()
    {
        for (int i = 0; i < CirclesList.Count; i++)
        {
            var newPoint = PositionHelper.PositionCalculation(_startPoint, _radius, CirclesList.Count, i + 1, _additionalCorner);
            var pp = CirclesList[i];
            pp.X = newPoint.X - _radius / 4;
            pp.Y = newPoint.Y - _radius / 4;
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
