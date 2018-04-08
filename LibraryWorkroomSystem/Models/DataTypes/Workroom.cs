using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWorkroomSystem.Models.DataTypes
{
    /// <summary>
    /// This class is a data container for workroom information.
    /// </summary>
    public class Workroom : IEquatable<Workroom>
    {
        /// <summary>
        /// Workroom number.
        /// </summary>
        public int number { get; set; }

        /// <summary>
        /// Floor number.
        /// </summary>
        public int floor { get; set; }

        /// <summary>
        /// Workroom capacity.
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// Admin responsible for managing this workroom.
        /// </summary>
        public int admin { get; set; }

        /// <summary>
        /// Method to determine if two workrooms are equivalent.
        /// </summary>
        /// <param name="other">Object to compare to.</param>
        /// <returns>true if equal, false othewise.</returns>
        public bool Equals(Workroom other)
        {
            return other.number == number && other.floor == floor;
        }
    }
}