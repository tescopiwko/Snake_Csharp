using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Snake_C_
{
    internal class Snake1
    {
        public List<Point> Body { get; private set; } = new List<Point>();
        public Point Direction { get; set; } = new Point(1, 0);

        public Snake1()
        {
            Reset();
        }

        public void Reset()
        {
            Body.Clear();
            Body.Add(new Point(10, 10));
            Body.Add(new Point(9, 10));
            Body.Add(new Point(8, 10));
            Direction = new Point(1, 0);
        }

        public void Move()
        {
            Point newHead = new Point(Body[0].X + Direction.X, Body[0].Y + Direction.Y);

            Body.Insert(0, newHead);
            Body.RemoveAt(Body.Count - 1);
        }

        public void Grow()
        {
            Body.Add(Body[Body.Count - 1]);
        }

        public bool CollidesWithSelf()
        {
            for (int i = 1; i < Body.Count; i++)
            {
                if (Body[0] == Body[i])
                    return true;

            }
            return false;
        }

        public bool CollidesWithWalls(int maxX, int maxY)
        {
            return
                Body[0].X < 0 || Body[0].X >= maxX ||
                Body[0].Y < 0 || Body[0].Y >= maxY;
        }

    }
}
    
