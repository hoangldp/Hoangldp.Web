﻿using System;

namespace Hoangldp.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoTrimAttribute : Attribute
    {
        public bool IsTrim { get; }

        public NoTrimAttribute()
        {
            IsTrim = false;
        }

        public NoTrimAttribute(bool isTrim)
        {
            IsTrim = isTrim;
        }
    }
}