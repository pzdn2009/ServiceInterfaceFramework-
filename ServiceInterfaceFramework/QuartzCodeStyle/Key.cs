using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework.QuartzCodeStyle
{
    [Serializable]
    public class Key<T> : IComparable<Key<T>>
    {
        public const string DefaultGroup = "DEFAULT";

        private readonly string name;
        private readonly string group;

        public Key(string name, string group)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name", "Name cannot be null.");
            }
            this.name = name;
            if (group != null)
            {
                this.group = group;
            }
            else
            {
                this.group = DefaultGroup;
            }
        }

        public virtual string Name
        {
            get { return name; }
        }

        public virtual string Group
        {
            get { return group; }
        }

        public override string ToString()
        {
            return Group + '.' + Name;
        }


        public override int GetHashCode()
        {
            const int Prime = 31;
            int result = 1;
            result = Prime * result + ((group == null) ? 0 : group.GetHashCode());
            result = Prime * result + ((name == null) ? 0 : name.GetHashCode());
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            Key<T> other = (Key<T>)obj;
            if (group == null)
            {
                if (other.group != null)
                {
                    return false;
                }
            }
            else if (!group.Equals(other.group))
            {
                return false;
            }
            if (name == null)
            {
                if (other.name != null)
                {
                    return false;
                }
            }
            else if (!name.Equals(other.name))
            {
                return false;
            }
            return true;
        }

        public int CompareTo(Key<T> o)
        {
            if (group.Equals(DefaultGroup) && !o.group.Equals(DefaultGroup))
            {
                return -1;
            }
            if (!group.Equals(DefaultGroup) && o.group.Equals(DefaultGroup))
            {
                return 1;
            }

            int r = group.CompareTo(o.Group);
            if (r != 0)
            {
                return r;
            }

            return name.CompareTo(o.Name);
        }
    }
}
