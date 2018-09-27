using System;
using System.Collections.Generic;
using System.Text;

namespace FriendProximityAPI.Domain.Entities
{
    public class Node
    {
        private List<Node> childNodes;

        public Node(Point maxPoint, Point minPoint, Node parentNode = null)
        {
            this.MaxPoint = maxPoint;
            this.MinPoint = minPoint;
            this.ParentNode = parentNode;
        }

        /// <summary>
        /// Vertice superior
        /// </summary>
        public Point MaxPoint { get; private set; }

        /// <summary>
        /// Vertice inferior
        /// </summary>
        public Point MinPoint { get; private set; }

        public Node ParentNode { get; private set; }

        public List<Node> ChildNodes
        {
            get
            {
                if (this.childNodes == null)
                    this.childNodes = new List<Node>();

                return this.childNodes;
            }
            private set => this.childNodes = value;
        }

        public Node SplitNode(int times)
        {
            if (times == 0)
                return this;
            else if (times % 2 == 0)
                NewLongitudeChildNodes((this.MaxPoint.Longitude - this.MinPoint.Longitude) / 2, times);
            else
                NewLatitudeChildNodes((this.MaxPoint.Latitude - this.MinPoint.Latitude) / 2, times);

            return this;
        }

        public Node GetLeaf(Point point)
        {
            if (ChildNodes.Count == 0)
                return this;

            var child = ChildNodes.Find(node => node.IsInside(point));

            return child.GetLeaf(point);
        }

        public bool IsInside(Point point)
            => this.MaxPoint.Latitude >= point.Latitude && this.MaxPoint.Longitude >= point.Longitude &&
                this.MinPoint.Latitude <= point.Latitude && this.MinPoint.Longitude <= point.Longitude;

        public Node GetSiblingNode() => this.ParentNode.childNodes.Find(n => !n.Equals(this));

        private void NewLongitudeChildNodes(int newLongitude, int times)
        {
            AddChildNode(new Point(this.MaxPoint.Latitude, newLongitude), this.MinPoint, times);
            AddChildNode(this.MaxPoint, new Point(this.MinPoint.Latitude, newLongitude), times);
        }

        private void NewLatitudeChildNodes(int newLatitude, int times)
        {
            AddChildNode(new Point(newLatitude, this.MaxPoint.Longitude), this.MinPoint, times);
            AddChildNode(this.MaxPoint, new Point(newLatitude, this.MinPoint.Longitude), times);
        }

        private void AddChildNode(Point maxPoint, Point minPoint, int times)
            => this.ChildNodes.Add(new Node(maxPoint, minPoint, this).SplitNode(times));
    }
}
