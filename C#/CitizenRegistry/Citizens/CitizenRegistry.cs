using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Citizens
{
    public class CitizenRegistry : ICitizenRegistry
    {
        //cannot use const to DateTime
        static readonly DateTime DECEMBER_31_1899 = new DateTime(1899, 12, 31);
        const int MAX_PEOPLES = 5000; 
        DateTime? lastRegistrationDate = null;

        Dictionary<string, ICitizen> registry = new Dictionary<string, ICitizen>();

        public ICitizen this[string id]
        {
            get
            {
                if (id == null)
                {
                    throw new ArgumentNullException("id cannot be null");
                }

                return registry.FirstOrDefault(citizen => citizen.Value.VatId == id).Value;
            }
        }

        private int CalculateControlNumber(string vat)
        {
            int[] numbers = CitizenRegistryHelper.ConvertStringToIntArray(vat);

            return (-numbers[0] + 5 * numbers[1] + 7 * numbers[2] + 9 * numbers[3] + 4 * numbers[4]
                + 6 * numbers[5] + 10 * numbers[6] + 5 * numbers[7] + 7 * numbers[8]) % 11 % 10;
        }

        private string GetOrderNumber(Gender gender, DateTime birthDate)
        {
            int todayWomenCounter = registry.Where(citizen => citizen.Value.Gender == Gender.Female && citizen.Value.BirthDate == birthDate).Count();
            int todayMenCounter = registry.Where(citizen => citizen.Value.Gender == Gender.Male && citizen.Value.BirthDate == birthDate).Count();

            if ((gender == Gender.Male && todayMenCounter >= MAX_PEOPLES) || (gender == Gender.Female && todayWomenCounter >= MAX_PEOPLES))
            {
                throw new InvalidOperationException(string.Format("maximum peoples for {0} birthdate", birthDate.ToShortDateString()));
            }

            //get next number considering gender counter
            string orderNumber = ((gender == Gender.Female ? todayWomenCounter : todayMenCounter) / 5).ToString();

            //set gender counter
            if (gender == Gender.Female)
            {
                orderNumber += todayWomenCounter * 2 % 10;
            }
            else
            {
                orderNumber += (todayMenCounter * 2 + 1) % 10;
            }

            //extend to 4 symbols
            return orderNumber.PadLeft(4, '0');
        }

        private string VatBuilder(ICitizen citizen)
        {
            //days from 31 december 1899, extended to 5 symbols
            StringBuilder vat = new StringBuilder((citizen.BirthDate - DECEMBER_31_1899).Days.ToString().PadRight(5, '0'), 10);
            
            vat.Append(GetOrderNumber(citizen.Gender, citizen.BirthDate));

            vat.Append(CalculateControlNumber(vat.ToString()).ToString());

            return vat.ToString();
        }

        public void Register(ICitizen citizen)
        {
            if (citizen.VatId == null || citizen.VatId == string.Empty)
            {
                citizen.VatId = VatBuilder(citizen);
            }

            if (registry.ContainsKey(citizen.VatId))
            {
                throw new InvalidOperationException("Already contains this citizen.");
            }

            registry.Add(citizen.VatId, citizen.Clone());
            lastRegistrationDate = SystemDateTime.Now();
        }

        private int GetMaleCount()
        {
            return registry.Where(citizen => citizen.Value.Gender == Gender.Male).Count();
        }

        private int GetFemaleCount()
        {
            return registry.Where(citizen => citizen.Value.Gender == Gender.Female).Count();
        }

        public string Stats()
        {
            string stats = "{0} and {1}.".FormatWith("man".ToQuantity(GetMaleCount()), "woman".ToQuantity(GetFemaleCount()));

            if (lastRegistrationDate.HasValue)
            {
                stats += " Last registration was {0}.".FormatWith(lastRegistrationDate.Humanize(dateToCompareAgainst: SystemDateTime.Now()));
            }

            return stats;
        }
    }
}
