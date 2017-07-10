using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                return registry.FirstOrDefault(citizen => citizen.Value.vatId == id).Value;
            }
        }

        private static int[] ConvertStringToIntArray(string number)
        {
            return number.Replace(" ", string.Empty).Select(c => (int)char.GetNumericValue(c)).ToArray();
        }

        private int CalculateControlNumber(string vat)
        {
            int[] numbers = ConvertStringToIntArray(vat);

            return (-numbers[0] + 5 * numbers[1] + 7 * numbers[2] + 9 * numbers[3] + 4 * numbers[4]
                + 6 * numbers[5] + 10 * numbers[6] + 5 * numbers[7] + 7 * numbers[8]) % 11 % 10;
        }

        private string GetOrderNumber(Gender gender, DateTime birthDate)
        {
            int todayWomenCounter = registry.Where(citizen => citizen.Value.gender == Gender.Female && citizen.Value.birthDate == birthDate).Count();
            int todayMenCounter = registry.Where(citizen => citizen.Value.gender == Gender.Male && citizen.Value.birthDate == birthDate).Count();

            while ((gender == Gender.Male && todayMenCounter >= MAX_PEOPLES) || (gender == Gender.Female && todayWomenCounter >= MAX_PEOPLES))
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
            StringBuilder vat = new StringBuilder((citizen.birthDate - DECEMBER_31_1899).Days.ToString().PadRight(5, '0'), 10);
            
            vat.Append(GetOrderNumber(citizen.gender, citizen.birthDate));

            vat.Append(CalculateControlNumber(vat.ToString()).ToString());

            return vat.ToString();
        }

        public void Register(ICitizen citizen)
        {
            if (citizen.vatId == null || citizen.vatId == string.Empty)
            {
                citizen.vatId = VatBuilder(citizen);
            }

            if (registry.ContainsKey(citizen.vatId))
            {
                throw new InvalidOperationException("Already contains this citizen");
            }

            registry.Add(citizen.vatId, citizen.Clone());
            lastRegistrationDate = SystemDateTime.Now();
        }

        private string GetTextRepresentationOfDaysAgoField(int days)
        {
            if (days < 1)
                return "today";
            else if (days == 1)
                return "yesterday";
            else if (days <= 7)
                return "this week";
            else if (days <= 14)
                return "week ago";
            else if (days <= 21)
                return "two weeks ago";
            else if (days <= 28)
                return "three weeks ago";

            return "more than month ago";
        }

        private string GetGrammarRightValueMan(int men)
        {
            return men == 1 ? "man" : "men";
        }

        private int GetMaleCount() { return registry.Where(citizen => citizen.Value.gender == Gender.Male).Count(); }
        private int GetFemaleCount() { return registry.Where(citizen => citizen.Value.gender == Gender.Female).Count(); }

        public string Stats()
        {
            string stats = "{0} and {1}.".FormatWith("man".ToQuantity(GetMaleCount()), "woman".ToQuantity(GetFemaleCount()));

            if (lastRegistrationDate.HasValue)
            {
                stats += " Last registration was {0}".FormatWith(lastRegistrationDate.Humanize(true, SystemDateTime.Now()));
            }

            return stats;
        }
    }
}
