﻿using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts.Managed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatientJourneyTest
{
    class PatientJourneyServices
    {
        //        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _abi;
        private readonly string _byteCode;
        private Contract _contract;

        public PatientJourneyServices()
        {
            // Test abi and bytecode extracted from the solidity contract found at https://gist.github.com/GLaDTheresCake/7d31884e5de80521cd5faa04699b1fd2#file-patientjourney-sol-L141
            _abi = @"[{""constant"": true,""inputs"": [{""name"": ""key"",""type"": ""uint256""}],""name"": ""getPermissionPersonAtAddress"",""outputs"": [{""name"": ""permissionPerson"",""type"": ""address""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": true,""inputs"": [{""name"": ""key"",""type"": ""uint256""}],""name"": ""getDatabaseLinkKeyAtAddress"",""outputs"": [{""name"": ""databaseKey"",""type"": ""string""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": true,""inputs"": [{""name"": ""key"",""type"": ""uint256""}],""name"": ""getDatabaseLinkAtAddress"",""outputs"": [{""name"": ""link"",""type"": ""string""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": true,""inputs"": [],""name"": ""getPermissionEnumerator"",""outputs"": [{""name"": ""permissionEnumerator"",""type"": ""uint256[]""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": false,""inputs"": [],""name"": ""getSecondMultiplier"",""outputs"": [{""name"": ""multiplier"",""type"": ""int256""}],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""value"",""type"": ""int256""}],""name"": ""multiply"",""outputs"": [{""name"": ""returnee"",""type"": ""int256""}],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""permissionTimestamp"",""type"": ""uint256""}],""name"": ""removePermission"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""multiplier"",""type"": ""int256""},{""name"": ""multiplier2"",""type"": ""int256""}],""name"": ""test"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": true,""inputs"": [{""name"": ""enumerator"",""type"": ""uint256""}],""name"": ""getDatabaseLinkKeyFromEnumerator"",""outputs"": [{""name"": ""databaseKeyEnum"",""type"": ""uint256""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": false,""inputs"": [],""name"": ""getMultiplier"",""outputs"": [{""name"": ""multiplier"",""type"": ""int256""}],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""value"",""type"": ""int256""}],""name"": ""multiply2"",""outputs"": [{""name"": ""returnee"",""type"": ""int256""}],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": true,""inputs"": [{""name"": """",""type"": ""uint256""}],""name"": ""permissionList"",""outputs"": [{""name"": """",""type"": ""uint256""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": true,""inputs"": [],""name"": ""getDatabaseLinkEnumerator"",""outputs"": [{""name"": ""databaseEnumerator"",""type"": ""uint256[]""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""from"",""type"": ""uint256""},{""name"": ""to"",""type"": ""uint256""},{""name"": ""personPermission"",""type"": ""address""}],""name"": ""addPermission"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""invalidateAddress"",""type"": ""address""}],""name"": ""invalidatePerson"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": true,""inputs"": [{""name"": ""enumerator"",""type"": ""uint256""}],""name"": ""getPermissionKeyFromEnumerator"",""outputs"": [{""name"": ""permissionKeyEnum"",""type"": ""uint256""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": true,""inputs"": [{""name"": """",""type"": ""uint256""}],""name"": ""databaseLinkList"",""outputs"": [{""name"": """",""type"": ""uint256""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": true,""inputs"": [],""name"": ""getPermissionCount"",""outputs"": [{""name"": ""count"",""type"": ""uint256""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": true,""inputs"": [],""name"": ""getDatabaseLinkCount"",""outputs"": [{""name"": ""count"",""type"": ""uint256""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""timestamp"",""type"": ""uint256""}],""name"": ""invalidateDatabaseLink"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": true,""inputs"": [{""name"": ""key"",""type"": ""uint256""}],""name"": ""getPermissionToAtAddress"",""outputs"": [{""name"": ""permissionTo"",""type"": ""uint256""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": true,""inputs"": [{""name"": ""key"",""type"": ""uint256""}],""name"": ""getDatabaseLinkHashAtAddress"",""outputs"": [{""name"": ""hash"",""type"": ""string""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": true,""inputs"": [{""name"": ""key"",""type"": ""uint256""}],""name"": ""getPermissionFromAtAddress"",""outputs"": [{""name"": ""permissionFrom"",""type"": ""uint256""}],""payable"": false,""stateMutability"": ""view"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""validateAddress"",""type"": ""address""}],""name"": ""validatePerson"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""uri"",""type"": ""string""},{""name"": ""hash"",""type"": ""string""},{""name"": ""key"",""type"": ""string""}],""name"": ""addDatabaseLink"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""inputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""constructor""},{""anonymous"": false,""inputs"": [{""indexed"": false,""name"": ""uri"",""type"": ""string""},{""indexed"": false,""name"": ""time"",""type"": ""uint256""}],""name"": ""LogDatabaseLink"",""type"": ""event""}]"; 
            _byteCode = @"0x6080604052336000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055506005600755600a60085534801561005a57600080fd5b506000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff16600660008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055506001600660008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff021916908315150217905550611662806101a96000396000f300608060405260043610610149576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806305234dc31461014e57806307d2a059146101bb5780630f66406b146102615780631420b0df146103075780631b05943c146103735780631df4f1441461039e578063200f6539146103df57806324d45ec31461040c57806339515e321461044357806340490a90146104845780634f4370a8146104af5780635051865d146104f057806358c1417f146105315780635ad7a8f71461059d5780635c62feba146105f45780635e390ddf1461063757806367238a13146106785780636c26ee0c146106b95780637081d4c9146106e4578063933131f51461070f57806397abeb581461073c578063a11b07391461077d578063b458c75914610823578063cdd0b26414610864578063e7d60d02146108a7575b600080fd5b34801561015a57600080fd5b506101796004803603810190808035906020019092919050505061099c565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b3480156101c757600080fd5b506101e6600480360381019080803590602001909291905050506109df565b6040518080602001828103825283818151815260200191508051906020019080838360005b8381101561022657808201518184015260208101905061020b565b50505050905090810190601f1680156102535780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34801561026d57600080fd5b5061028c60048036038101908080359060200190929190505050610a9a565b6040518080602001828103825283818151815260200191508051906020019080838360005b838110156102cc5780820151818401526020810190506102b1565b50505050905090810190601f1680156102f95780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34801561031357600080fd5b5061031c610b55565b6040518080602001828103825283818151815260200191508051906020019060200280838360005b8381101561035f578082015181840152602081019050610344565b505050509050019250505060405180910390f35b34801561037f57600080fd5b50610388610bb0565b6040518082815260200191505060405180910390f35b3480156103aa57600080fd5b506103c960048036038101908080359060200190929190505050610bba565b6040518082815260200191505060405180910390f35b3480156103eb57600080fd5b5061040a60048036038101908080359060200190929190505050610bc8565b005b34801561041857600080fd5b506104416004803603810190808035906020019092919080359060200190929190505050610c16565b005b34801561044f57600080fd5b5061046e60048036038101908080359060200190929190505050610c28565b6040518082815260200191505060405180910390f35b34801561049057600080fd5b50610499610c4e565b6040518082815260200191505060405180910390f35b3480156104bb57600080fd5b506104da60048036038101908080359060200190929190505050610c58565b6040518082815260200191505060405180910390f35b3480156104fc57600080fd5b5061051b60048036038101908080359060200190929190505050610c66565b6040518082815260200191505060405180910390f35b34801561053d57600080fd5b50610546610c89565b6040518080602001828103825283818151815260200191508051906020019060200280838360005b8381101561058957808201518184015260208101905061056e565b505050509050019250505060405180910390f35b3480156105a957600080fd5b506105f26004803603810190808035906020019092919080359060200190929190803573ffffffffffffffffffffffffffffffffffffffff169060200190929190505050610ce4565b005b34801561060057600080fd5b50610635600480360381019080803573ffffffffffffffffffffffffffffffffffffffff169060200190929190505050610e31565b005b34801561064357600080fd5b5061066260048036038101908080359060200190929190505050610ff7565b6040518082815260200191505060405180910390f35b34801561068457600080fd5b506106a36004803603810190808035906020019092919050505061101d565b6040518082815260200191505060405180910390f35b3480156106c557600080fd5b506106ce611040565b6040518082815260200191505060405180910390f35b3480156106f057600080fd5b506106f9611050565b6040518082815260200191505060405180910390f35b34801561071b57600080fd5b5061073a60048036038101908080359060200190929190505050611060565b005b34801561074857600080fd5b5061076760048036038101908080359060200190929190505050611092565b6040518082815260200191505060405180910390f35b34801561078957600080fd5b506107a8600480360381019080803590602001909291905050506110b5565b6040518080602001828103825283818151815260200191508051906020019080838360005b838110156107e85780820151818401526020810190506107cd565b50505050905090810190601f1680156108155780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34801561082f57600080fd5b5061084e60048036038101908080359060200190929190505050611170565b6040518082815260200191505060405180910390f35b34801561087057600080fd5b506108a5600480360381019080803573ffffffffffffffffffffffffffffffffffffffff169060200190929190505050611193565b005b3480156108b357600080fd5b5061099a600480360381019080803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290505050611359565b005b60006004600083815260200190815260200160002060000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff169050809050919050565b6060600260008381526020019081526020016000206003018054600181600116156101000203166002900480601f016020809104026020016040519081016040528092919081815260200182805460018160011615610100020316600290048015610a8b5780601f10610a6057610100808354040283529160200191610a8b565b820191906000526020600020905b815481529060010190602001808311610a6e57829003601f168201915b50505050509050809050919050565b6060600260008381526020019081526020016000206000018054600181600116156101000203166002900480601f016020809104026020016040519081016040528092919081815260200182805460018160011615610100020316600290048015610b465780601f10610b1b57610100808354040283529160200191610b46565b820191906000526020600020905b815481529060010190602001808311610b2957829003601f168201915b50505050509050809050919050565b60606005805480602002602001604051908101604052809291908181526020018280548015610ba357602002820191906000526020600020905b815481526020019060010190808311610b8f575b5050505050905080905090565b6000600854905090565b600060075482029050919050565b6004600082815260200190815260200160002060030160009054906101000a900460ff161515610bf757610c13565b4260046000838152602001908152602001600020600201819055505b50565b81600781905550806008819055505050565b6000600382815481101515610c3957fe5b90600052602060002001549050809050919050565b6000600754905090565b600060085482029050919050565b600581815481101515610c7557fe5b906000526020600020016000915090505481565b60606003805480602002602001604051908101604052809291908181526020018280548015610cd757602002820191906000526020600020905b815481526020019060010190808311610cc3575b5050505050905080905090565b600660003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161515610d3f57610e2c565b600660008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161515610d9a57610e2c565b82600460004281526020019081526020016000206001018190555081600460004281526020019081526020016000206002018190555060016004600042815260200190815260200160002060030160006101000a81548160ff02191690831515021790555060054290806001815401808255809150509060018203906000526020600020016000909192909190915055505b505050565b600660003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161515610e8c57610ff4565b600660008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615610f41576000600660008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff021916908315150217905550610ff3565b600160008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615610ff2576000600160008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff0219169083151502179055505b5b5b50565b600060058281548110151561100857fe5b90600052602060002001549050809050919050565b60038181548110151561102c57fe5b906000526020600020016000915090505481565b6000600580549050905080905090565b6000600380549050905080905090565b60006002600083815260200190815260200160002060020160006101000a81548160ff02191690831515021790555050565b600060046000838152602001908152602001600020600201549050809050919050565b6060600260008381526020019081526020016000206001018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156111615780601f1061113657610100808354040283529160200191611161565b820191906000526020600020905b81548152906001019060200180831161114457829003601f168201915b50505050509050809050919050565b600060046000838152602001908152602001600020600101549050809050919050565b600660003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156111ee57611356565b600660008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156112a4576001600660008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff021916908315150217905550611355565b600160008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff16156113545760018060008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff0219169083151502179055505b5b5b50565b6000429050600160003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff16806114055750600660003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff165b1561158b5783600260008381526020019081526020016000206000019080519060200190611434929190611591565b508260026000838152602001908152602001600020600101908051906020019061145f929190611591565b508160026000838152602001908152602001600020600301908051906020019061148a929190611591565b5060016002600083815260200190815260200160002060020160006101000a81548160ff02191690831515021790555060038190806001815401808255809150509060018203906000526020600020016000909192909190915055507fcfca8d21a1853b213dfc243126ee5355d599bf9f8566ce62ad79aecbeed2f35b84826040518080602001838152602001828103825284818151815260200191508051906020019080838360005b8381101561154f578082015181840152602081019050611534565b50505050905090810190601f16801561157c5780820380516001836020036101000a031916815260200191505b50935050505060405180910390a15b50505050565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106115d257805160ff1916838001178555611600565b82800160010185558215611600579182015b828111156115ff5782518255916020019190600101906115e4565b5b50905061160d9190611611565b5090565b61163391905b8082111561162f576000816000905550600101611617565b5090565b905600a165627a7a7230582012776daef54a576ce33d7c00d1ce583815b5940eb28df7fd48161e8714c8b2790029";
         }

        public Web3 GetWeb3Instance(ManagedAccount account)
        {
            // This constructor takes the IP of your blockchain as an argument. This is the IP from my local Ganache blockchain, it may be
            // so that your Ganache client has a different IP address. Please double-check that. 
            return new Web3(account, "HTTP://127.0.0.1:7545");
        }

        public async Task<TransactionReceipt> CreateNewPatientJourneyRequestContract(ManagedAccount account)
        {
            var web3 = GetWeb3Instance(account);
            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(_abi, _byteCode, account.Address, new HexBigInteger(64777777));
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            while (receipt == null)
            {
                Thread.Sleep(5000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }

            return receipt;
        }

        public int ConvertDateToUnixTimestamp(DateTime date)
        {
            int unixTimestamp = (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp;
        }

        public DateTime ConvertUnixTimestampToDate(int unixTimestamp)
        {
            DateTime date = new DateTime(1970, 1, 1).AddSeconds(unixTimestamp);
            return date;
        }


        public Contract GetContract(Web3 web3, string contractAddress)
        {
            _contract = web3.Eth.GetContract(_abi, contractAddress);
            return _contract;
        }

        public ManagedAccount AccountFactory()
        {
            // This is a hardcoded account from my local Ganache blockchain. It's the first account in the list. 
            // For the mock-up application you'd want to create your own accounts. Although it's not been simulated in this 
            // project, there's documentation available at https://nethereum.readthedocs.io/en/latest/accounts/#working-with-an-account
            return new ManagedAccount("0x1ac5075Ed791a1e7ea7306088d34C8041B573225", "password");
        }
    }
}
