﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    public class User:IEntity
    {
        public int UserId { get; set; }
        public long CitizenId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhones { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public bool Status { get; set; }
        public DateTime RecordDate { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public DateTime UpdateContactDate { get; set; }

        [NotMapped]
        public string AuthenticationProviderType { get; set; } = "Person";
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public User()
        {
            UpdateContactDate = RecordDate = DateTime.Now;
            Status = true;
        }

        public bool UpdateMobilePhone(string mobilePhone)
        {
            if (mobilePhone != MobilePhones)
            {
                MobilePhones = mobilePhone;
                return true;
            }
            else
                return false;
        }

    }
}