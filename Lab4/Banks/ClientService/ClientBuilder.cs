namespace Banks.ClientService
{
    public class ClientBuilder
    {
        private string? name;
        private string? surname;
        private string? passportId;

        public ClientBuilder() { }

        public void AddNameToClient(string name)
        {
            this.name = name;
        }

        public void AddSurnameToClient(string surname)
        {
            this.surname = surname;
        }

        public void AddPassportIdToClient(string passportId)
        {
            this.passportId = passportId;
        }

        public Client GetClient()
        {
            var client = new Client(name, surname, passportId);

            return client;
        }
    }
}
