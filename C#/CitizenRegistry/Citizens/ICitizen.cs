using System;

namespace Citizens
{
    public interface ICitizen
    {
        string firstName { get; }
        string lastName { get; }
        Gender gender { get; }
        DateTime birthDate { get; }
        string vatId { get; set; }

        ICitizen Clone();
    }
}