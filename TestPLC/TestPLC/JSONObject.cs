using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPLC
{
    public class JSONObject
    { /// <summary>
      /// 
      /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DataItem> Data { get; set; }
    }
}
