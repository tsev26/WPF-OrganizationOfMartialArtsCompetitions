using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace OOMAC.Domain.Models
{

    [Table("Contestant")]
    public class Contestant : DomainObject
    {

        [Column("ConId")]
        public new int Id { get; set; }

        [Column("ConLName")]
        public string LastName { get; set; }

        [Column("ConFName")]
        public string FirstName { get; set; }

        [Column("ConEmail")]
        public string Email { get; set; }

        [Column("ConDateBorn")]
        public DateTime DateBorn { get; set; }


        [Column("ConTechnicalSkill")]
        public TechnicalSkill TechSkill { get; set; }

        public int Age => CalculateAge(DateBorn);

        public List<Tournament> Tournaments { get; set; }

        public string TechSkillString => GetEnumDescription(TechSkill);

        public enum TechnicalSkill
        {
            [Description("10kyu")]
            _10kyu = 1,
            [Description("9kyu")]
            _9kyu = 2,
            [Description("8kyu")]
            _8kyu = 3,
            [Description("7kyu")]
            _7kyu = 4,
            [Description("6kyu")]
            _6kyu = 5,
            [Description("5kyu")]
            _5kyu = 6,
            [Description("4kyu")]
            _4kyu = 7,
            [Description("3kyu")]
            _3kyu = 8,
            [Description("2kyu")]
            _2kyu = 9,
            [Description("1kyu")]
            _1kyu = 10,
            [Description("1dan")]
            _1dan = 11,
            [Description("2dan")]
            _2dan = 12,
            [Description("3dan")]
            _3dan = 13,
            [Description("4dan")]
            _4dan = 14,
            [Description("5dan")]
            _5dan = 15,
            [Description("6dan")]
            _6dan = 16,
            [Description("7dan")]
            _7dan = 17,
            [Description("8dan")]
            _8dan = 18,
            [Description("9dan")]
            _9dan = 19,
            [Description("10dan")]
            _10dan = 20
        }

        public int CalculateAge(DateTime birthDate)
        {
            int age =  DateTime.Now.Year - birthDate.Year;

            if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
                age--;

            return age;
        }

        public static string GetEnumDescription(Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null) return "";
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
