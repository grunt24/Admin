﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BCASLibrary.Areas.Identity.Data;

// Add profile data for application users by adding properties to the LibraryUser class
public class LibraryUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
