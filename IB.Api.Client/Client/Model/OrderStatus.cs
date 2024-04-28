namespace IB.Api.Client.Model
{
    public static class OrderStatus
    {
        public const string UNKNOWN = "UNKNOWN";
        public const string FILLED = "Filled";
        public const string CANCELLED = "Cancelled";
        public const string PENDING_CANCEL = "PendingCancel";
        public const string INACTIVE = "Inactive";
        public const string ACTIVE = "Active";
        public const string CLOSED = "Closed";
        public const string SUBMITTED = "Submitted";
        public const string PENDING_SUBMIT = "PendingSubmit";
    }
}
