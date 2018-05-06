namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        /// <summary>
        /// Player Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Player First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Player Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// player email address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// player cell phone 
        /// </summary>
        public string CellphoneNumber { get; set; }

        public string FullName
        {
            get
            {
                return $"{ FirstName } { LastName }";
            }
        }
    }
}
