using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citizens
{
    public class Citizen : ICitizen
    {
        public Citizen(string firstName, string lastName, DateTime birthDate, Gender gender)
        {
            this.firstName = firstName.Transform(To.TitleCase);
            this.lastName = lastName.Transform(To.TitleCase);

            if (birthDate.Date <= SystemDateTime.Now().Date)
                this.birthDate = birthDate.Date;
            else
                throw new ArgumentException("Wrong date");

            if (Enum.IsDefined(typeof(Gender), gender))
                this.gender = gender;
            else
                throw new ArgumentOutOfRangeException("invalid gender");
        }

        public DateTime birthDate { get; }

        public string firstName { get; }

        public Gender gender { get; }

        public string lastName { get; }

        public string vatId { get; set; }

        public ICitizen Clone()
        {
            Citizen newCitizen = new Citizen(firstName, lastName, birthDate, gender);
            newCitizen.vatId = vatId;
            return newCitizen;
        }
    }
}
