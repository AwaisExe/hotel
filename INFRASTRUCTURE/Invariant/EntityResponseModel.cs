using System.Collections;

namespace INFRASTRUCTURE.Invariant
{
    public class EntityResponseModel<T>
    {
        public bool ReturnStatus { get; set; }
        public int StatusCode { get; set; }
        public List<string> ReturnMessage { get; set; }
        public Hashtable Errors;
        public T Data;
        public EntityResponseModel()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            Errors = new Hashtable();
            Data = default(T);
        }
    }
}
