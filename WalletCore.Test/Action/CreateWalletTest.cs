using Moq;
using System.Threading.Tasks;
using WalletCore.Action;
using WalletCore.Interface;
using WalletCore.Model.Action;
using WalletCore.Model.Database;
using Xunit;

namespace WalletCore.Test.Action
{
    public class CreateWalletTest
    {
        [Fact]
        public async Task Should_Create_New_WalletAsync()
        {
            #region Arrange

            var wallet = new CreateWallet()
            {
                AccountNumber = "1",
                CPF = "00934402868",
                Name = "Elza Flávia Moraes"
            };

            var walletDatabaseMock = new Mock<IWalletDatabase>();

            var action = new CreateWalletAction(walletDatabaseMock.Object);

            #endregion Arrange

            #region Act

            await action.ExecuteAsync(wallet);

            #endregion Act

            #region Assert

            walletDatabaseMock.Verify(x => x.InsertAsync(It.IsAny<Wallet>()), Times.Once);

            #endregion Assert
        }
    }
}