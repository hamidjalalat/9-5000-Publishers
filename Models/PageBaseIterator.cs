using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class PageBaseIterator: IEnumerable, IEnumerator
    {
        public PageBaseIterator(List<PageBaseFullList> PageBase)
        {
            PageBaseArray = PageBase;
            Reset();
        }
        public int Postion { get; set; }

        public List<PageBaseFullList> PageBaseArray { get; set; }

        public object Current
        {
            get
            {
                try
                {
                    return (PageBaseArray[Postion]);
                }
                catch (System.IndexOutOfRangeException)
                {
                    throw (new System.IndexOutOfRangeException());
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return (this);
        }

        public virtual bool MoveNext()
        {
            if (Postion == PageBaseArray.Count - 1)
            {
                return (false);
            }
            else
            {
                Postion++;

                return (true);
            }
        }
        public void Reset()
        {
            Postion = -1;
        }

    }
}
