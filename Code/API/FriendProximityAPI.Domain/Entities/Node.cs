using System.Collections.Generic;
using System.Linq;

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

        public Node SplitNode(List<Friend> friends, int times)
        {
            var friendsInsideNode = this.GetFriendsInsideNode(friends);
            var friendSelected = friendsInsideNode.FirstOrDefault();

            if (times == 0)
                return this;
            else if (times % 2 == 0)
                NewLongitudeChildNodes(friendSelected.Point.Longitude, friendsInsideNode, --times);
            else
                NewLatitudeChildNodes(friendSelected.Point.Latitude, friendsInsideNode, --times);

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

        public Node GetSiblingNode() => this.ParentNode?.childNodes?.Find(n => !n.Equals(this));

        public List<Friend> GetFriendsInsideNode(List<Friend> friends)
            => friends.Where(f => this.IsInside(f.Point)).ToList();

        private void NewLongitudeChildNodes(int newLongitude, List<Friend> friends, int times)
        {
            AddChildNode(new Point(this.MaxPoint.Latitude, newLongitude), this.MinPoint, friends, times);
            AddChildNode(this.MaxPoint, new Point(this.MinPoint.Latitude, newLongitude), friends, times);
        }

        private void NewLatitudeChildNodes(int newLatitude, List<Friend> friends, int times)
        {
            AddChildNode(new Point(newLatitude, this.MaxPoint.Longitude), this.MinPoint, friends, times);
            AddChildNode(this.MaxPoint, new Point(newLatitude, this.MinPoint.Longitude), friends, times);
        }

        private void AddChildNode(Point maxPoint, Point minPoint, List<Friend> friends, int times)
            => this.ChildNodes.Add(new Node(maxPoint, minPoint, this).SplitNode(friends, times));
    }
}
