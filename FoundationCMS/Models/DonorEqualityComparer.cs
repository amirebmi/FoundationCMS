using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Models
{
    public class DonorEqualityComparer : IEqualityComparer<Donor>
    {
        public bool Equals(Donor d1, Donor d2)
        {
            if (d1 == null && d2 == null)
                return true;
            else if (d1 == null || d2 == null)
                return false;
            else if (d1.Id == d2.Id)
                return true;
            else
                return false;
        }

        public int GetHashCode(Donor obj)
        {
            int hCode = obj.Id;
            return hCode.GetHashCode();
        }
    }
}
