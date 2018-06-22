using System;
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
