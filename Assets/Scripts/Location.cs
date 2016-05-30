
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    /// <summary>
    /// Class representing a location.
    /// </summary>
    [Serializable]
    public class Location: IComparable
    {
        public int id;
        public DataType type;
        public string location;
        public string describtion;
        public string[] bssids;
        public int navigate;

        public Location(int id, string location, string describtion, string bssids, int navigate)
        {
            this.id = id;
            this.location = location;
            this.describtion = describtion;
            this.bssids = bssids.Split(new Char[] { ' ' });
            this.navigate = navigate;
        }

        public int CompareTo(object obj)
        {
            return id - ((Location)obj).id;
        }
    }
}