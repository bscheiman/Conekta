#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class BaseObject {
        [DataMember(Name = "created_at")]
        public int CreatedAt { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        public bool IsError {
            get { return Object == "error"; }
        }

        [DataMember(Name = "livemode")]
        public bool LiveMode { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "message_to_purchaser")]
        public string MessageToPurchaser { get; set; }

        [DataMember(Name = "object")]
        public string Object { get; set; }
    }
}