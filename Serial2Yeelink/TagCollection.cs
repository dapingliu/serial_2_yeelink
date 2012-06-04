using System;
using System.Collections.Generic;
using System.Text;

namespace Location_Tracking
{
    class TagCollection
    {

        public string tagAddr;
        public DateTime firstSeen;
        public List<TagReaderCollection> readers = new List<TagReaderCollection>();

    }
}
