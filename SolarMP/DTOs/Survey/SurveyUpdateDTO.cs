﻿namespace SolarMP.DTOs.Survey
{
    public class SurveyUpdateDTO
    {
        public string SurveyId { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public string? StaffId { get; set; }
        public bool? Status { get; set; }
        public string? RequestId { get; set; }
        public decimal? KWperMonth { get; set; }
        public decimal? RoofArea { get; set; }
    }
}
