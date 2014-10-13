namespace Conekta.Objects {
    public class Card : BaseObject {
        public bool active { get; set; }
        public string brand { get; set; }
        public string customer_id { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string last4 { get; set; }
        public string name { get; set; }
    }
}