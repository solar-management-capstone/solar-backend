﻿using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Survey;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class SurveyServices : ISurvey
    {
        protected readonly solarMPContext context;
        public SurveyServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteSurvey(string surveyId)
        {
            try
            {
                var survey = await this.context.Survey
                    .Where(x => surveyId.Equals(x.SurveyId))
                    .FirstOrDefaultAsync();
                if (survey != null)
                {
                    survey.Status = false;
                    this.context.Survey.Update(survey);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new ArgumentException("No Survey found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<Survey>> GetAllSurveys()
        {
            try
            {
                var data = await this.context.Survey.Where(x => x.Status)
                    .Include(x=>x.ConstructionContract)
                    .Include(x=>x.Request)
                    .Include(x=>x.Staff)
                    .OrderByDescending(x=>x.CreateAt)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<Survey>> GetSurveyById(string? surveyId)
        {
            try
            {
                var data = await this.context.Survey.Where(x => x.Status && x.SurveyId.Equals(surveyId))
                    .Include(x => x.ConstructionContract)
                    .Include(x => x.Request)
                    .Include(x => x.Staff)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<Survey>> GetSurveyByIdstaff(string surveyId)
        {
            try
            {
                var data = await this.context.Survey.Where(x => x.Status && x.StaffId.Equals(surveyId))
                    .Include(x => x.ConstructionContract)
                    .Include(x => x.Request)
                    .Include(x => x.Staff)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Staff không tồn tại trong hệ thống!");
            }
        }

        public async Task<bool> InsertSurvey(SurveyDTO survey)
        {
            try
            {
                var _survey = new Survey();
                _survey.SurveyId = "SUR" + Guid.NewGuid().ToString().Substring(0, 13);
                _survey.Description = survey.Description;
                _survey.Note= survey.Note;
                _survey.StaffId= survey.StaffId;
                _survey.Status = true;
                _survey.RequestId = survey.RequestId ?? null;
                _survey.KWperMonth = survey.KWperMonth ?? 0;
                _survey.RoofArea = survey.RoofArea ?? 0;
                await this.context.Survey.AddAsync(_survey);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> UpdateSurvey(SurveyUpdateDTO upSurvey)
        {
            try
            {
                Survey _survey = await this.context.Survey.FirstAsync(x => x.SurveyId == upSurvey.SurveyId);
                if (_survey != null)
                {
                    _survey.Description = upSurvey.Description ?? _survey.Description;
                    _survey.Note = upSurvey.Note ?? _survey.Note;
                    _survey.StaffId = upSurvey.StaffId ?? _survey.StaffId;
                    _survey.Status = upSurvey.Status ?? _survey.Status;
                    _survey.RequestId = upSurvey.RequestId ?? _survey.RequestId;
                    _survey.KWperMonth = upSurvey.KWperMonth ?? _survey.KWperMonth;
                    _survey.RoofArea = upSurvey.RoofArea ?? _survey.RoofArea;
                    context.Survey.Update(_survey);
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ArgumentException("Survey not found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }
    }
}
