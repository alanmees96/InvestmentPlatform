namespace WalletCore.Model.Response
{
    public class ActionResponse
    {
        public string Message { get; protected set; }

        public int? ErrorCode { get; protected set; }

        public bool HasError { get => ErrorCode != null; }
    }
}