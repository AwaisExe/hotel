using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRASTRUCTURE.Invariant
{
    public class EntityResponseListModel<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long Total { get; set; }
        public bool ReturnStatus { get; set; }
        public int StatusCode { get; set; }
        public List<String> ReturnMessage { get; set; }
        public Hashtable Errors;
        public List<T> Data;
        public EntityResponseListModel()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            Errors = new Hashtable();
            Data = default;            
        }
    }
}
