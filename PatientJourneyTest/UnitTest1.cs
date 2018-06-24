using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using System.Threading;
using System.Collections.Generic;

namespace PatientJourneyTest
{
    [TestClass]
    public class PatientJourneyTests
    {
        [TestMethod]
        public async Task ShouldProcessTestMethods()
        {
            var service = new PatientJourneyServices();
            var account = service.AccountFactory();
            var receipt = await service.CreateNewPatientJourneyRequestContract(account);
            Assert.IsNotNull(receipt);

            var web3 = service.GetWeb3Instance(account);
            var contractAddress = receipt.ContractAddress;

            var contract = service.GetContract(web3, contractAddress);
            var multiplyFunction = contract.GetFunction("multiply");
            var multiply2Function = contract.GetFunction("multiply2");
            var testFunction = contract.GetFunction("test");
            var getMultiplierFunction = contract.GetFunction("getMultiplier");
            var getSecondMultiplierFunction = contract.GetFunction("getSecondMultiplier");

            var result = 0;
            result = await multiplyFunction.CallAsync<int>(5);
            Assert.AreEqual(25, result);

            result = await multiply2Function.CallAsync<int>(5);
            Assert.AreEqual(50, result);

            result = await getMultiplierFunction.CallAsync<int>();
            Assert.AreEqual(5, result);

            result = await getSecondMultiplierFunction.CallAsync<int>();
            Assert.AreEqual(10, result);

            await testFunction.SendTransactionAsync(account.Address, new HexBigInteger(64777777), null, 2, 4);
            await testFunction.SendTransactionAsync(account.Address, new HexBigInteger(64777777), null, 2, 4);

            result = await multiplyFunction.CallAsync<int>(5);
            Assert.AreEqual(10, result);

            result = await multiply2Function.CallAsync<int>(5);
            Assert.AreEqual(20, result);

            result = await getMultiplierFunction.CallAsync<int>();
            Assert.AreEqual(2, result);

            result = await getSecondMultiplierFunction.CallAsync<int>();
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public async Task ShouldProcessPatientJourneys()
        {
            // Instantiate variables required for getting the contract
            var service = new PatientJourneyServices();
            var account = service.AccountFactory();
            var web3 = service.GetWeb3Instance(account);
            var receipt = await service.CreateNewPatientJourneyRequestContract(account);
            Assert.IsNotNull(receipt);

            var contractAddress = receipt.ContractAddress;

            PatientJourney patientJourney = new PatientJourney();
            patientJourney.ContractAddress = contractAddress;
            Assert.IsNotNull(patientJourney.ContractAddress);

            // Retrieve the specific PatientJourney contract through web3 from the blockchain. 
            var contractPatientJourney = service.GetContract(web3, patientJourney.ContractAddress);

            // Get the various functions from the Contract. 
            var addDatabaseLinkFunction = contractPatientJourney.GetFunction("addDatabaseLink");
            var invalidateDatabaseLinkFunction = contractPatientJourney.GetFunction("invalidateDatabaseLink");
            var validatePersonFunction = contractPatientJourney.GetFunction("validatePerson");
            var invalidatePersonFunction = contractPatientJourney.GetFunction("invalidatePerson");
            var addPermissionFunction = contractPatientJourney.GetFunction("addPermission");
            var removePermissionFunction = contractPatientJourney.GetFunction("removePermission");

            // Functions for retrieving a databaseLink object
            var getDatabaseLinkCountFunction = contractPatientJourney.GetFunction("getDatabaseLinkCount");
            var getDatabaseLinkKeyFunction = contractPatientJourney.GetFunction("getDatabaseLinkKeyFromEnumerator");
            var getDatabaseLinkUriFunction = contractPatientJourney.GetFunction("getDatabaseLinkAtAddress");
            var getDatabaseLinkhashFunction = contractPatientJourney.GetFunction("getDatabaseLinkHashAtAddress");
            var getDatabaseLinkPublicKeyFunction = contractPatientJourney.GetFunction("getDatabaseLinkKeyAtAddress");

            // Functions for retrieving permissions
            var getPermissionEnumeratorFunction = contractPatientJourney.GetFunction("getPermissionEnumerator");
            var getPermissionDateFromFunction = contractPatientJourney.GetFunction("getPermissionFromAtAddress");
            var getPermissionDateToFunction = contractPatientJourney.GetFunction("getPermissionToAtAddress");
            var getPermissionPersonFunction = contractPatientJourney.GetFunction("getPermissionPersonAtAddress");

            // Setting up parameter objects where applicable. 
            var dateFrom = new DateTime(2018, 6, 21);
            var dateTo = new DateTime(2019, 6, 21);
            var dateFromUnixTimestamp = service.ConvertDateToUnixTimestamp(dateFrom);
            var dateToUnixTimestamp = service.ConvertDateToUnixTimestamp(dateTo);
            Object[] addPermissionFunctionParameters = { dateFromUnixTimestamp, dateToUnixTimestamp, account.Address };
            var dataHash = "dataHash"; // This is supposed to be a hash containing the data at the corresponding data link, to ensure the data has not been changed. 
            var dataUri = "dataUri"; // This has been filled with dummy data as of now because we dont have actual links yet. 
            var dataPublicKey = "dataPublicKey"; // Same for public key. 
            var databaseLink = new DatabaseLink(uri: dataUri, hash: dataHash, publicKey: dataPublicKey);
            object[] addDatabaseLinkFunctionParameters = { databaseLink.Uri, databaseLink.Hash, databaseLink.PublicKey };

            // Calling the functions by sending an async transaction to the blockchain. 
            await validatePersonFunction.SendTransactionAsync(account.Address, new HexBigInteger(64777777), null, account.Address);
            await addPermissionFunction.SendTransactionAsync(account.Address, new HexBigInteger(64777777), null, dateFromUnixTimestamp, dateToUnixTimestamp, account.Address);
            await addDatabaseLinkFunction.SendTransactionAsync(account.Address, new HexBigInteger(64777777), null, databaseLink.Uri, databaseLink.Hash, databaseLink.PublicKey);
            
            // Calling the blockchain methods to expose the data from the blockchain.
            // There are two methods to extract a list from the blockchain, see this example and the example from the permissionList below. 
            int count = await getDatabaseLinkCountFunction.CallAsync<int>(); 
            DatabaseLink[] databaseLinkArray = new DatabaseLink[count];
            for (int index = 0; index < count; index++) 
            {
                int key = await getDatabaseLinkKeyFunction.CallAsync<int>(index);
                string uri = await getDatabaseLinkUriFunction.CallAsync<string>(key);
                string hash = await getDatabaseLinkhashFunction.CallAsync<string>(key);
                string publicKey = await getDatabaseLinkPublicKeyFunction.CallAsync<string>(key);
                databaseLinkArray.SetValue(new DatabaseLink(uri, hash, publicKey), index);
            }

            var databaseLinkFromArray = (DatabaseLink)databaseLinkArray.GetValue(count - 1);
            Assert.AreEqual(databaseLink.Uri, databaseLinkFromArray.Uri.ToString());

            // Retrieving permissions
            // There are two methods to extract a list from the blockchain, see this example and the example from the databaseLinkList above. 
            var permissionEnumerator = await getPermissionEnumeratorFunction.CallAsync<List<int>>();
            Permission[] permissionList = new Permission[permissionEnumerator.Count];
            int permissionIndex = 0;
            foreach( int key in permissionEnumerator)
            {
                int retrievalUnixTimestampFrom = await getPermissionDateFromFunction.CallAsync<int>(key);
                int retrievalUnixTimestampTo = await getPermissionDateToFunction.CallAsync<int>(key);
                string retrievalPermissionAddress = await getPermissionPersonFunction.CallAsync<string>(key);
                DateTime retrievalDateFrom = service.ConvertUnixTimestampToDate(retrievalUnixTimestampFrom);
                DateTime retrievalDateTo = service.ConvertUnixTimestampToDate(retrievalUnixTimestampTo);
                permissionList.SetValue(new Permission(dateFrom, dateTo, retrievalPermissionAddress), permissionIndex);
                permissionIndex++;
            }

            var permissionFromArray = (Permission)permissionList.GetValue(0);
            Assert.AreEqual(dateFrom, permissionFromArray.DateFrom);
            
            // Calling the invalidate and removal functions
            await invalidateDatabaseLinkFunction.SendTransactionAsync(from: account.Address, gas: new HexBigInteger(64777777), value: null, functionInput: 25);
            await removePermissionFunction.SendTransactionAsync(from: account.Address, gas: new HexBigInteger(64777777), value: null, functionInput: account.Address);
            await invalidatePersonFunction.SendTransactionAsync(from: account.Address, gas: new HexBigInteger(64777777), value: null, functionInput: account.Address);
        }
    }
}