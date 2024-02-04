namespace Circles.Helpers;

public class ColorsHelper
{
    private readonly Random _r;
    public ColorsHelper()
    {
        _r = new Random();
    }

    public System.Windows.Media.Color GetRandomColor()
    {
        return System.Windows.Media.Color.FromArgb(100, (byte)_r.Next(255), (byte)_r.Next(255), (byte)_r.Next(255));
    }
}
