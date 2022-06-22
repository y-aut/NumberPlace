using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace
{
    [Serializable]
    public struct NumList
    {
        public static readonly NumList Zero = new NumList() { data = 0 };
        public static readonly IEnumerable<NumList> Nums =
            Enumerable.Range((int)Number.NUM_ZERO, (int)Number.NUM_NB).Select(i => new NumList((Number)i));
        public static readonly NumList All = new NumList(_ => true);

        private int data;

        private NumList(Number num)
        {
            data = 1 << (int)num;
        }

        public NumList(Predicate<Number> pred)
        {
            data = 0;
            for (Number i = Number.NUM_ZERO; i < Number.NUM_NB; ++i)
            {
                if (pred(i)) data |= Nums.ElementAt((int)i).data;
            }
        }

        public NumList(List<Number> list)
        {
            data = 0;
            foreach (var num in list)
                this[num] = true;
        }

        public bool this[Number num]
        {
            get => (bool)(this & new NumList(num));
            set
            {
                if (value) this |= new NumList(num);
                else this &= ~new NumList(num);
            }
        }

        public List<Number> ToList()
        {
            var list = new List<Number>();
            for (var i = Number.NUM_ZERO; i < Number.NUM_NB; ++i)
                if (this[i]) list.Add(i);
            return list;
        }

        public static bool operator ==(NumList left, NumList right) => left.data == right.data;
        public static bool operator !=(NumList left, NumList right) => !(left == right);
        public static NumList operator &(NumList left, NumList right) => new NumList() { data = left.data & right.data };
        public static NumList operator |(NumList left, NumList right) => new NumList() { data = left.data | right.data };
        public static NumList operator ^(NumList left, NumList right) => new NumList() { data = left.data ^ right.data };
        public static NumList operator ~(NumList left) => left ^ All;
        public static explicit operator bool(NumList left) => left != Zero;

        public override bool Equals(object obj) => obj is NumList l && this == l;
        public override int GetHashCode() => data;
    }
}
