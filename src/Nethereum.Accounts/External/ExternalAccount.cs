﻿using System.Numerics;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Accounts;
using Nethereum.RPC.NonceServices;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Signer;

namespace Nethereum.Web3.Accounts
{
    public class ExternalAccount : IAccount
    {
        public IEthECKeyExternalSigner ExternalSigner { get; }
        public BigInteger? ChainId { get; }


        public ExternalAccount(IEthECKeyExternalSigner externalSigner, BigInteger? chainId = null)
        {
            ExternalSigner = externalSigner;
            ChainId = chainId;
        }

        public ExternalAccount(string address, IEthECKeyExternalSigner externalSigner, BigInteger? chainId = null)
        {
            ChainId = chainId;
            Address = address;
            ExternalSigner = externalSigner;
        }

        public async Task InitialiseAsync()
        {
            Address = await ExternalSigner.GetAddressAsync();
        }

        public void InitialiseDefaultTransactionManager(IClient client)
        {
            TransactionManager = new ExternalAccountSignerTransactionManager(client, this, ChainId);
        }

        public string Address { get; protected set; }
        public ITransactionManager TransactionManager { get; protected set; }
        public INonceService NonceService { get; set; }
    }
}