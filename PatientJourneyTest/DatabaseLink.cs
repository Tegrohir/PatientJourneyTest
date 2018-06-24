namespace PatientJourneyTest
{
    public class DatabaseLink
    {
        //Don't make this an actual link/uri, this has to be encrypted data, like everything else referenced on the blockchain
        private string uri;
        // bool to check if the object exists in the addDataBaseLink method, should default to false, and be changed to true when the object is instantiated.
        private string hash;
        private string publicKey;

        public DatabaseLink(string uri, string hash, string publicKey)
        {
            this.uri = uri;
            this.hash = hash;
            this.publicKey = publicKey;
        }

        public string Uri
        {
            get { return uri; }
            set { uri = value; }
        }

        public string Hash
        {
            get { return hash; }
            set { hash = value; }
        }        

        public string PublicKey
        {
            get { return publicKey; }
            set { publicKey = value; }
        }
    }
}