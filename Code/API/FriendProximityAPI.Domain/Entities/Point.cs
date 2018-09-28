using FriendProximityAPI.Shared.Entities;
using Serilog;
using System;
using System.Linq;

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
        {
            var result = Math.Sqrt(Math.Pow(Math.Abs(Longitude - point.Longitude), 2) + Math.Pow(Math.Abs(this.Latitude - point.Latitude), 2));
            Log.Information($"{result} = Math.Sqrt(Math.Pow(Math.Abs({Longitude} - {point.Longitude}), 2) + Math.Pow(Math.Abs({this.Latitude} - {point.Latitude}), 2))");
            return result;
        }

        internal double CalculateGroupDistance(Node node)
        {
            var dx = new[] { node.MinPoint.Longitude - this.Longitude, this.Longitude - node.MaxPoint.Longitude }.Max();
            var dy = new[] { node.MinPoint.Latitude - this.Latitude, this.Latitude - node.MaxPoint.Latitude }.Max();
            var result = Math.Sqrt(dx * dx + dy * dy);
            Log.Information($"Math.Sqrt({dx} * {dx} + {dy} * {dy})");
            return result;
        }
    }
}
