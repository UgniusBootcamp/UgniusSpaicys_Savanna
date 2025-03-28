﻿using System.ComponentModel.DataAnnotations;

namespace SavannaApp.Data.Entities.Auth
{
    public class Session
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string LastRefreshToken { get; set; } = null!;

        [Required]
        public DateTimeOffset InitiatedAt{ get; set; }

        [Required]
        public DateTimeOffset ExpiresAt { get; set; }

        [Required]
        public bool IsRevoked { get; set; }

        [Required]

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
