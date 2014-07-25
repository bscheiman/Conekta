#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class BaseObject {
        [DataMember(Name = "created_at")]
        public long CreatedAt { get; internal set; }

        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        public bool IsError {
            get { return Object == "error"; }
        }

        [DataMember(Name = "livemode")]
        public bool LiveMode { get; internal set; }

        [DataMember(Name = "message")]
        public string Message { get; internal set; }

        [DataMember(Name = "object")]
        public string Object { get; internal set; }

        internal BaseObject() {
        }
    }
}