namespace WalletCore.Model.Response
{
    public class DontHaveError : ActionResponse
    {
        public DontHaveError(string message)
        {
            Message = message;
        }

        public DontHaveError()
        {
        }
    }
}