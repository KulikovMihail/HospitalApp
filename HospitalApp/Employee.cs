﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public enum Role
    {
        Intern,
        Nurse,
        Doctor
    }

    public class Employee
    {
        public string Name { get; set; }
        public Role UserRole { get; set; }
    }
}
