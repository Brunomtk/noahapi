﻿using System;
using Core.Enums;

namespace Core.DTO.GpsTracking
{
    public class CreateGpsTrackingDTO
    {
        public int ProfessionalId { get; set; }
        public string? ProfessionalName { get; set; }

        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }

        public string Vehicle { get; set; } = string.Empty;

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Accuracy { get; set; }

        public double Speed { get; set; }
        public GpsTrackingStatus Status { get; set; } = GpsTrackingStatus.Active;

        public int Battery { get; set; }
        public string? Notes { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
