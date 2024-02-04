using System.Drawing;

namespace Circles.Helpers;

public static class PositionHelper
{
    public static Point PositionCalculation(Point startPoint, int radius, int circleCount, int circleNumber, int additionalCorner = 0)
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
}
