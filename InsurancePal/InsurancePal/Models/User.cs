namespace InsurancePal.Models
{
    public class User
    {
        private int id;
        private string username;
        private string passwordHash;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }

        }

        public string PasswordHash
        {
            get
            {
                return passwordHash;
            }

            set
            {
                passwordHash = value;
            }
        }
    }
}
