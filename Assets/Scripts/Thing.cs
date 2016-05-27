
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    /// <summary>
    /// Class representing a event.
    /// </summary>
    [Serializable]
    public class Thing: IComparable
    {
        public int id;
        public DataType type;
        public string thing;
        public int start;
        public string location;

        public Thing(int id, string thing, int start, string location)
        {
            this.id = id;
            this.thing = thing;
            this.start = start;
            this.location = location;
        }

        public int CompareTo(object obj)
        {
            return start - ((Thing)obj).start;
        }
    }
}