﻿using System;
using System.Collections.Generic;

namespace BankProject.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Givenname { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Streetaddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public string? NationalId { get; set; }

    public string? Telephonecountrycode { get; set; }

    public string? Telephonenumber { get; set; }

    public string? Emailaddress { get; set; }

    public int CustomerGender { get; set; }

    public int CustomerCountry { get; set; }

    public int? CustomerNumber { get; set; }

    public virtual ICollection<Disposition> Dispositions { get; set; } = new List<Disposition>();
}
