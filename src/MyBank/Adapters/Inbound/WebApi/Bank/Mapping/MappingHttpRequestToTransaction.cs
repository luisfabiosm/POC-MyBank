
using Domain.Core.Entity;
using Domain.Core.Models.Request;
using Domain.UseCases.Accounts.GetBalance;
using Domain.UseCases.Accounts.GetStatement;
using Domain.UseCases.PIX.GetPixKey;
using Domain.UseCases.PIX.InitiatePixPayment;
using Domain.UseCases.Security.AuthTransaction;
using Domain.UseCases.Security.LoginAccount;
using System.Linq.Expressions;

namespace Adapters.Inbound.WebApi.Bank.Mapping
{
    public class MappingHttpRequestToTransaction
    {
        public TransactionGetBalance ToTransactionGetBalance(AccountRequest request)
        {
            try
            {
                return new TransactionGetBalance(request.BankNumber, request.AgencyNumber, request.AccountNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TransactionGetStatement ToTransactionGetStatement(AccountRequest request)
        {
            try
            {
                return new TransactionGetStatement(request.BankNumber, request.AgencyNumber, request.AccountNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TransactionGetPixKey ToTransactionGetPixKey(PixGetKeyRequest request)
        {
            try
            {
                return new TransactionGetPixKey(request.Key, request.Type);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TransactionInitiatePixPayment ToTransactionInitiatePixPayment(PixPayRequest request)
        {
            try
            {
                return new TransactionInitiatePixPayment(new ExternalAccount(request.Bank, request.AgencyNumber, request.AccountNumber, request.Cpf),request.Amount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TransactionAuthTransaction ToTransactionAuthTransaction(AuthTransactionRequest request)
        {
            try
            {
                return new TransactionAuthTransaction(request.AgencyNumber, request.AccountNumber, request.CardPassword, request.OriginaLTransactionId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        
        public TransactionLoginAccount ToTransactionLoginAccount(LoginRequest request)
        {
            try
            {
                return new TransactionLoginAccount(request.Cpf, request.Password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

      