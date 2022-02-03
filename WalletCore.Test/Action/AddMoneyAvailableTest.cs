using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletCore.Action;
using WalletCore.Interface;
using WalletCore.Model.Action.AddMoneyAvailable;
using WalletCore.Model.Database;
using WalletCore.Model.Response;
using Xunit;

namespace WalletCore.Test.Action
{
    public class AddMoneyAvailableTest
    {
        [Fact]
        public async Task Should_Return_Error_Wallet_Not_Found_When_Not_Found_Wallet_By_Account()
        {
            #region Arrange

            var expectedResponse = new ErrorResponse(ErrorCode.WalletNotFound);

            var transferMoney = new WalletTransferMoneyInfo()
            {
                Event = "TRANSFER",
                Origin = new OriginInfo()
                {
                    Bank = "352",
                    Branch = "0001",
                    CPF = Consts.CPF
                },
                Target = new TargetInfo()
                {
                    Bank = "352",
                    Branch = "0001",
                    Account = Consts.AccountNumber
                },
                Amount = 1000
            };

            var walletDatabaseMock = new Mock<IWalletDatabase>();
            walletDatabaseMock.Setup(x => x.FindByAccountNumberAsync(Consts.AccountNumber)).ReturnsAsync((Wallet) null);

            var action = new AddMoneyAvailableAction(walletDatabaseMock.Object);

            #endregion Arrange

            #region Act

            var response = await action.ExecuteAsync(transferMoney);

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
        public async Task Should_Return_Error_Not_Match_CPF_When_CPF_Origin_Different_Target()
        {
            #region Arrange

            var expectedResponse = new ErrorResponse(ErrorCode.TransferCPFDoesntMatch);

            var transferMoney = new WalletTransferMoneyInfo()
            {
                Event = "TRANSFER",
                Origin = new OriginInfo()
                {
                    Bank = "352",
                    Branch = "0001",
                    CPF = "45358996060"
                },
                Target = new TargetInfo()
                {
                    Bank = "352",
                    Branch = "0001",
                    Account = Consts.AccountNumber
                },
                Amount = 1000
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

            var action = new AddMoneyAvailableAction(walletDatabaseMock.Object);

            #endregion Arrange

            #region Act

            var response = await action.ExecuteAsync(transferMoney);

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
        public async Task Should_Add_Money_When_Receive_Valid_Payload()
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
                MoneyAvailable = 1500,
                Shares = new List<Share>()
            };

            var transferMoney = new WalletTransferMoneyInfo()
            {
                Event = "TRANSFER",
                Origin = new OriginInfo()
                {
                    Bank = "352",
                    Branch = "0001",
                    CPF = Consts.CPF
                },
                Target = new TargetInfo()
                {
                    Bank = "352",
                    Branch = "0001",
                    Account = Consts.AccountNumber
                },
                Amount = 1000
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

            var action = new AddMoneyAvailableAction(walletDatabaseMock.Object);

            #endregion Arrange

            #region Act

            var response = await action.ExecuteAsync(transferMoney);

            #endregion Act

            #region Assert

            walletDatabaseMock.Verify(x => x.FindByAccountNumberAsync(It.IsAny<string>()), Times.Once);
            walletDatabaseMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>()), Times.Once);

            Assert.NotNull(response);

            Assert.False(response.HasError);
            Assert.Equal(expectedResponse.ErrorCode, response.ErrorCode);
            Assert.Equal(expectedResponse.Message, response.Message);

            Assert.Empty(updatedWallet.Shares);

            Assert.Equal(expectedWallet.MoneyAvailable, updatedWallet.MoneyAvailable);
            Assert.Equal(expectedWallet.MoneyInvested, updatedWallet.MoneyInvested);

            #endregion Assert
        }
    }
}
