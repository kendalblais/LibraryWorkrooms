using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWorkroomSystem.Models.DataTypes
{
    /// <summary>
    /// This class is a data container that represents a workroom reservation.
    /// All reservations are a time length of 1 hour.
    /// </summary>
    public class WorkroomReservation : IEquatable<WorkroomReservation>
    {
        /// <summary>
        /// Information of the reserved workroom.
        /// </summary>
        public Workroom room { get; set; }

        /// <summary>
        /// Username of the reserver.
        /// </summary>
        public string reserver { get; set; }

        /// <summary>
        /// DateTime representing the time of the reservation.
        /// </summary>
        public DateTime timeOfReservation { get; set; }

        /// <summary>
        /// Method to determine if two reservations are equivalent.
        /// Note: This method is intentionally not entirely correct.
        /// </summary>
        /// <param name="other">Object to compare to.</param>
        /// <returns>true if equal, false othewise.</returns>
        public bool Equals(WorkroomReservation other)
        {
            return other.timeOfReservation.Hour == timeOfReservation.Hour;
        }
    }
}