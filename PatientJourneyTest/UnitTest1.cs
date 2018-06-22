﻿using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using System.Threading;

namespace PatientJourneyTest
{
    [TestClass]
    public class PatientJourneyTests
    {
        [TestMethod]    
        public async Task ShouldWork()
        {
            var ethereumClientIntegrationFixture = new EthereumClientIntegrationFixture();

            var account = AccountFactory.GetManagedAccount();

            var web3 = Web3Factory.GetWeb3Managed();

            var unlockAccountResult = await web3.Personal.UnlockAccount.SendRequestAsync(account.Address, account.Password, 0);
            Assert.IsTrue(unlockAccountResult);

            // Valid ABI and byteCode
            //var abi = @"[{""constant"": false,""inputs"": [{""name"": ""uri"",""type"": ""string""},{""name"": ""hash"",""type"": ""string""}],""name"": ""addDatabaseLink"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""permissionTimestamp"",""type"": ""uint256""}],""name"": ""removePermission"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""from"",""type"": ""uint256""},{""name"": ""to"",""type"": ""uint256""},{""name"": ""personPermission"",""type"": ""address""}],""name"": ""addPermission"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""invalidateAddress"",""type"": ""address""}],""name"": ""invalidatePerson"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""timestamp"",""type"": ""uint256""}],""name"": ""invalidateDatabaseLink"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""validateAddress"",""type"": ""address""}],""name"": ""validatePerson"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""inputs"": [{""name"": ""patient"",""type"": ""address""}],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""constructor""},{""anonymous"": false,""inputs"": [{""indexed"": false,""name"": ""uri"",""type"": ""string""},{""indexed"": false,""name"": ""time"",""type"": ""uint256""}],""name"": ""LogDatabaseLink"",""type"": ""event""}]";
            //var byteCode = @"0x608060405234801561001057600080fd5b50604051602080610b888339810180604052810190808051906020019092919050505080600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055506001600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff02191690831515021790555050610a698061011f6000396000f300608060405260043610610078576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680630d65dc931461007d578063200f65391461012c5780635ad7a8f7146101595780635c62feba146101b0578063933131f5146101f3578063cdd0b26414610220575b600080fd5b34801561008957600080fd5b5061012a600480360381019080803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290505050610263565b005b34801561013857600080fd5b506101576004803603810190808035906020019092919050505061046e565b005b34801561016557600080fd5b506101ae6004803603810190808035906020019092919080359060200190929190803573ffffffffffffffffffffffffffffffffffffffff1690602001909291905050506104bc565b005b3480156101bc57600080fd5b506101f1600480360381019080803573ffffffffffffffffffffffffffffffffffffffff1690602001909291905050506105dd565b005b3480156101ff57600080fd5b5061021e600480360381019080803590602001909291905050506107a1565b005b34801561022c57600080fd5b50610261600480360381019080803573ffffffffffffffffffffffffffffffffffffffff1690602001909291905050506107d3565b005b60004290506001600082815260200190815260200160002060020160009054906101000a900460ff1615801561033b57506000803373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff168061033a5750600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff165b5b15610469578260016000838152602001908152602001600020600001908051906020019061036a929190610998565b5081600160008381526020019081526020016000206001019080519060200190610395929190610998565b50600180600083815260200190815260200160002060020160006101000a81548160ff0219169083151502179055507fcfca8d21a1853b213dfc243126ee5355d599bf9f8566ce62ad79aecbeed2f35b83826040518080602001838152602001828103825284818151815260200191508051906020019080838360005b8381101561042d578082015181840152602081019050610412565b50505050905090810190601f16801561045a5780820380516001836020036101000a031916815260200191505b50935050505060405180910390a15b505050565b6002600082815260200190815260200160002060030160009054906101000a900460ff16151561049d576104b9565b4260026000838152602001908152602001600020600201819055505b50565b600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161515610517576105d8565b600360008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161515610572576105d8565b82600260004281526020019081526020016000206001018190555081600260004281526020019081526020016000206002018190555060016002600042815260200190815260200160002060030160006101000a81548160ff0219169083151502179055505b505050565b600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156106385761079e565b600360008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff16156106ed576000600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff02191690831515021790555061079d565b6000808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161561079c5760008060008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff0219169083151502179055505b5b5b50565b60006001600083815260200190815260200160002060020160006101000a81548160ff02191690831515021790555050565b600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff16151561082e57610995565b600360008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156108e4576001600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff021916908315150217905550610994565b6000808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff16156109935760016000808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff0219169083151502179055505b5b5b50565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106109d957805160ff1916838001178555610a07565b82800160010185558215610a07579182015b82811115610a065782518255916020019190600101906109eb565b5b509050610a149190610a18565b5090565b610a3a91905b80821115610a36576000816000905550600101610a1e565b5090565b905600a165627a7a72305820f38ddd8ba62581bc770b20eaa3597749bbd3279ded54408cf52e52a65863fef30029";

            // Test abi and bytecode
            var abi = @"[{""constant"": false,""inputs"": [{""name"": ""uri"",""type"": ""string""},{""name"": ""hash"",""type"": ""string""}],""name"": ""addDatabaseLink"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""value"",""type"": ""int256""}],""name"": ""multiply"",""outputs"": [{""name"": ""returnee"",""type"": ""int256""}],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""permissionTimestamp"",""type"": ""uint256""}],""name"": ""removePermission"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""from"",""type"": ""uint256""},{""name"": ""to"",""type"": ""uint256""},{""name"": ""personPermission"",""type"": ""address""}],""name"": ""addPermission"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""invalidateAddress"",""type"": ""address""}],""name"": ""invalidatePerson"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""timestamp"",""type"": ""uint256""}],""name"": ""invalidateDatabaseLink"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""multiplier"",""type"": ""int256""}],""name"": ""test"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""validateAddress"",""type"": ""address""}],""name"": ""validatePerson"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""inputs"": [{""name"": ""patient"",""type"": ""address""}],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""constructor""},{""anonymous"": false,""inputs"": [{""indexed"": false,""name"": ""uri"",""type"": ""string""},{""indexed"": false,""name"": ""time"",""type"": ""uint256""}],""name"": ""LogDatabaseLink"",""type"": ""event""}]";
            var byteCode = @"0x608060405234801561001057600080fd5b50604051602080610c248339810180604052810190808051906020019092919050505080600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055506001600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff02191690831515021790555050610b058061011f6000396000f30060806040526004361061008e576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680630d65dc93146100935780631df4f14414610142578063200f6539146101835780635ad7a8f7146101b05780635c62feba14610207578063933131f51461024a5780639b22c05d14610277578063cdd0b264146102a4575b600080fd5b34801561009f57600080fd5b50610140600480360381019080803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290803590602001908201803590602001908080601f01602080910402602001604051908101604052809392919081815260200183838082843782019150505050505091929192905050506102e7565b005b34801561014e57600080fd5b5061016d600480360381019080803590602001909291905050506104f2565b6040518082815260200191505060405180910390f35b34801561018f57600080fd5b506101ae60048036038101908080359060200190929190505050610500565b005b3480156101bc57600080fd5b506102056004803603810190808035906020019092919080359060200190929190803573ffffffffffffffffffffffffffffffffffffffff16906020019092919050505061054e565b005b34801561021357600080fd5b50610248600480360381019080803573ffffffffffffffffffffffffffffffffffffffff16906020019092919050505061066f565b005b34801561025657600080fd5b5061027560048036038101908080359060200190929190505050610833565b005b34801561028357600080fd5b506102a260048036038101908080359060200190929190505050610865565b005b3480156102b057600080fd5b506102e5600480360381019080803573ffffffffffffffffffffffffffffffffffffffff16906020019092919050505061086f565b005b60004290506001600082815260200190815260200160002060020160009054906101000a900460ff161580156103bf57506000803373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff16806103be5750600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff165b5b156104ed57826001600083815260200190815260200160002060000190805190602001906103ee929190610a34565b5081600160008381526020019081526020016000206001019080519060200190610419929190610a34565b50600180600083815260200190815260200160002060020160006101000a81548160ff0219169083151502179055507fcfca8d21a1853b213dfc243126ee5355d599bf9f8566ce62ad79aecbeed2f35b83826040518080602001838152602001828103825284818151815260200191508051906020019080838360005b838110156104b1578082015181840152602081019050610496565b50505050905090810190601f1680156104de5780820380516001836020036101000a031916815260200191505b50935050505060405180910390a15b505050565b600060045482029050919050565b6002600082815260200190815260200160002060030160009054906101000a900460ff16151561052f5761054b565b4260026000838152602001908152602001600020600201819055505b50565b600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156105a95761066a565b600360008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156106045761066a565b82600260004281526020019081526020016000206001018190555081600260004281526020019081526020016000206002018190555060016002600042815260200190815260200160002060030160006101000a81548160ff0219169083151502179055505b505050565b600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156106ca57610830565b600360008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161561077f576000600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff02191690831515021790555061082f565b6000808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161561082e5760008060008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff0219169083151502179055505b5b5b50565b60006001600083815260200190815260200160002060020160006101000a81548160ff02191690831515021790555050565b8060048190555050565b600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156108ca57610a31565b600360008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161515610980576001600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff021916908315150217905550610a30565b6000808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615610a2f5760016000808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff0219169083151502179055505b5b5b50565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f10610a7557805160ff1916838001178555610aa3565b82800160010185558215610aa3579182015b82811115610aa2578251825591602001919060010190610a87565b5b509050610ab09190610ab4565b5090565b610ad691905b80821115610ad2576000816000905550600101610aba565b5090565b905600a165627a7a72305820c96987d8e6b91648e232f819247589b813b63e8e9e435777c0aea9a5a888cb2c0029";

            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, account.Address, new HexBigInteger(999999));
           
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            while (receipt == null)
            {
                Thread.Sleep(5000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }

            var contractAddress = receipt.ContractAddress;

            var contract = web3.Eth.GetContract(abi, contractAddress);

            Assert.IsNotNull(contract);

        }

        [TestMethod]
        public async Task ShouldWorkWithRamonsSnippet()
        {   
            var service = new PatientJourneyServices();
            var receipt = await service.CreateNewPatientJourneyRequestContract("0x1ac5075Ed791a1e7ea7306088d34C8041B573225");
            Assert.IsNotNull(receipt);

            var web3 = service.GetWeb3Instance("0x1ac5075Ed791a1e7ea7306088d34C8041B573225");
            var contractAddress = receipt.ContractAddress;

            var contract = service.GetContract(web3, contractAddress);
            var multiplyFunction = contract.GetFunction("multiply");
            var testFunction = contract.GetFunction("test");
            var getMultiplierFunction = contract.GetFunction("getMultiplier");

            var result = 0;
            result = await multiplyFunction.CallAsync<int>(5);
            Assert.AreEqual(25, result);
            
            var receipt2 = await testFunction.SendTransactionAndWaitForReceiptAsync(from:"0x1ac5075Ed791a1e7ea7306088d34C8041B573225", gas: new HexBigInteger(4712388), value: null,  functionInput: 10);
            
            Thread.Sleep(2000);

            result = await multiplyFunction.CallAsync<int>(5);
            Assert.AreEqual(50, result);

            result = await getMultiplierFunction.CallAsync<int>();
            Assert.AreEqual(10, result);
        }
    }
}
