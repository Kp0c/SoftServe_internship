using Humanizer;
using System;

namespace Citizens
{
    public class Citizen : ICitizen
    {
        public Citizen(string firstName, string lastName, DateTime birthDate, Gender gender)
        {
            FirstName = firstName.Transform(To.TitleCase);
            LastName = lastName.Transform(To.TitleCase);

            if (birthDate.Date <= SystemDateTime.Now().Date)
            {
                BirthDate = birthDate.Date;
            }
            else
            {
                throw new ArgumentException("Wrong date");
            }

            if (Enum.IsDefined(typeof(Gender), gender))
            {
                Gender = gender;
            }
            else
            {
                throw new ArgumentOutOfRangeException("invalid gender");
            }
        }

        public DateTime BirthDate { get; }

        public string FirstName { get; }

        public Gender Gender { get; }

        public string LastName { get; }

        public string VatId { get; set; }

        public ICitizen Clone()
        {
            Citizen newCitizen = new Citizen(FirstName, LastName, BirthDate, Gender);
            newCitizen.VatId = VatId;
            return newCitizen;
        }
    }
}
