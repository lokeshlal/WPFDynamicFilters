using System.Collections.Generic;

namespace WPFDynamicFilters
{
    public class GetDropDown
    {
        public static List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            countries.Add(new Country() { Id = 1, Name = "India" });
            countries.Add(new Country() { Id = 2, Name = "Australia" });
            countries.Add(new Country() { Id = 3, Name = "Denmark" });

            return countries;
        }
    }
}
