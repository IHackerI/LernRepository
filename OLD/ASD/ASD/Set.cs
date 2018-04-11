using System;
using System.Collections.Generic;
using System.Linq;

namespace ASD
{
	public class Set<T>
	{
		T[] _items;
		public int Count { get; private set; }

		public bool IsContain(T obj){
			var index = IndexOf(obj);
			return index > -1;
		}

		public int IndexOf(T obj){
			for(int i = 0 ; i < Count; i++){
				if (_items[i].Equals(obj))
					return i;
			}
			return -1;
		}

		public void Add(T obj){
			if (IndexOf(obj) > -1 ){
				return;
			}

			CheckArray(Count);

			_items [Count] = obj;
			Count++;
		}

		public void AddRange(IList<T> range){
			var ar = (from x in range where !IsContain(x) select x).ToArray();

			var finalLen = ar.Length + Count;

			CheckArray(finalLen-1);

			for (int i = 0; i < ar.Length; i++){
				_items [Count + i] = ar [i];
			}

			Count = finalLen;
		}

		public static Set<TT> Union<TT>(Set<TT> FirstRange, Set<TT> SecondRange)
		{
			var ans = new Set<TT> ();
			ans.AddRange (FirstRange.ToArray());
			ans.AddRange (SecondRange.ToArray());
			return ans;
		}

		public static Set<TT> Intersection<TT>(Set<TT> FirstRange, Set<TT> SecondRange)
		{
			var ans = new Set<TT> ();

			var firstAr = FirstRange.ToArray ();
			var secondAr = SecondRange.ToArray ();

			ans.AddRange ((from x in firstAr where SecondRange.IsContain(x) select x).ToArray());
			ans.AddRange ((from x in secondAr where FirstRange.IsContain(x) select x).ToArray());

			return ans;
		}

		public static Set<TT> Addition<TT>(Set<TT> FirstRange, Set<TT> SecondRange)
		{
			var ans = new Set<TT> ();

			var firstAr = FirstRange.ToArray ();
			var secondAr = SecondRange.ToArray ();

			ans.AddRange ((from x in firstAr where !SecondRange.IsContain(x) select x).ToArray());
			ans.AddRange ((from x in secondAr where !FirstRange.IsContain(x) select x).ToArray());

			return ans;
		}

		public bool Remove(T obj)
		{
			var index = IndexOf (obj);
			if (index < 0)
				return false;
			RemoveAt(index);
			return true;
		}

		public void RemoveAt(int index){
			for (int i = index; i < Count-1; i++){
				_items [i] = _items [i + 1];
			}
			Count--;
		}

		void CheckArray(int index){
			if (_items == null)
			{
				_items = new T[index+1];
				return;
			}

			int newLen = _items.Length;

			while (newLen <= index)
			{
				newLen <<= 1;
			}

			if (newLen > _items.Length){
				T[] ans = new T[newLen];

				for(int i = 0; i < _items.Length; i++){
					ans [i] = _items [i];
				}

				_items = ans;
			}
		}
	
		public IList<T> ToArray(){
			var ans = new T[Count];

			for (int i = 0; i < ans.Length; i++){
				ans [i] = _items [i];
			}
			return ans;
		}
	}
}

