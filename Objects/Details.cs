#region
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Details {
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "line_items")]
        public IList<object> LineItems { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "phone")]
        public object Phone { get; set; }

        internal Details() {
        }
    }
}