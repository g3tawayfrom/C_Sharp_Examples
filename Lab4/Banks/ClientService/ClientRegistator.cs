using Banks.Exceptions;

namespace Banks.ClientService
{
    public class ClientRegistator
    {
        private ClientBuilder? builder;

        public ClientBuilder Builder
        {
            get
            {
                if (builder == null)
                {
                    throw new ObjectNotFoundException();
                }

                return builder;
            }
            set
            {
                builder = value;
            }
        }

        public Client BuildBasicClient(string name, string surname)
        {
            if (builder == null)
            {
                throw new ObjectNotFoundException();
            }

            builder.AddNameToClient(name);
            builder.AddSurnameToClient(surname);

            return builder.GetClient();
        }

        public Client BuildAdvancedClient(string name, string surname, string passportId)
        {
            if (builder == null)
            {
                throw new ObjectNotFoundException();
            }

            builder.AddNameToClient(name);
            builder.AddSurnameToClient(surname);
            builder.AddPassportIdToClient(passportId);

            return builder.GetClient();
        }

        public void UpgradeClientsInfo(string passportId)
        {
            if (builder == null)
            {
                throw new ObjectNotFoundException();
            }

            builder.AddPassportIdToClient(passportId);
        }
    }
}
