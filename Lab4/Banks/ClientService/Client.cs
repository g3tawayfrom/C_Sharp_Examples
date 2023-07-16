namespace Banks
{
    public class Client
    {
        public Client(string? name, string? surname, string? passportId = null)
        {
            Name = name;
            Surname = surname;
            PassportId = passportId;
            SubscriptionsList = new List<Guid>();
            NotificationsList = new List<Notification>();
        }

        public string? Name { get; } = default;
        public string? Surname { get; } = default;
        public string? PassportId { get; } = default;
        public List<Guid> SubscriptionsList { get; }
        public List<Notification> NotificationsList { get; }

        public void SubcribeToBanksNotification(Guid id)
        {
            SubscriptionsList.Add(id);
        }
    }
}
