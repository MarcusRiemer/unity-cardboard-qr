
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

        public Location(int id, string location)
        {
            this.id = id;
            this.location = location;
        }

        public int CompareTo(object obj)
        {
            return id - ((Location)obj).id;
        }
    }
}