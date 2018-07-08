namespace Leo.Actors.Interfaces.Boards
{
    public class PaymentMeta
    {
        public PaymentMeta(string payeeName, string ppdid, string referenceNumber)
        {
            PayeeName = payeeName;
            PpdId = ppdid;
            ReferenceNumber = referenceNumber;
        }

        public string PayeeName { get; }

        public string PpdId { get; }

        public string ReferenceNumber { get; }
    }
}