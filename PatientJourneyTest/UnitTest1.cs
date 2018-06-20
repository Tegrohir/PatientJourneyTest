﻿using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;

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


            var abi = @"[{""constant"": false,""inputs"": [{""name"": ""uri"",""type"": ""string""},{""name"": ""hash"",""type"": ""string""}],""name"": ""addDatabaseLink"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""permissionTimestamp"",""type"": ""uint256""}],""name"": ""removePermission"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""from"",""type"": ""uint256""},{""name"": ""to"",""type"": ""uint256""},{""name"": ""personPermission"",""type"": ""address""}],""name"": ""addPermission"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""invalidateAddress"",""type"": ""address""}],""name"": ""invalidatePerson"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""timestamp"",""type"": ""uint256""}],""name"": ""invalidateDatabaseLink"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""constant"": false,""inputs"": [{""name"": ""validateAddress"",""type"": ""address""}],""name"": ""validatePerson"",""outputs"": [],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""function""},{""inputs"": [{""name"": ""patient"",""type"": ""address""}],""payable"": false,""stateMutability"": ""nonpayable"",""type"": ""constructor""},{""anonymous"": false,""inputs"": [{""indexed"": false,""name"": ""uri"",""type"": ""string""},{""indexed"": false,""name"": ""time"",""type"": ""uint256""}],""name"": ""LogDatabaseLink"",""type"": ""event""}]";
            var byteCode = @"{""linkReferences"": {},""object"": ""608060405234801561001057600080fd5b50604051602080610b888339810180604052810190808051906020019092919050505080600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055506001600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff02191690831515021790555050610a698061011f6000396000f300608060405260043610610078576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680630d65dc931461007d578063200f65391461012c5780635ad7a8f7146101595780635c62feba146101b0578063933131f5146101f3578063cdd0b26414610220575b600080fd5b34801561008957600080fd5b5061012a600480360381019080803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290505050610263565b005b34801561013857600080fd5b506101576004803603810190808035906020019092919050505061046e565b005b34801561016557600080fd5b506101ae6004803603810190808035906020019092919080359060200190929190803573ffffffffffffffffffffffffffffffffffffffff1690602001909291905050506104bc565b005b3480156101bc57600080fd5b506101f1600480360381019080803573ffffffffffffffffffffffffffffffffffffffff1690602001909291905050506105dd565b005b3480156101ff57600080fd5b5061021e600480360381019080803590602001909291905050506107a1565b005b34801561022c57600080fd5b50610261600480360381019080803573ffffffffffffffffffffffffffffffffffffffff1690602001909291905050506107d3565b005b60004290506001600082815260200190815260200160002060020160009054906101000a900460ff1615801561033b57506000803373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff168061033a5750600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff165b5b15610469578260016000838152602001908152602001600020600001908051906020019061036a929190610998565b5081600160008381526020019081526020016000206001019080519060200190610395929190610998565b50600180600083815260200190815260200160002060020160006101000a81548160ff0219169083151502179055507fcfca8d21a1853b213dfc243126ee5355d599bf9f8566ce62ad79aecbeed2f35b83826040518080602001838152602001828103825284818151815260200191508051906020019080838360005b8381101561042d578082015181840152602081019050610412565b50505050905090810190601f16801561045a5780820380516001836020036101000a031916815260200191505b50935050505060405180910390a15b505050565b6002600082815260200190815260200160002060030160009054906101000a900460ff16151561049d576104b9565b4260026000838152602001908152602001600020600201819055505b50565b600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161515610517576105d8565b600360008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161515610572576105d8565b82600260004281526020019081526020016000206001018190555081600260004281526020019081526020016000206002018190555060016002600042815260200190815260200160002060030160006101000a81548160ff0219169083151502179055505b505050565b600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156106385761079e565b600360008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff16156106ed576000600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff02191690831515021790555061079d565b6000808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff161561079c5760008060008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff0219169083151502179055505b5b5b50565b60006001600083815260200190815260200160002060020160006101000a81548160ff02191690831515021790555050565b600360003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff16151561082e57610995565b600360008273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff1615156108e4576001600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff021916908315150217905550610994565b6000808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160149054906101000a900460ff16156109935760016000808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060000160146101000a81548160ff0219169083151502179055505b5b5b50565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106109d957805160ff1916838001178555610a07565b82800160010185558215610a07579182015b82811115610a065782518255916020019190600101906109eb565b5b509050610a149190610a18565b5090565b610a3a91905b80821115610a36576000816000905550600101610a1e565b5090565b905600a165627a7a72305820f38ddd8ba62581bc770b20eaa3597749bbd3279ded54408cf52e52a65863fef30029"",""opcodes"": ""PUSH1 0x80 PUSH1 0x40 MSTORE CALLVALUE DUP1 ISZERO PUSH2 0x10 JUMPI PUSH1 0x0 DUP1 REVERT JUMPDEST POP PUSH1 0x40 MLOAD PUSH1 0x20 DUP1 PUSH2 0xB88 DUP4 CODECOPY DUP2 ADD DUP1 PUSH1 0x40 MSTORE DUP2 ADD SWAP1 DUP1 DUP1 MLOAD SWAP1 PUSH1 0x20 ADD SWAP1 SWAP3 SWAP2 SWAP1 POP POP POP DUP1 PUSH1 0x3 PUSH1 0x0 DUP4 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x0 PUSH2 0x100 EXP DUP2 SLOAD DUP2 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF MUL NOT AND SWAP1 DUP4 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND MUL OR SWAP1 SSTORE POP PUSH1 0x1 PUSH1 0x3 PUSH1 0x0 DUP4 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 PUSH2 0x100 EXP DUP2 SLOAD DUP2 PUSH1 0xFF MUL NOT AND SWAP1 DUP4 ISZERO ISZERO MUL OR SWAP1 SSTORE POP POP PUSH2 0xA69 DUP1 PUSH2 0x11F PUSH1 0x0 CODECOPY PUSH1 0x0 RETURN STOP PUSH1 0x80 PUSH1 0x40 MSTORE PUSH1 0x4 CALLDATASIZE LT PUSH2 0x78 JUMPI PUSH1 0x0 CALLDATALOAD PUSH29 0x100000000000000000000000000000000000000000000000000000000 SWAP1 DIV PUSH4 0xFFFFFFFF AND DUP1 PUSH4 0xD65DC93 EQ PUSH2 0x7D JUMPI DUP1 PUSH4 0x200F6539 EQ PUSH2 0x12C JUMPI DUP1 PUSH4 0x5AD7A8F7 EQ PUSH2 0x159 JUMPI DUP1 PUSH4 0x5C62FEBA EQ PUSH2 0x1B0 JUMPI DUP1 PUSH4 0x933131F5 EQ PUSH2 0x1F3 JUMPI DUP1 PUSH4 0xCDD0B264 EQ PUSH2 0x220 JUMPI JUMPDEST PUSH1 0x0 DUP1 REVERT JUMPDEST CALLVALUE DUP1 ISZERO PUSH2 0x89 JUMPI PUSH1 0x0 DUP1 REVERT JUMPDEST POP PUSH2 0x12A PUSH1 0x4 DUP1 CALLDATASIZE SUB DUP2 ADD SWAP1 DUP1 DUP1 CALLDATALOAD SWAP1 PUSH1 0x20 ADD SWAP1 DUP3 ADD DUP1 CALLDATALOAD SWAP1 PUSH1 0x20 ADD SWAP1 DUP1 DUP1 PUSH1 0x1F ADD PUSH1 0x20 DUP1 SWAP2 DIV MUL PUSH1 0x20 ADD PUSH1 0x40 MLOAD SWAP1 DUP2 ADD PUSH1 0x40 MSTORE DUP1 SWAP4 SWAP3 SWAP2 SWAP1 DUP2 DUP2 MSTORE PUSH1 0x20 ADD DUP4 DUP4 DUP1 DUP3 DUP5 CALLDATACOPY DUP3 ADD SWAP2 POP POP POP POP POP POP SWAP2 SWAP3 SWAP2 SWAP3 SWAP1 DUP1 CALLDATALOAD SWAP1 PUSH1 0x20 ADD SWAP1 DUP3 ADD DUP1 CALLDATALOAD SWAP1 PUSH1 0x20 ADD SWAP1 DUP1 DUP1 PUSH1 0x1F ADD PUSH1 0x20 DUP1 SWAP2 DIV MUL PUSH1 0x20 ADD PUSH1 0x40 MLOAD SWAP1 DUP2 ADD PUSH1 0x40 MSTORE DUP1 SWAP4 SWAP3 SWAP2 SWAP1 DUP2 DUP2 MSTORE PUSH1 0x20 ADD DUP4 DUP4 DUP1 DUP3 DUP5 CALLDATACOPY DUP3 ADD SWAP2 POP POP POP POP POP POP SWAP2 SWAP3 SWAP2 SWAP3 SWAP1 POP POP POP PUSH2 0x263 JUMP JUMPDEST STOP JUMPDEST CALLVALUE DUP1 ISZERO PUSH2 0x138 JUMPI PUSH1 0x0 DUP1 REVERT JUMPDEST POP PUSH2 0x157 PUSH1 0x4 DUP1 CALLDATASIZE SUB DUP2 ADD SWAP1 DUP1 DUP1 CALLDATALOAD SWAP1 PUSH1 0x20 ADD SWAP1 SWAP3 SWAP2 SWAP1 POP POP POP PUSH2 0x46E JUMP JUMPDEST STOP JUMPDEST CALLVALUE DUP1 ISZERO PUSH2 0x165 JUMPI PUSH1 0x0 DUP1 REVERT JUMPDEST POP PUSH2 0x1AE PUSH1 0x4 DUP1 CALLDATASIZE SUB DUP2 ADD SWAP1 DUP1 DUP1 CALLDATALOAD SWAP1 PUSH1 0x20 ADD SWAP1 SWAP3 SWAP2 SWAP1 DUP1 CALLDATALOAD SWAP1 PUSH1 0x20 ADD SWAP1 SWAP3 SWAP2 SWAP1 DUP1 CALLDATALOAD PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND SWAP1 PUSH1 0x20 ADD SWAP1 SWAP3 SWAP2 SWAP1 POP POP POP PUSH2 0x4BC JUMP JUMPDEST STOP JUMPDEST CALLVALUE DUP1 ISZERO PUSH2 0x1BC JUMPI PUSH1 0x0 DUP1 REVERT JUMPDEST POP PUSH2 0x1F1 PUSH1 0x4 DUP1 CALLDATASIZE SUB DUP2 ADD SWAP1 DUP1 DUP1 CALLDATALOAD PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND SWAP1 PUSH1 0x20 ADD SWAP1 SWAP3 SWAP2 SWAP1 POP POP POP PUSH2 0x5DD JUMP JUMPDEST STOP JUMPDEST CALLVALUE DUP1 ISZERO PUSH2 0x1FF JUMPI PUSH1 0x0 DUP1 REVERT JUMPDEST POP PUSH2 0x21E PUSH1 0x4 DUP1 CALLDATASIZE SUB DUP2 ADD SWAP1 DUP1 DUP1 CALLDATALOAD SWAP1 PUSH1 0x20 ADD SWAP1 SWAP3 SWAP2 SWAP1 POP POP POP PUSH2 0x7A1 JUMP JUMPDEST STOP JUMPDEST CALLVALUE DUP1 ISZERO PUSH2 0x22C JUMPI PUSH1 0x0 DUP1 REVERT JUMPDEST POP PUSH2 0x261 PUSH1 0x4 DUP1 CALLDATASIZE SUB DUP2 ADD SWAP1 DUP1 DUP1 CALLDATALOAD PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND SWAP1 PUSH1 0x20 ADD SWAP1 SWAP3 SWAP2 SWAP1 POP POP POP PUSH2 0x7D3 JUMP JUMPDEST STOP JUMPDEST PUSH1 0x0 TIMESTAMP SWAP1 POP PUSH1 0x1 PUSH1 0x0 DUP3 DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x2 ADD PUSH1 0x0 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO DUP1 ISZERO PUSH2 0x33B JUMPI POP PUSH1 0x0 DUP1 CALLER PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND DUP1 PUSH2 0x33A JUMPI POP PUSH1 0x3 PUSH1 0x0 CALLER PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND JUMPDEST JUMPDEST ISZERO PUSH2 0x469 JUMPI DUP3 PUSH1 0x1 PUSH1 0x0 DUP4 DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD SWAP1 DUP1 MLOAD SWAP1 PUSH1 0x20 ADD SWAP1 PUSH2 0x36A SWAP3 SWAP2 SWAP1 PUSH2 0x998 JUMP JUMPDEST POP DUP2 PUSH1 0x1 PUSH1 0x0 DUP4 DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x1 ADD SWAP1 DUP1 MLOAD SWAP1 PUSH1 0x20 ADD SWAP1 PUSH2 0x395 SWAP3 SWAP2 SWAP1 PUSH2 0x998 JUMP JUMPDEST POP PUSH1 0x1 DUP1 PUSH1 0x0 DUP4 DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x2 ADD PUSH1 0x0 PUSH2 0x100 EXP DUP2 SLOAD DUP2 PUSH1 0xFF MUL NOT AND SWAP1 DUP4 ISZERO ISZERO MUL OR SWAP1 SSTORE POP PUSH32 0xCFCA8D21A1853B213DFC243126EE5355D599BF9F8566CE62AD79AECBEED2F35B DUP4 DUP3 PUSH1 0x40 MLOAD DUP1 DUP1 PUSH1 0x20 ADD DUP4 DUP2 MSTORE PUSH1 0x20 ADD DUP3 DUP2 SUB DUP3 MSTORE DUP5 DUP2 DUP2 MLOAD DUP2 MSTORE PUSH1 0x20 ADD SWAP2 POP DUP1 MLOAD SWAP1 PUSH1 0x20 ADD SWAP1 DUP1 DUP4 DUP4 PUSH1 0x0 JUMPDEST DUP4 DUP2 LT ISZERO PUSH2 0x42D JUMPI DUP1 DUP3 ADD MLOAD DUP2 DUP5 ADD MSTORE PUSH1 0x20 DUP2 ADD SWAP1 POP PUSH2 0x412 JUMP JUMPDEST POP POP POP POP SWAP1 POP SWAP1 DUP2 ADD SWAP1 PUSH1 0x1F AND DUP1 ISZERO PUSH2 0x45A JUMPI DUP1 DUP3 SUB DUP1 MLOAD PUSH1 0x1 DUP4 PUSH1 0x20 SUB PUSH2 0x100 EXP SUB NOT AND DUP2 MSTORE PUSH1 0x20 ADD SWAP2 POP JUMPDEST POP SWAP4 POP POP POP POP PUSH1 0x40 MLOAD DUP1 SWAP2 SUB SWAP1 LOG1 JUMPDEST POP POP POP JUMP JUMPDEST PUSH1 0x2 PUSH1 0x0 DUP3 DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x3 ADD PUSH1 0x0 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO ISZERO PUSH2 0x49D JUMPI PUSH2 0x4B9 JUMP JUMPDEST TIMESTAMP PUSH1 0x2 PUSH1 0x0 DUP4 DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x2 ADD DUP2 SWAP1 SSTORE POP JUMPDEST POP JUMP JUMPDEST PUSH1 0x3 PUSH1 0x0 CALLER PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO ISZERO PUSH2 0x517 JUMPI PUSH2 0x5D8 JUMP JUMPDEST PUSH1 0x3 PUSH1 0x0 DUP3 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO ISZERO PUSH2 0x572 JUMPI PUSH2 0x5D8 JUMP JUMPDEST DUP3 PUSH1 0x2 PUSH1 0x0 TIMESTAMP DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x1 ADD DUP2 SWAP1 SSTORE POP DUP2 PUSH1 0x2 PUSH1 0x0 TIMESTAMP DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x2 ADD DUP2 SWAP1 SSTORE POP PUSH1 0x1 PUSH1 0x2 PUSH1 0x0 TIMESTAMP DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x3 ADD PUSH1 0x0 PUSH2 0x100 EXP DUP2 SLOAD DUP2 PUSH1 0xFF MUL NOT AND SWAP1 DUP4 ISZERO ISZERO MUL OR SWAP1 SSTORE POP JUMPDEST POP POP POP JUMP JUMPDEST PUSH1 0x3 PUSH1 0x0 CALLER PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO ISZERO PUSH2 0x638 JUMPI PUSH2 0x79E JUMP JUMPDEST PUSH1 0x3 PUSH1 0x0 DUP3 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO PUSH2 0x6ED JUMPI PUSH1 0x0 PUSH1 0x3 PUSH1 0x0 DUP4 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 PUSH2 0x100 EXP DUP2 SLOAD DUP2 PUSH1 0xFF MUL NOT AND SWAP1 DUP4 ISZERO ISZERO MUL OR SWAP1 SSTORE POP PUSH2 0x79D JUMP JUMPDEST PUSH1 0x0 DUP1 DUP3 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO PUSH2 0x79C JUMPI PUSH1 0x0 DUP1 PUSH1 0x0 DUP4 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 PUSH2 0x100 EXP DUP2 SLOAD DUP2 PUSH1 0xFF MUL NOT AND SWAP1 DUP4 ISZERO ISZERO MUL OR SWAP1 SSTORE POP JUMPDEST JUMPDEST JUMPDEST POP JUMP JUMPDEST PUSH1 0x0 PUSH1 0x1 PUSH1 0x0 DUP4 DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x2 ADD PUSH1 0x0 PUSH2 0x100 EXP DUP2 SLOAD DUP2 PUSH1 0xFF MUL NOT AND SWAP1 DUP4 ISZERO ISZERO MUL OR SWAP1 SSTORE POP POP JUMP JUMPDEST PUSH1 0x3 PUSH1 0x0 CALLER PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO ISZERO PUSH2 0x82E JUMPI PUSH2 0x995 JUMP JUMPDEST PUSH1 0x3 PUSH1 0x0 DUP3 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO ISZERO PUSH2 0x8E4 JUMPI PUSH1 0x1 PUSH1 0x3 PUSH1 0x0 DUP4 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 PUSH2 0x100 EXP DUP2 SLOAD DUP2 PUSH1 0xFF MUL NOT AND SWAP1 DUP4 ISZERO ISZERO MUL OR SWAP1 SSTORE POP PUSH2 0x994 JUMP JUMPDEST PUSH1 0x0 DUP1 DUP3 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 SWAP1 SLOAD SWAP1 PUSH2 0x100 EXP SWAP1 DIV PUSH1 0xFF AND ISZERO PUSH2 0x993 JUMPI PUSH1 0x1 PUSH1 0x0 DUP1 DUP4 PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND PUSH20 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF AND DUP2 MSTORE PUSH1 0x20 ADD SWAP1 DUP2 MSTORE PUSH1 0x20 ADD PUSH1 0x0 KECCAK256 PUSH1 0x0 ADD PUSH1 0x14 PUSH2 0x100 EXP DUP2 SLOAD DUP2 PUSH1 0xFF MUL NOT AND SWAP1 DUP4 ISZERO ISZERO MUL OR SWAP1 SSTORE POP JUMPDEST JUMPDEST JUMPDEST POP JUMP JUMPDEST DUP3 DUP1 SLOAD PUSH1 0x1 DUP2 PUSH1 0x1 AND ISZERO PUSH2 0x100 MUL SUB AND PUSH1 0x2 SWAP1 DIV SWAP1 PUSH1 0x0 MSTORE PUSH1 0x20 PUSH1 0x0 KECCAK256 SWAP1 PUSH1 0x1F ADD PUSH1 0x20 SWAP1 DIV DUP2 ADD SWAP3 DUP3 PUSH1 0x1F LT PUSH2 0x9D9 JUMPI DUP1 MLOAD PUSH1 0xFF NOT AND DUP4 DUP1 ADD OR DUP6 SSTORE PUSH2 0xA07 JUMP JUMPDEST DUP3 DUP1 ADD PUSH1 0x1 ADD DUP6 SSTORE DUP3 ISZERO PUSH2 0xA07 JUMPI SWAP2 DUP3 ADD JUMPDEST DUP3 DUP2 GT ISZERO PUSH2 0xA06 JUMPI DUP3 MLOAD DUP3 SSTORE SWAP2 PUSH1 0x20 ADD SWAP2 SWAP1 PUSH1 0x1 ADD SWAP1 PUSH2 0x9EB JUMP JUMPDEST JUMPDEST POP SWAP1 POP PUSH2 0xA14 SWAP2 SWAP1 PUSH2 0xA18 JUMP JUMPDEST POP SWAP1 JUMP JUMPDEST PUSH2 0xA3A SWAP2 SWAP1 JUMPDEST DUP1 DUP3 GT ISZERO PUSH2 0xA36 JUMPI PUSH1 0x0 DUP2 PUSH1 0x0 SWAP1 SSTORE POP PUSH1 0x1 ADD PUSH2 0xA1E JUMP JUMPDEST POP SWAP1 JUMP JUMPDEST SWAP1 JUMP STOP LOG1 PUSH6 0x627A7A723058 KECCAK256 RETURN DUP14 0xdd DUP12 0xa6 0x25 DUP2 0xbc PUSH24 0xB20EAA3597749BBD3279DED54408CF52E52A65863FEF300 0x29 "",""sourceMap"": ""26:3552:0:-;;;1104:133;8:9:-1;5:2;;;30:1;27;20:12;5:2;1104:133:0;;;;;;;;;;;;;;;;;;;;;;;;;;;;;1173:7;1150:8;:17;1159:7;1150:17;;;;;;;;;;;;;;;:20;;;:30;;;;;;;;;;;;;;;;;;1225:4;1191:8;:17;1200:7;1191:17;;;;;;;;;;;;;;;:31;;;:38;;;;;;;;;;;;;;;;;;1104:133;26:3552;;;;;;""}";

            var receipt = await
                web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, byteCode, "f34541f637ef17d92bf54d22308c56e2af9a86fe",
                    new HexBigInteger(900000), null);

            var contractAddress = receipt.ContractAddress;

            var contract = web3.Eth.GetContract(abi, contractAddress);

            Assert.IsNotNull(contract);

        }
    }
}
