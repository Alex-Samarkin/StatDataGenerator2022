// SimpleStat
// SimpleStat1
// TUser.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 21:35 13 02 2022

using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace SimpleStat1
{
    public class TUser
    {
        [Category("Личные данные"), Description("Фамилия"), DefaultValue("Иванов")]
        [DisplayName("Фамилия")]
        [NotifyParentProperty(true)]
        public string FirstName { get; set; } = "Иванов";

        [Category("Личные данные"), Description("Отчество"), DefaultValue("Иванович")]
        [DisplayName("Отчество")]
        [NotifyParentProperty(true)]
        public string MiddleName { get; set; } = "Иванович";

        [Category("Личные данные"), Description("Имя"), DefaultValue("Иван")]
        [DisplayName("Имя")]
        [NotifyParentProperty(true)]
        public string LastName { get; set; } = "Иван";
        
        public string Abbrev ()  => $"{LastName[0]}.{MiddleName[0]}.";

        [Category("Личные данные"), Description("Полное имя")]
        [DisplayName("Полное имя")]
        [NotifyParentProperty(true)]
        public string FullName =>$"{FirstName} {LastName} {MiddleName}";
        
        [Category("Личные данные"), Description("Полное имя")]
        [DisplayName("Инициалы")]
        [NotifyParentProperty(true)]
        public string ShortName => $"{FirstName} {Abbrev()}";

        [Category("Сведения о группе"), Description("Курс"), DefaultValue("3")]
        [DisplayName("Курс")]
        [NotifyParentProperty(true)]
        public string Course { get; set; } = "3";
        
        [Category("Сведения о группе"), Description("Группа"), DefaultValue("0033-")]
        [DisplayName("Группа")]
        [NotifyParentProperty(true)]
        public string Group { get; set; } = "0033-04";
        
        [Category("Сведения о группе"), Description("Название института"), DefaultValue("Институт инженерных наук")]
        [DisplayName("Институт")]
        [NotifyParentProperty(true)]
        public string Branch { get; set; } = "Институт инженерных наук";

        [Category("Время выполнения"), Description("")]
        [DisplayName("Дата")]
        [NotifyParentProperty(true)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Category("Вспомогательные данные"), Description(""), DefaultValue("")]
        [DisplayName("hash-code")]
        [NotifyParentProperty(true)]
        public int Hash
        {
            get
            {
                var hashCode = FullName.GetHashCode();
                hashCode = (hashCode * 397) ^ (Course.GetHashCode());
                hashCode = (hashCode * 397) ^ (Group.GetHashCode());
                hashCode = (hashCode * 397) ^ (Branch.GetHashCode());
                hashCode = (hashCode * 397) ^ Date.GetHashCode();
                return hashCode;
            }
        }


        #region UserEqualityComparer

        private sealed class UserEqualityComparer : IEqualityComparer<TUser>
        {
            public bool Equals(TUser x, TUser y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.FirstName == y.FirstName && x.MiddleName == y.MiddleName && x.LastName == y.LastName && x.Course == y.Course && x.Group == y.Group && x.Branch == y.Branch && x.Date.Equals(y.Date);
            }

            public int GetHashCode(TUser obj)
            {
                unchecked
                {
                    /*
                    var hashCode = (obj.FirstName != null ? obj.FirstName.GetHashCode() : 0);
                     hashCode = (hashCode * 397) ^ (obj.MiddleName != null ? obj.MiddleName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.LastName != null ? obj.LastName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Course != null ? obj.Course.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Group != null ? obj.Group.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Branch != null ? obj.Branch.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.Date.GetHashCode();
                    //hashCode = (hashCode * 397) ^ obj.Name.GetHashCode();
                    */
                    var hashCode = (obj.FullName != null ? obj.FullName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Course != null ? obj.Course.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Group != null ? obj.Group.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Branch != null ? obj.Branch.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.Date.GetHashCode();

                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<TUser> UserComparer { get; } = new UserEqualityComparer();

        #endregion
    }
}