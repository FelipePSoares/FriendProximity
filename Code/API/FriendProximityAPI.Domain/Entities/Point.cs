using FriendProximityAPI.Shared.Entities;
using System;

namespace FriendProximityAPI.Domain.Entities
{
    public class Point : Entity
    {
        public Point(int latitude, int longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public int Latitude { get; private set; }
        public int Longitude { get; private set; }
        
        public double CalculateDistance(Point point)
            => Math.Sqrt(Math.Pow(Math.Abs(this.Longitude - point.Longitude), 2) + Math.Pow(Math.Abs(this.Latitude - point.Latitude), 2));
    }
}
