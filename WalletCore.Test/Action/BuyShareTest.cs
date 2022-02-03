using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletCore.Action;
using WalletCore.Interface;
using WalletCore.Model.Action;
using WalletCore.Model.Database;
using WalletCore.Model.Response;
using Xunit;

namespace WalletCore.Test.Action
{
    public class BuyShareTest
    {
        [Fact]
        public async Task Should_Add_Share_In_Wallet_When_MoneyAvaliable_Equal_Or_Greater_Than_Buy_Operation()
        {
            #region Arrange

            var expectedResponse = new DontHaveError();
            var expectedWallet = new Wallet()
            {
                Owner = new Owner()
                {
                    AccountNumber = Consts.AccountNumber,
                    CPF = Consts.CPF,
                    Name = Consts.Name
                },
                MoneyAvailable = 460,
                MoneyInvested = 40,
                Shares = new List<Share>()
            };

            var expectedSharedInWallet = new Share()
            {
                PurchasePrice = 20,
                Quantity = 2,
                Symbol = "TORO4"
            };
            expectedWallet.Shares.Add(expectedSharedInWallet);

            var buyShare = new BuyShare()
            {
                PurchasePrice = 20,
                Quantity = 2,
                Symbol = "TORO4"
            };

            var walletDatabaseMock = new Mock<IWalletDatabase>();

            var currentWallet = new Wallet()
            {
                Owner = new Owner()
                {
                    AccountNumber = Consts.AccountNumber,
                    CPF = Consts.CPF,
                    Name = Consts.Name
                },
                MoneyAvailable = 500,
                Shares = new List<Share>()
            };

            walletDatabaseMock.Setup(x => x.FindByAccountNumberAsync(Consts.AccountNumber)).ReturnsAsync(currentWallet);

            Wallet updatedWallet = null;
            walletDatabaseMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>()))
                .Returns((Wallet walletToUpdate) =>
                {
                    updatedWallet = walletToUpdate;

                    return Task.CompletedTask;
                });

            var action = new BuyShareAction(walletDatabaseMock.Object);

            #endregion Arrange

            #region Act

            var response = await action.ExecuteAsync(buyShare, Consts.AccountNumber);

            #endregion Act

            #region Assert

            walletDatabaseMock.Verify(x => x.FindByAccountNumberAsync(It.IsAny<string>()), Times.Once);
            walletDatabaseMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>()), Times.Once);

            Assert.NotNull(response);

            Assert.False(response.HasError);
            Assert.Equal(expectedResponse.ErrorCode, response.ErrorCode);
            Assert.Equal(expectedResponse.Message, response.Message);

            Assert.NotEmpty(updatedWallet.Shares);

            Assert.Equal(expectedWallet.MoneyAvailable, updatedWallet.MoneyAvailable);
            Assert.Equal(expectedWallet.MoneyInvested, updatedWallet.MoneyInvested);

            var newShare = updatedWallet.Shares.First();

            Assert.Equal(expectedSharedInWallet.PurchasePrice, newShare.PurchasePrice);
            Assert.Equal(expectedSharedInWallet.Quantity, newShare.Quantity);
            Assert.Equal(expectedSharedInWallet.Symbol, newShare.Symbol);

            #endregion Assert
        }

        [Fact]
        public async Task Should_Return_Insufficient_Funds_When_MoneyAvaliable_Less_Than_Buy_Operation()
        {
            #region Arrange

            var expectedResponse = new ErrorResponse(ErrorCode.InsufficientFunds);

            var buyShare = new BuyShare()
            {
                PurchasePrice = 20,
                Quantity = 2,
                Symbol = "TORO4"
            };

            var walletDatabaseMock = new Mock<IWalletDatabase>();

            var wallet = new Wallet()
            {
                Owner = new Owner()
                {
                    AccountNumber = Consts.AccountNumber,
                    CPF = Consts.CPF,
                    Name = Consts.Name
                },
                MoneyAvailable = 5
            };

            walletDatabaseMock.Setup(x => x.FindByAccountNumberAsync(Consts.AccountNumber)).ReturnsAsync(wallet);

            var action = new BuyShareAction(walletDatabaseMock.Object);

            #endregion Arrange

            #region Act

            var response = await action.ExecuteAsync(buyShare, Consts.AccountNumber);

            #endregion Act

            #region Assert

            walletDatabaseMock.Verify(x => x.FindByAccountNumberAsync(It.IsAny<string>()), Times.Once);
            walletDatabaseMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>()), Times.Never);

            Assert.NotNull(response);

            Assert.True(response.HasError);
            Assert.Equal(expectedResponse.ErrorCode, response.ErrorCode);
            Assert.Equal(expectedResponse.Message, response.Message);

            #endregion Assert
        }

        [Fact]
        public async Task Should_Return_Wallet_Not_Found_When_Not_Found_Any_Wallet_By_AccountNumberAsync()
        {
            #region Arrange

            var expectedResponse = new ErrorResponse(ErrorCode.WalletNotFound);

            var buyShare = new BuyShare()
            {
                PurchasePrice = 20,
                Quantity = 2,
                Symbol = "TORO4"
            };

            var walletDatabaseMock = new Mock<IWalletDatabase>();

            var action = new BuyShareAction(walletDatabaseMock.Object);

            #endregion Arrange

            #region Act

            var response = await action.ExecuteAsync(buyShare, Consts.AccountNumber);

            #endregion Act

            #region Assert

            walletDatabaseMock.Verify(x => x.FindByAccountNumberAsync(It.IsAny<string>()), Times.Once);
            walletDatabaseMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>()), Times.Never);

            Assert.NotNull(response);

            Assert.True(response.HasError);
            Assert.Equal(expectedResponse.ErrorCode, response.ErrorCode);
            Assert.Equal(expectedResponse.Message, response.Message);

            #endregion Assert
        }
        [Fact]
        public async Task Should_Update_Share_In_Wallet_When_Already_Exists()
        {
            #region Arrange

            var expectedResponse = new DontHaveError();
            var expectedWallet = new Wallet()
            {
                Owner = new Owner()
                {
                    AccountNumber = Consts.AccountNumber,
                    CPF = Consts.CPF,
                    Name = Consts.Name
                },
                MoneyAvailable = 380,
                MoneyInvested = 120,
                Shares = new List<Share>()
            };

            var expectedSharedInWallet = new Share()
            {
                PurchasePrice = 30,
                Quantity = 4,
                Symbol = "TORO4"
            };
            expectedWallet.Shares.Add(expectedSharedInWallet);

            var currentWallet = new Wallet()
            {
                Owner = new Owner()
                {
                    AccountNumber = Consts.AccountNumber,
                    CPF = Consts.CPF,
                    Name = Consts.Name
                },
                MoneyAvailable = 460,
                MoneyInvested = 40,
                Shares = new List<Share>()
            };

            var currentSharedInWallet = new Share()
            {
                PurchasePrice = 20,
                Quantity = 2,
                Symbol = "TORO4"
            };
            currentWallet.Shares.Add(currentSharedInWallet);

            var walletDatabaseMock = new Mock<IWalletDatabase>();
            walletDatabaseMock.Setup(x => x.FindByAccountNumberAsync(Consts.AccountNumber)).ReturnsAsync(currentWallet);

            Wallet updatedWallet = null;
            walletDatabaseMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>()))
                .Returns((Wallet walletToUpdate) =>
                {
                    updatedWallet = walletToUpdate;

                    return Task.CompletedTask;
                });

            var action = new BuyShareAction(walletDatabaseMock.Object);

            var buyShare = new BuyShare()
            {
                PurchasePrice = 40,
                Quantity = 2,
                Symbol = "TORO4"
            };

            #endregion Arrange

            #region Act

            var response = await action.ExecuteAsync(buyShare, Consts.AccountNumber);

            #endregion Act

            #region Assert

            walletDatabaseMock.Verify(x => x.FindByAccountNumberAsync(It.IsAny<string>()), Times.Once);
            walletDatabaseMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>()), Times.Once);

            Assert.NotNull(response);

            Assert.False(response.HasError);
            Assert.Equal(expectedResponse.ErrorCode, response.ErrorCode);
            Assert.Equal(expectedResponse.Message, response.Message);

            Assert.NotEmpty(updatedWallet.Shares);

            Assert.Equal(expectedWallet.MoneyAvailable, updatedWallet.MoneyAvailable);
            Assert.Equal(expectedWallet.MoneyInvested, updatedWallet.MoneyInvested);

            var newShare = updatedWallet.Shares.First();

            Assert.Equal(expectedSharedInWallet.PurchasePrice, newShare.PurchasePrice);
            Assert.Equal(expectedSharedInWallet.Quantity, newShare.Quantity);
            Assert.Equal(expectedSharedInWallet.Symbol, newShare.Symbol);

            #endregion Assert
        }
    }
}